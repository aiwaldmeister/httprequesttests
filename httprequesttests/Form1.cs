using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using HtmlAgilityPack;
using System.Linq;
using System.Xml.Serialization;


namespace httprequesttests
{
    public partial class Form1 : Form
    {
        string myUsername;
        string myPW;
        string myBaseURL = "https://www.crazypatterns.net/de/";
        string myLoginPage = "users/login";
        string myPatmanPage = "patman";
        string myDashboardPage = "users/dashboard";
        bool notifyViews = true;
        bool logViews = true;
        bool preventFormClosing = true;
        CookieAwareWebClient client = new CookieAwareWebClient();
        List<patterndata> referencepatdataList = new List<patterndata>();
        List<patterndata> currentpatdataList = new List<patterndata>();
        headerdata referenceheaderdata;
        headerdata currentheaderdata;
        List<notification> notificationQueue = new List<notification>();


        public class notification
        {
            public string timestamp { get; set; }
            public string type { get; set; }
            public string message { get; set; }

            public notification(string timestamp,string type, string message)
            {
                this.timestamp = timestamp;
                this.type = type;
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
            public string ratings { get; set; }
            public string comments { get; set; }
            public string messages { get; set; }

            public headerdata(string balance, string followers, string sells, string ratings, string comments, string messages)
            {
                this.balance = balance;
                this.followers = followers;
                this.sells = sells;
                this.ratings = ratings;
                this.comments = comments;
                this.messages = messages;
            }
            public headerdata()
            {
                this.balance = "";
                this.followers = "";
                this.sells = "";
                this.ratings = "";
                this.comments = "";
                this.messages = "";
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

        public void saveStatetoXML(accountdata acc)
        {
        XmlSerializer xs = new XmlSerializer(typeof(accountdata));
        TextWriter tw = new StreamWriter("state.xml");
        xs.Serialize(tw, acc);
            tw.Close();
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

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            myPW = ConfigurationManager.AppSettings["myPW"];
            myUsername = ConfigurationManager.AppSettings["myUsername"];
            int intervall = 10000;
            Int32.TryParse(ConfigurationManager.AppSettings["intervall"], out intervall);
            trackBar1.Value = intervall;
            timer1.Interval = intervall;

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

            XmlSerializer xs = new XmlSerializer(typeof(accountdata));
            accountdata referenceaccountdata = new accountdata();
            using (var sr = new StreamReader("state.xml"))
            {
                referenceaccountdata = (accountdata)xs.Deserialize(sr);
            }
            referencepatdataList = referenceaccountdata.patterns;
            referenceheaderdata = referenceaccountdata.header;

            showdata(referenceheaderdata,referencepatdataList);

            timer1.Enabled = true;
            button_Timer_Toggle.Text = "Stop Tracking";
            refresh();

        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            e.Cancel= preventFormClosing;
            preventFormClosing = true;

        }

        private bool login()
        {


            //MessageBox.Show(client.cookie.Count.ToString());

            client = new CookieAwareWebClient();
            client.Encoding = Encoding.UTF8;
            client.BaseAddress = myBaseURL;
            var loginData = new System.Collections.Specialized.NameValueCollection();
            loginData.Add("username", myUsername);
            loginData.Add("password", myPW);
            loginData.Add("permalogin", "1");

            //MessageBox.Show(client.cookie.Count.ToString());

            try
            {
                client.UploadValues(myLoginPage, "POST", loginData);

            }
            catch (Exception)
            {

                throw;
            }

            //MessageBox.Show(client.cookie.Count.ToString());

            return getClientLoggedin();

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

        private void button_Webclient_Request_Click(object sender, EventArgs e)
        {
            refresh();
        }

        private void refresh()
        {
            if (!getClientLoggedin())
            {
                login();
            }


            string page = client.DownloadString(myPatmanPage);
            textBox_response.Text = page;

            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(page);

            var TDs = doc.DocumentNode.SelectSingleNode("//tbody").Descendants("tr")
            .Where(tr => tr.Elements("td").Count() > 1)
            .Select(tr => tr.Elements("td").Select(td => td.InnerText.Trim()).ToList())
            .ToList();



            //fill newList

            currentpatdataList.Clear();
            foreach (List<string> Rows in TDs)
            {
                patterndata pat = new patterndata(
                    Rows[3],  //number
                    Rows[2].Substring(0, Rows[2].IndexOf("\r\n")),  //name
                    Rows[8],  //views
                    Rows[9],  //sells
                    Rows[13], //wishlists
                    Rows[15], //ratings
                    Rows[14]  //score
                    );

                currentpatdataList.Add(pat);
                
            }

            

            //TODO
            //Compare newList to refList
            bool changes = CompareLists(currentpatdataList, referencepatdataList);
            if (changes)
            {
                showdata(currentheaderdata,currentpatdataList);
            }


            //neue Liste ist fürs nächte mal die referenz
            referencepatdataList.Clear();
            foreach (patterndata pat in currentpatdataList)
            {
                referencepatdataList.Add(pat);
            }

            
        }

        public static String Timestamp()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmss");
        }

        public bool CompareLists(List<patterndata> newlist, List<patterndata> oldlist)
        {
            bool changeshappened = false;
            
            foreach (patterndata n in newlist)
            {
                foreach (patterndata o in oldlist)
                {
                    if (n.number.CompareTo(o.number)==0)
                    {//found the matching pattern in the List, now we can copare values

                        if (n.name.Equals(o.name)!=true)
                        {
                            AddAlert(Timestamp(), "name", "Der name von Pattern '" + o.name + "' wurde zu '" + n.name + "' geändert.");
                            changeshappened = true;
                        }

                        if (n.views.Equals(o.views)!= true)
                        {
                            AddAlert(Timestamp(), "views", "Die Anzahl der Views auf " + n.name + " ist von " + o.views + " auf " + n.views + " gestiegen.");
                            changeshappened = true;
                        }

                        if (n.sells.Equals(o.sells) != true)
                        {
                            AddAlert(Timestamp(), "sells", "Die Anzahl der Verkäufe auf " + n.name + " ist von " + o.sells + " auf " + n.sells + " gestiegen.");
                            changeshappened = true;
                        }

                        if (n.wishlists.Equals(o.wishlists) != true)
                        {
                            AddAlert(Timestamp(), "wishlists", "Die Anzahl der Wunschlisten auf " + n.name + " hat sich von " + o.wishlists + " auf " + n.wishlists + " geändert.");
                            changeshappened = true;
                        }

                        if (n.ratings.Equals(o.ratings) != true)
                        {
                            AddAlert(Timestamp(), "score", "Die Anzahl der Bewertungen auf " + n.name + " ist von " + o.ratings + " auf " + n.ratings + " gestiegen. (Neuer Schnitt ist "  + n.score + ")");
                            changeshappened = true;
                        }

                        break;
                    }
                }
            }
            return changeshappened;
        }

        private void AddAlert(string timestamp, string type ,string message)
        {
            //MessageBox.Show(message);
            //TODO Akustisches Signal abhängig von type des Alerts

            notificationQueue.Add(new notification(timestamp, type, message));

            //Ausgabe in der Textbox
            textBox_Alerts.AppendText(timestamp + ": " + message + Environment.NewLine);

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

            //TODO
            //Display Accountwide data
            

        }

        private void button_Timer_Toggle_Click(object sender, EventArgs e)
        {
            if (!timer1.Enabled)
            {
                timer1.Enabled = true;
                button_Timer_Toggle.Text = "Stop Tracking";
            }
            else
            {
                timer1.Enabled = false;
                button_Timer_Toggle.Text = "Start Tracking";
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            refresh();
        }

        private void button_DisposeAlerts_Click(object sender, EventArgs e)
        {
            textBox_Alerts.Clear();
            notificationQueue.Add(new notification("10000", "asdf", "asdfasdf"));
            //showNotification(new notification("10000","asdf","asdfasdf"));
            //ToastNotificationManager.CreateToastNotifier("MyApplicationId").Show(toast);
        }

        private void showNotificationfromQueue()
        {
            

            if (notificationQueue.Count > 0)
            {
                showNotification(notificationQueue.Last());
                notificationQueue.Remove(notificationQueue.Last());
            }
        }

        private void showNotification(notification notification)
        {
            notifyIcon1.Visible = true;
            notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;

            //notifyIcon1.ShowBalloonTip();

            notifyIcon1.ShowBalloonTip(10000, "Heureka!", notification.message, ToolTipIcon.Info);

   

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

        private void setConfigValue(string key, string value)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);

            config.AppSettings.Settings.Remove(key);
            config.AppSettings.Settings.Add(key, value);
            config.Save(ConfigurationSaveMode.Modified);
        }



        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //this.Visible = true;
            this.WindowState = FormWindowState.Normal;
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
            timer1.Interval = trackBar1.Value;

        }

        private void notifyIcon1_BalloonTipClicked(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            saveStatetoXML(new accountdata(new headerdata(), referencepatdataList));

            Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
            //config.AppSettings.Settings.Remove("myCookie");
            //config.AppSettings.Settings.Add("myCookie", myCookie);
            config.Save(ConfigurationSaveMode.Minimal);
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
                    this.Show();
                    this.WindowState = FormWindowState.Normal;
                }
                else
                {
                    this.Hide();
                    this.WindowState = FormWindowState.Minimized;
                }
            }

        }
    }
}           