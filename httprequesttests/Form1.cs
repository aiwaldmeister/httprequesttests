﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using HtmlAgilityPack;
using System.Linq;
using System.Xml.Serialization;
using System.Globalization;

namespace httprequesttests
{
    public partial class Form1 : Form
    {
        string myUsername;
        string myPW;
        string baseURL = "https://www.crazypatterns.net/de/";
        string loginURL = "users/login";
        string patmanURL = "patman";
        string dashboardURL = "users/dashboard";
        bool notifyViews = true;
        bool logViews = true;
        bool preventFormClosing = true;
        CookieAwareWebClient client = new CookieAwareWebClient();
        List<patterndata> referencepatdataList = new List<patterndata>();
        List<patterndata> currentpatdataList = new List<patterndata>();
        headerdata referenceheaderdata = new headerdata();
        headerdata currentheaderdata = new headerdata();
        List<notification> notificationQueue = new List<notification>();

        public Form1()
        {
            InitializeComponent();
        }

        public class notification
        {
            public string timestamp { get; set; }
            public string type { get; set; }
            public string subject { get; set; }
            public string message { get; set; }

            public notification(string timestamp, string type, string subject, string message)
            {
                this.timestamp = timestamp;
                this.type = type;
                this.subject = subject;
                this.message = message;
            }

            public void show()
            {
                
            }

        }

        [XmlRootAttribute("accountdata")]
        public class accountdata
        {
            public headerdata header { get; set; }
            public List<patterndata> patterns { get; set; }

            public accountdata(headerdata header, List<patterndata> patterns)
            {
                this.header = header;
                this.patterns = patterns;
            }
            public accountdata()
            {

            }
        }
       
        public class headerdata
        {
            public string balance { get; set; }
            public string followers { get; set; }
            public string sells { get; set; }
            public string unreadmessages { get; set; }
            public string comments { get; set; }

            public headerdata(string balance, string followers, string sells, string unreadmessages, string comments)
            {
                this.balance = balance;
                this.followers = followers;
                this.sells = sells;
                this.unreadmessages = unreadmessages;
                this.comments = comments;
            }
            public headerdata()
            {
                this.balance = "";
                this.followers = "";
                this.sells = "";
                this.unreadmessages = "";
                this.comments = "";
            }

            internal void clear()
            {
                this.balance = "";
                this.followers = "";
                this.sells = "";
                this.unreadmessages = "";
                this.comments = "";
            }
        }
        
        public class patterndata
        {
            public string number { get; set; }
            public string name { get; set; }
            public string views { get; set; }
            public string sells { get; set; }
            public string wishlists { get; set; }
            public string ratings { get; set; }
            public string score { get; set; }

            public patterndata(string number, string name, string views, string sells, string wishlists, string ratings, string score)
            {
                this.number = number;
                this.name = name;
                this.views = views;
                this.sells = sells;
                this.wishlists = wishlists;
                this.ratings = ratings;
                this.score = score;
            }
            public patterndata()
            {
                this.number = "";
                this.name = "";
                this.views = "";
                this.sells = "";
                this.wishlists = "";
                this.ratings = "";
                this.score = "";
            }
        }

        public class CookieAwareWebClient : WebClient
        {
            public CookieContainer cookie = new CookieContainer();

            protected override WebRequest GetWebRequest(Uri address)
            {
                WebRequest request = base.GetWebRequest(address);
                if (request is HttpWebRequest)
                {
                    (request as HttpWebRequest).CookieContainer = cookie;
                }
                return request;
            }

        }
        
        private void setConfigValue(string key, string value)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);

            config.AppSettings.Settings.Remove(key);
            config.AppSettings.Settings.Add(key, value);
            config.Save(ConfigurationSaveMode.Modified);
        }

        private void Form1_Load(object sender, EventArgs e)
        {           
            myPW = ConfigurationManager.AppSettings["myPW"];
            myUsername = ConfigurationManager.AppSettings["myUsername"];
            int intervall = 10000;
            Int32.TryParse(ConfigurationManager.AppSettings["intervall"], out intervall);
            trackBar1.Value = intervall;
            timer_autorefresh.Interval = intervall;

            if (ConfigurationManager.AppSettings["logViews"] =="1")
            {
                logViews = true;
            }
            else
            {
                logViews = false;
            }

            if (ConfigurationManager.AppSettings["notifyViews"] == "1")
            {
                notifyViews = true;
            }
            else
            {
                notifyViews = false;
            }
            checkBox_LogViews.Checked = logViews;
            checkBox_NotifyViews.Checked = notifyViews;

            if (System.IO.File.Exists("state.xml")){

            XmlSerializer xs = new XmlSerializer(typeof(accountdata));
            accountdata referenceaccountdata = new accountdata();
            using (var sr = new StreamReader("state.xml"))
            {
                referenceaccountdata = (accountdata)xs.Deserialize(sr);
            }
            referencepatdataList = referenceaccountdata.patterns;
            referenceheaderdata = referenceaccountdata.header;

            showdata(referenceheaderdata,referencepatdataList);
            }

            timer_autorefresh.Enabled = true;
            button_Timer_Toggle.Text = "Stop Tracking";
            listView1.Items.Clear();
            refresh();

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            hidetheForm();
            e.Cancel = preventFormClosing;
            preventFormClosing = true;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            saveStatetoXML(new accountdata(referenceheaderdata, referencepatdataList));

            Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
            //config.AppSettings.Settings.Remove("myCookie");
            //config.AppSettings.Settings.Add("myCookie", myCookie);
            config.Save(ConfigurationSaveMode.Minimal);
        }

        public void saveStatetoXML(accountdata acc)
        {
        XmlSerializer xs = new XmlSerializer(typeof(accountdata));
        TextWriter tw = new StreamWriter("state.xml");
        xs.Serialize(tw, acc);
            tw.Close();
        }

        private bool getClientLoggedin()
        {
            if (client.cookie.Count == 7) //bei erfolgreichem Login sollten 7 Cookies gesetzt sein, sonst nur 0-4 -> prüfbar über die auskommentierten msg in der login methode
            {
                this.Text = "CrazyPatterns Event-Tracker... Angemeldet als " + myUsername;
                notifyIcon1.Text = "Angemeldet als " + myUsername;
                return true;

            }
            else
            {
                this.Text = "CrazyPatterns Event-Tracker... OFfline";
                notifyIcon1.Text = "Offline";
                return false;
                
            }
                        
        }

        private bool login()
        {


            //MessageBox.Show(client.cookie.Count.ToString());

            client = new CookieAwareWebClient();
            client.Encoding = Encoding.UTF8;
            client.BaseAddress = baseURL;
            var loginData = new System.Collections.Specialized.NameValueCollection();
            loginData.Add("username", myUsername);
            loginData.Add("password", myPW);
            loginData.Add("permalogin", "1");

            //MessageBox.Show(client.cookie.Count.ToString());

            try
            {
                client.UploadValues(loginURL, "POST", loginData);

            }
            catch (Exception)
            {

                throw;
            }

            //MessageBox.Show(client.cookie.Count.ToString());

            return getClientLoggedin();

        }

        private void fillcurrentpatdata()
        {
            string patmanpage = client.DownloadString(patmanURL);
            textBox_response.Text = patmanpage;

            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(patmanpage);

            var TDs = doc.DocumentNode.SelectSingleNode("//tbody").Descendants("tr")
            .Where(tr => tr.Elements("td").Count() > 1)
            .Select(tr => tr.Elements("td").Select(td => td.InnerText.Trim()).ToList())
            .ToList();

            
            currentpatdataList.Clear();
            foreach (List<string> Rows in TDs)
            {
                patterndata pat = new patterndata();
                pat.number = Rows[3];
                pat.name = Rows[2].Substring(0, Rows[2].IndexOf("\r\n"));  //name
                pat.views = Rows[8];
                pat.sells = Rows[9];
                pat.wishlists = Rows[14];
                pat.ratings = Rows[16];
                pat.score = Rows[15];

                currentpatdataList.Add(pat);                
            }

        }

        private void fillcurrentheaderdata()
        {
            //TODO:

            currentheaderdata.clear();
                        
            string headerpage = client.DownloadString(dashboardURL);

            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(headerpage);

            var values = doc.DocumentNode.SelectSingleNode("//div[@class='col-xs-12 col-sm-12 col-md-12 col-lg-12 dashboard']")
                .Descendants("div").Where(div => div.Descendants("div").Count()<1).Select(div => div.InnerText.Trim()).ToList();

            for (int i = 0; i < values.Count; i++)
            {
                if (values[i].Equals("Kontostand")) currentheaderdata.balance = values[i + 1];
                if (values[i].Equals("Verkäufe insgesamt")) currentheaderdata.sells = values[i + 1];
                if (values[i].Equals("Abonnenten")) currentheaderdata.followers = values[i + 1];
                               
            }

            var values2 = doc.DocumentNode.SelectSingleNode("//table[@class='table']")
                .Descendants("span").Select(span => span.InnerText.Trim()).ToList();

            for (int i = 0; i < values2.Count; i++)
            {
                if (values2[i].Equals("Benachrichtigungen (alle / ungelesen)")) currentheaderdata.unreadmessages = values2[i + 2];
                if (values2[i].Equals("Kommentare zu meinen Produkten")) currentheaderdata.comments = values2[i + 1];
            }

        }

        private void refresh()
        {
            if (!getClientLoggedin())
            {
                login();
            }

            fillcurrentpatdata();
            fillcurrentheaderdata();

            //Compare new header and patterndata to references
            bool changes = false;
            if (ComparepatdataLists(currentpatdataList, referencepatdataList))
            {
                changes = true;
            }
            if (Compareheaders(currentheaderdata, referenceheaderdata))
            {
                changes = true;
            }

            if (changes)
            {
                showdata(currentheaderdata,currentpatdataList);
            }

            //neuer header ist fürs nächste mal die referenz
            referenceheaderdata.balance = currentheaderdata.balance;
            referenceheaderdata.sells = currentheaderdata.sells;
            referenceheaderdata.followers = currentheaderdata.followers;
            referenceheaderdata.unreadmessages = currentheaderdata.unreadmessages;
            referenceheaderdata.comments = currentheaderdata.comments;


            //neue Liste ist fürs nächte mal die referenz
            referencepatdataList.Clear();
            foreach (patterndata pat in currentpatdataList)
            {
                referencepatdataList.Add(pat);
            }

            
        }

        public bool Compareheaders(headerdata newheader, headerdata oldheader)
        {
            bool changeshappened = false;

            // Änderungen prüfen und ggf. flag setzen
            if (!newheader.sells.Equals(oldheader.sells))
            {
                addNotificationToQueue(Timestamp(), "Accountweit", "totalsells", oldheader.sells, newheader.sells, "Verkäufe: " + oldheader.sells + " -> " + newheader.sells);
                changeshappened = true;
            }
            if (!newheader.balance.Equals(oldheader.balance))

            {
                addNotificationToQueue(Timestamp(), "Accountweit", "balance", oldheader.balance, newheader.balance, "Kontostand: " + oldheader.balance + " -> " + newheader.balance);
                changeshappened = true;
            }

            if (!newheader.followers.Equals(oldheader.followers))
            {
                addNotificationToQueue(Timestamp(), "Accountweit", "followers", oldheader.followers, newheader.followers, "Follower:" + oldheader.followers + " -> " + newheader.followers);
                changeshappened = true;
            }

            if (!newheader.unreadmessages.Equals(oldheader.unreadmessages))
            {
                addNotificationToQueue(Timestamp(), "Accountweit", "unreadmessages", oldheader.unreadmessages, newheader.unreadmessages, "ungelesene Benachrichtigungen: " + oldheader.unreadmessages + " -> " + newheader.unreadmessages);
                changeshappened = true;
            }

            if (!newheader.comments.Equals(oldheader.comments))
            {
                addNotificationToQueue(Timestamp(), "Accountweit", "comments", oldheader.comments, newheader.comments, "Kommentare: " + oldheader.comments + " -> " + newheader.comments);
                changeshappened = true;
            }


            return changeshappened;
        }

        public bool ComparepatdataLists(List<patterndata> newlist, List<patterndata> oldlist)
        {
            bool changeshappened = false;
            
            foreach (patterndata n in newlist)
            {
                foreach (patterndata o in oldlist)
                {
                    if (n.number.CompareTo(o.number)==0)
                    {//found the matching pattern in the List, now we can copare values

                        if (!n.name.Equals(o.name))
                        {
                            addNotificationToQueue(Timestamp(), o.name, "name", o.name, n.name, "Name: '" + o.name + "' -> '" + n.name + "'");
                            changeshappened = true;
                        }

                        if (!n.views.Equals(o.views))
                        {
                            addNotificationToQueue(Timestamp(), o.name, "views", o.views, n.views, "Views: " + o.views + " -> " + n.views);
                            changeshappened = true;
                        }

                        if (!n.sells.Equals(o.sells))
                        {
                            addNotificationToQueue(Timestamp(), o.name, "sells", o.sells, n.sells,  "Verkäufe: " + o.sells + " -> " + n.sells);
                            changeshappened = true;
                        }

                        if (!n.wishlists.Equals(o.wishlists))
                        {
                            addNotificationToQueue(Timestamp(), o.name, "wishlists", o.wishlists, n.wishlists,  "Wunschlisten: " + o.wishlists + " -> " + n.wishlists);
                            changeshappened = true;
                        }

                        if (!n.ratings.Equals(o.ratings))
                        {
                            addNotificationToQueue(Timestamp(), o.name, "ratings", o.ratings, n.ratings, "Bewertungen: " + o.ratings + " -> " + n.ratings);
                            changeshappened = true;
                        }

                        if (!n.score.Equals(o.score))
                        {
                            addNotificationToQueue(Timestamp(), o.name, "score", o.score, n.score, "Bewertung: " + o.score + " -> " + n.score);
                            changeshappened = true;
                        }

                        break;
                    }
                }
            }
            return changeshappened;
        }
        
        private void showdata(headerdata head, List<patterndata> patterns)
        {
            //populate DataGridView...
            DG1.Rows.Clear();
            foreach (patterndata pat in patterns)
            {
                DG1.Rows.Add(
                    pat.number,
                    pat.name,
                    pat.views,
                    pat.sells,
                    pat.wishlists,
                    pat.ratings,
                    pat.score);
            }

            //Display Accountwide data
            textBox_headerdata.Clear();
            textBox_headerdata.AppendText("Kontostand: " + head.balance + "\r\n");
            textBox_headerdata.AppendText("Follower: " + head.followers + "\r\n");
            textBox_headerdata.AppendText("Verkäufe: " + head.sells + "\r\n");
            textBox_headerdata.AppendText("Ungelesene Nachrichten: " + head.unreadmessages + "\r\n");
            textBox_headerdata.AppendText("Kommentare: " + head.comments);

        }

        public static String Timestamp()
        {
            return DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss",
                  CultureInfo.CreateSpecificCulture("de-DE"));
        }

        private void addNotificationToQueue(string timestamp, string subject, string type, string oldvalue, string newvalue ,string message)
        {
           

            if (notifyViews || !type.Equals("views"))
            {
                notificationQueue.Add(new notification(timestamp, type, subject, message));
                //TODO Akustisches Signal abhängig von type des Alerts
            }

            if (logViews || !type.Equals("views"))
            {
                //Ausgabe in der Textbox
                textBox_Alerts.AppendText(timestamp + ": " + Environment.NewLine + subject + Environment.NewLine + message + Environment.NewLine + Environment.NewLine);
                string direction = "è";

                if (!type.Equals(name))
                {
                    float oldfloat = 0;
                    float newfloat = 0;

                    float.TryParse(oldvalue, out oldfloat);
                    float.TryParse(newvalue, out newfloat);
                    
                    
                    //Pfusch für die Ausgabe der Richtungspfeile durch die Schriftart Wingdings
                    //è = Pfeil nach rechts
                    //ì = Pfeil nach rechts oben
                    //î = Pfeil nach rechts unten
                    if (newfloat > oldfloat)
                    {
                        direction = "ì";
                    }
                    if (newfloat < oldfloat)
                    {
                        direction = "î";
                    }
                }

                ListViewItem item = new ListViewItem(type);
                item.ImageKey = type;
                item.UseItemStyleForSubItems = false;

                item.SubItems.Add(timestamp);
                item.SubItems.Add(subject);
                item.SubItems.Add(oldvalue);
                item.SubItems.Add(direction);
                item.SubItems.Add(newvalue);

                item.SubItems[4].Font = new System.Drawing.Font( "Wingdings",12);
                if (direction.Equals("ì"))
                {
                    item.SubItems[4].ForeColor = System.Drawing.Color.DeepSkyBlue;
                }
                if (direction.Equals("î"))
                {
                    item.SubItems[4].ForeColor = System.Drawing.Color.OrangeRed;
                }

                listView1.Items.Add(item);
                

            }

            if (!timer_notifications.Enabled)
            {
                timer_notifications.Enabled = true;
            }

            button_ClearMessages.Text = "Meldungen leeren ("+ notificationQueue.Count +")";
        }

        private void showNotification(notification notification)
        {
            notifyIcon1.Visible = true;
            notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;

            //notifyIcon1.ShowBalloonTip();

            notifyIcon1.ShowBalloonTip(10000, notification.subject, notification.message, ToolTipIcon.Info);

   

        }

        private void showNotificationfromQueue()
        {
            if (notificationQueue.Count > 0)
            {
                showNotification(notificationQueue.First());
                notificationQueue.Remove(notificationQueue.First());
                button_ClearMessages.Text = "Meldungen leeren (" + notificationQueue.Count + ")";

            }
            else
            {
                timer_notifications.Enabled = false;
                button_ClearMessages.Text = "Meldungen leeren";
            }
        }

        private void timer_autorefresh_Tick(object sender, EventArgs e)
        {
            refresh();
        }

        private void button_refresh_Click(object sender, EventArgs e)
        {
            refresh();
        }

        private void button_Timer_Toggle_Click(object sender, EventArgs e)
        {
            if (!timer_autorefresh.Enabled)
            {
                timer_autorefresh.Enabled = true;
                button_Timer_Toggle.Text = "Stop Tracking";
            }
            else
            {
                timer_autorefresh.Enabled = false;
                button_Timer_Toggle.Text = "Start Tracking";
            }
        }

        private void button_Clear_Messages_Click(object sender, EventArgs e)
        {
            textBox_Alerts.Clear();
            notificationQueue.Clear();
            listView1.Items.Clear();
            button_ClearMessages.Text = "Meldungen leeren";
            //notificationQueue.Add(new notification("10000", "asdf", "asdfasdf"));
            //showNotification(new notification("10000","asdf","asdfasdf"));
            //ToastNotificationManager.CreateToastNotifier("MyApplicationId").Show(toast);
        }

        private void checkBox_LogViews_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_LogViews.Checked)
            {
                logViews = true;

                setConfigValue("logViews", "1");
            }
            else
            {
                logViews = false;
                setConfigValue("logViews", "0");
            }

            
        }

        private void checkBox_NotifyViews_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_NotifyViews.Checked)
            {
                notifyViews = true;
                setConfigValue("notifyViews", "1");
            }
            else
            {
                notifyViews = false;
                setConfigValue("notifyViews", "0");
            }

            
        }

        private void showtheForm()
        {
            this.WindowState = FormWindowState.Minimized;
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void hidetheForm()
        {
            this.WindowState = FormWindowState.Minimized;
            this.Hide();
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showtheForm();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            preventFormClosing = false;
            this.Close();
        }

        private void abbrechenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            contextMenu.Hide();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            setConfigValue("intervall", trackBar1.Value.ToString());
            timer_autorefresh.Interval = trackBar1.Value;

        }

        private void notifyIcon1_BalloonTipClicked(object sender, EventArgs e)
        {
            showtheForm();
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
            contextMenu.Show(Cursor.Position);
            }
            if (e.Button == MouseButtons.Left)
            {
                if (this.WindowState == FormWindowState.Minimized)
                {
                    showtheForm();
                }
                else
                {
                    hidetheForm();
                }
            }

        }

        private void timer_notifications_Tick(object sender, EventArgs e)
        {
            showNotificationfromQueue();
        }

        private void DG1_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
            if (e.Column.Index != 1) //übersteuern des Vergleichs beim Sortieren für alle Spalten ausser der Namensspalte
            {
                e.SortResult = float.Parse(e.CellValue1.ToString()).CompareTo(float.Parse(e.CellValue2.ToString()));
                e.Handled = true;//pass by the default sorting
            }
        }
    }
}           