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

            var rows = doc.DocumentNode.SelectSingleNode("//tbody").Descendants("tr");

            foreach (HtmlNode col in doc.DocumentNode.SelectNodes("//table[@id='table2']//tr//td"))
            {
                if (DG1.Rows.Count == 0)
                {
                DG1.Rows.Add;

                }
                DataGridViewCell newcell = new DataGridViewCell();
                DG1.Rows[DG1.Rows.Count-1].Cells.Add()
            }
                    
                    //Response.Write(col.InnerText);

            //List<List<string>> table = doc.DocumentNode.SelectSingleNode("tbody")
            //            .Descendants("tr")
            //            .Skip(1)
            //            .Where(tr => tr.Elements("td").Count() > 1)
            //            .Select(tr => tr.Elements("td").Select(td => td.InnerText.Trim()).ToList())
            //            .ToList();
            //MessageBox.Show("done...");


        }
    }
}
