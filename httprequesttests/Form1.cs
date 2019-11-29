using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using HtmlAgilityPack;
using System.Linq;

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
        CookieAwareWebClient client = new CookieAwareWebClient();
        List<patterndata> referencepatdataList = new List<patterndata>();
        List<patterndata> currentpatdataList = new List<patterndata>();
        accountdata referenceaccdata;
        accountdata newaccdata;


        public class accountdata
        {
            public string balance;
            public string followers;
            public string sells;
            public string ratings;
            public string comments;
            public string messages;

            public accountdata(string balance, string followers, string sells, string ratings, string comments, string messages)
            {
                this.balance = balance;
                this.followers = followers;
                this.sells = sells;
                this.ratings = ratings;
                this.comments = comments;
                this.messages = messages;
            }
            public List<String> getData()
            {
                var retlist = new List<string>();
                retlist.Add(this.balance);
                retlist.Add(this.followers);
                retlist.Add(this.sells);
                retlist.Add(this.ratings);
                retlist.Add(this.comments);
                retlist.Add(this.messages);
                
                return retlist;
            }

        }

        public class patterndata
        {
            public string number;
            public string name;
            public string views;
            public string sells;
            public string wishlists;
            public string ratings;
            public string score;

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

            public List<String> getData()
            {
                var retlist = new List<string>();
                retlist.Add(this.number);
                retlist.Add(this.name);
                retlist.Add(this.views);
                retlist.Add(this.sells);
                retlist.Add(this.wishlists);
                retlist.Add(this.ratings);
                retlist.Add(this.score);
                
                return retlist;
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

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            myPW = ConfigurationManager.AppSettings["myPW"];
            myUsername = ConfigurationManager.AppSettings["myUsername"];
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {


            Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
            //config.AppSettings.Settings.Remove("myCookie");
            //config.AppSettings.Settings.Add("myCookie", myCookie);
            config.Save(ConfigurationSaveMode.Minimal);

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
                return true;
            }
            else
            {
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

            shownewdata();

            //TODO
            //Compare newList to refList

            CompareLists(currentpatdataList, referencepatdataList);


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

        private void CompareLists(List<patterndata> newlist, List<patterndata> oldlist)
        {
            foreach (patterndata n in newlist)
            {
                foreach (patterndata o in oldlist)
                {
                    if (n.number.CompareTo(o.number)==0)
                    {//found the matching pattern in the List, now we can copare values

                        if (n.name.CompareTo(o.name)!=0)
                        {
                            AddAlert(Timestamp(), "name", "Der name von Pattern '" + o.name + "' wurde zu '" + n.name + "' geändert.");
                        }

                        if (n.views.CompareTo(o.views)!=0)
                        {
                            AddAlert(Timestamp(), "views", "Die Anzahl der Views auf " + n.name + " ist von " + o.views + " auf " + n.views + " gestiegen.");
                        }

                        if (n.views.CompareTo(o.views) != 0)
                        {
                            AddAlert(Timestamp(), "sells", "Die Anzahl der Verkäufe auf " + n.name + " ist von " + o.sells + " auf " + n.sells + " gestiegen.");
                        }

                        if (n.views.CompareTo(o.views) != 0)
                        {
                            AddAlert(Timestamp(), "wishlists", "Die Anzahl der Wunschlisten auf " + n.name + " hat sich von " + o.wishlists + " auf " + n.wishlists + " geändert.");
                        }

                        if (n.views.CompareTo(o.views) != 0)
                        {
                            AddAlert(Timestamp(), "score", "Die Anzahl der Bewertungen auf " + n.name + " ist von " + o.ratings + " auf " + n.ratings + " gestiegen. (Neuer Schnitt ist "  + n.score + ")");
                        }

                        break;
                    }
                }
            }
        }

        private void AddAlert(string timestamp, string type ,string message)
        {
            //MessageBox.Show(message);
            //TODO Akustisches Signal abhängig von type des Alerts

            //Ausgabe in der Textbox
            textBox_Alerts.AppendText(timestamp + ": " + message + Environment.NewLine);
        }

        private void shownewdata()
        {
            //populate DataGridView...
            DG1.Rows.Clear();
            foreach (patterndata pat in currentpatdataList)
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

    }
}           