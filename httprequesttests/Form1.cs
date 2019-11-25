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
        string formUrl;
        string myURL;
        string myUsername;
        string myPW;
        string cookieHeader;
        string myCookie;
        string myBaseURL;
        CookieAwareWebClient client = new CookieAwareWebClient();


        public Form1()
        {
            InitializeComponent();
        }

        public class CookieAwareWebClient : WebClient
        {
            private CookieContainer cookie = new CookieContainer();

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

        private void button_request_Click(object sender, EventArgs e)
        {
            WebRequest request = WebRequest.Create(myURL);
            request.Headers.Add("Cookie", myCookie);

            WebResponse response = request.GetResponse();
            Stream data = response.GetResponseStream();
            string html = String.Empty;
            using (StreamReader sr = new StreamReader(data))
            {
                html = sr.ReadToEnd();
                textBox_response.Text = html;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            formUrl = textBox_LoginURL.Text; // NOTE: This is the URL the form POSTs to, not the URL of the form (you can find this in the "action" attribute of the HTML's form tag
            myURL = textBox_url.Text;
            myBaseURL = textBox_BaseURL.Text;
            myPW = ConfigurationManager.AppSettings["myPW"];
            myUsername = ConfigurationManager.AppSettings["myUsername"];
            myCookie = ConfigurationManager.AppSettings["myCookie"];
            //MessageBox.Show(myCookie);





        }

        private void button_Login_Click(object sender, EventArgs e)
        {

            string formParams = string.Format("username={0}&password={1}&permalogin={2}", myUsername, myPW, "1");
            WebRequest req = WebRequest.Create(formUrl);
            req.ContentType = "application/x-www-form-urlencoded";
            req.Method = "POST";
            byte[] bytes = Encoding.ASCII.GetBytes(formParams);
            req.ContentLength = bytes.Length;
            using (Stream os = req.GetRequestStream())
            {
                os.Write(bytes, 0, bytes.Length);
            }
            WebResponse resp = req.GetResponse();
            cookieHeader = resp.Headers["Set-cookie"];

            //MessageBox.Show(cookieHeader);
            myCookie = cookieHeader;


        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {


            Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
            config.AppSettings.Settings.Remove("myCookie");
            config.AppSettings.Settings.Add("myCookie", myCookie);
            config.Save(ConfigurationSaveMode.Minimal);

                                   
        }

        private void button_WebClient_Login_Click(object sender, EventArgs e)
        {
            client = new CookieAwareWebClient();
            client.Encoding = Encoding.UTF8;
            client.BaseAddress = myBaseURL;
            var loginData = new System.Collections.Specialized.NameValueCollection();
            loginData.Add("username", myUsername);
            loginData.Add("password", myPW);
            loginData.Add("permalogin", "1");
            client.UploadValues("users/login", "POST", loginData);
        }

        private void button_Webclient_Request_Click(object sender, EventArgs e)
        {
            string page =  client.DownloadString("patman");
            textBox_response.Text = page;

            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(page);

            var TDs = doc.DocumentNode.SelectSingleNode("//tbody").Descendants("tr")
            .Where(tr => tr.Elements("td").Count() > 1)
            .Select(tr => tr.Elements("td").Select(td => td.InnerText.Trim()).ToList())
            .ToList();

            foreach(List<string> Rows in TDs)
            {

                string name = Rows[2];
                string number = Rows[3];
                string views = Rows[8];
                string sells = Rows[9];
                string wishlists = Rows[13];
                string ratings = Rows[15];

                name = name.Substring(0, name.IndexOf("\r\n"));
                               
                DG1.Rows.Add(name,number,views,sells,wishlists,ratings);
                                

            }




        }
    }
}
