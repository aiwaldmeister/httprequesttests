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
        CookieAwareWebClient client = new CookieAwareWebClient();
        List<patterndata> referenceList = new List<patterndata>();
        List<patterndata> newList = new List<patterndata>();

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
            public string toString()
            {
                return "";
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


        public Form1()
        {
            InitializeComponent();
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
            client = new CookieAwareWebClient();
            client.Encoding = Encoding.UTF8;
            client.BaseAddress = myBaseURL;
            var loginData = new System.Collections.Specialized.NameValueCollection();
            loginData.Add("username", myUsername);
            loginData.Add("password", myPW);
            loginData.Add("permalogin", "1");
            try
            {
                client.UploadValues(myLoginPage, "POST", loginData);

            }
            catch (Exception)
            {

                throw;
            }

            return getClientLoggedin();

        }

        private bool getClientLoggedin()
        {
            
            return false;
            //            if (client.cookie.ToString) { }

        }

        private void button_Webclient_Request_Click(object sender, EventArgs e)
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

                newList.Add(pat);
                
            }

            //populate DataGridView...
            DG1.Rows.Clear();
            foreach (patterndata pat in newList)
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
            
        }

        private void button_Timer_Toggle_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            {
                timer1.Enabled = false;
                button_Timer_Toggle.Text = "Activate Tracking";
            }
            else
            {
                timer1.Enabled = true;
                button_Timer_Toggle.Text = "Stop Tracking";
            }

        }
    }
}           