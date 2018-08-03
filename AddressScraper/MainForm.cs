using HtmlAgilityPack;
using Newtonsoft.Json;
using ScrapySharp.Network;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AddressScraper
{
    public partial class MainForm : Form
    {
         
        ReturnData iller;
        ReturnData ilceler;
        ReturnData koyler;
        ReturnData mahalleler;
        HtmlNodeCollection caddeler;
        HtmlNodeCollection binalar;
        HtmlNodeCollection daireler;

        public class Yt
        {
            public string value { get; set; }
            public string text { get; set; }
        }

        public class ReturnData
        {
            public List<Yt> yt { get; set; }
        }

        public MainForm()
        {
            InitializeComponent();
        }

        private async void btnGetCities_Click(object sender, EventArgs e)
        {
            ScrapingBrowser browser = new ScrapingBrowser();
            browser.Encoding = Encoding.UTF8;

            //set UseDefaultCookiesParser as false if a website returns invalid cookies format
            //browser.UseDefaultCookiesParser = false;
            WebPage homePage = null;

            //WebPage homePage = await browser.NavigateToPageAsync(new Uri("http://adreskodu.dask.gov.tr/"));
            //Debug.WriteLine(homePage.Html.OuterHtml);
            homePage = await browser.NavigateToPageAsync(new Uri("http://adreskodu.dask.gov.tr/site-element/control/load.ashx"),
                HttpVerb.Post, "fZwxqvPOpAViHEOXXVuXBaTd+2018072821lryxuVH5Y45L6Yb8Wo05CB46f1vrJzTCd1vDE8EiSLYLbaFSn0O0MhabkTx4t3A+Q==&t=il&u=0");
            Debug.WriteLine(homePage.Html.OuterHtml);
            iller = JsonConvert.DeserializeObject<ReturnData>(homePage.Html.OuterHtml);
            comboBox1.Items.Clear();
            foreach (var city in iller.yt)
            {
                comboBox1.Items.Add(city.text);
            }
            comboBox1.SelectedIndex = 0;

            //HtmlNode[] resultsLinks = resultsPage.Html.CssSelect("div.sb_tlst h3 a").ToArray();

            //WebPage blogPage = resultsPage.FindLinks(By.Text("romcyber blog | Just another WordPress site")).Single().Click();
        }

        private async void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var index = comboBox1.SelectedIndex;
            if (index == 0) return;
            var ilkodu = iller.yt[index].value;
            var postData = "fZwxqvPOpAViHEOXXVuXBaTd+2018072821lryxuVH5Y45L6Yb8Wo05CB46f1vrJzTCd1vDE8EiSLYLbaFSn0O0MhabkTx4t3A+Q==&t=ce&u=";
            ScrapingBrowser browser = new ScrapingBrowser();
            browser.Encoding = Encoding.UTF8;
            WebPage homePage = null;
            homePage = await browser.NavigateToPageAsync(new Uri("http://adreskodu.dask.gov.tr/site-element/control/load.ashx"),
                HttpVerb.Post, postData + ilkodu);
            ilceler = JsonConvert.DeserializeObject<ReturnData>(homePage.Html.OuterHtml);
            comboBox2.Items.Clear();
            foreach (var ilce in ilceler.yt)
            {
                comboBox2.Items.Add(ilce.text);
            }
            comboBox2.SelectedIndex = 0;
        }

        private async void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            var index = comboBox2.SelectedIndex;
            if (index == 0) return;
            var ilcekodu = ilceler.yt[index].value;
            var postData = "fZwxqvPOpAViHEOXXVuXBaTd+2018072821lryxuVH5Y45L6Yb8Wo05CB46f1vrJzTCd1vDE8EiSLYLbaFSn0O0MhabkTx4t3A+Q==&t=vl&u=";
            ScrapingBrowser browser = new ScrapingBrowser();
            browser.Encoding = Encoding.UTF8;
            WebPage homePage = null;
            homePage = await browser.NavigateToPageAsync(new Uri("http://adreskodu.dask.gov.tr/site-element/control/load.ashx"),
                HttpVerb.Post, postData + ilcekodu);
            koyler = JsonConvert.DeserializeObject<ReturnData>(homePage.Html.OuterHtml);
            comboBox3.Items.Clear();
            foreach (var koy in koyler.yt)
            {
                comboBox3.Items.Add(koy.text);
            }
            comboBox3.SelectedIndex = 0;
        }

        private async void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            var index = comboBox3.SelectedIndex;
            if (index == 0) return;
            var koykodu = koyler.yt[index].value;
            var postData = "fZwxqvPOpAViHEOXXVuXBaTd+2018072821lryxuVH5Y45L6Yb8Wo05CB46f1vrJzTCd1vDE8EiSLYLbaFSn0O0MhabkTx4t3A+Q==&t=mh&u=";
            ScrapingBrowser browser = new ScrapingBrowser();
            browser.Encoding = Encoding.UTF8;
            WebPage homePage = null;
            homePage = await browser.NavigateToPageAsync(new Uri("http://adreskodu.dask.gov.tr/site-element/control/load.ashx"),
                HttpVerb.Post, postData + koykodu);
            mahalleler = JsonConvert.DeserializeObject<ReturnData>(homePage.Html.OuterHtml);
            comboBox4.Items.Clear();
            foreach (var mahalle in mahalleler.yt)
            {
                comboBox4.Items.Add(mahalle.text);
            }
            comboBox4.SelectedIndex = 0;
        }

        private async void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            var index = comboBox4.SelectedIndex;
            if (index == 0) return;
            var mahallekodu = mahalleler.yt[index].value;
            var postData = $"fZwxqvPOpAViHEOXXVuXBaTd+2018072821lryxuVH5Y45L6Yb8Wo05CB46f1vrJzTCd1vDE8EiSLYLbaFSn0O0MhabkTx4t3A+Q==&t=sf&u={mahallekodu}&term=";
            ScrapingBrowser browser = new ScrapingBrowser();
            browser.Encoding = Encoding.UTF8;
            WebPage homePage = null;
            homePage = await browser.NavigateToPageAsync(new Uri("http://adreskodu.dask.gov.tr/site-element/control/load.ashx"),
                HttpVerb.Post, postData);

            caddeler = homePage.Html.SelectNodes("//tbody/tr");
            comboBox5.Items.Clear();
            comboBox5.Items.Add("SEÇİNİZ");

            foreach (var cadde in caddeler)
            {
                var hucreler = cadde.SelectNodes("td");
                var caddeIsmi = hucreler[0].InnerText + "-" + hucreler[1].InnerText;
                caddeIsmi = caddeIsmi.Replace("&nbsp;", " ");
                comboBox5.Items.Add(caddeIsmi);
            }
            comboBox5.SelectedIndex = 0;
        }

        private async void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            var index = comboBox5.SelectedIndex;
            if (index == 0) return;
            index = index - 1;
            var caddekodu = caddeler[index].GetAttributeValue("id", "").Substring(1);

            var postData = $"fZwxqvPOpAViHEOXXVuXBaTd+2018072821lryxuVH5Y45L6Yb8Wo05CB46f1vrJzTCd1vDE8EiSLYLbaFSn0O0MhabkTx4t3A+Q==&t=dk&u={caddekodu}&term=";
            ScrapingBrowser browser = new ScrapingBrowser();
            browser.Encoding = Encoding.UTF8;
            WebPage homePage = null;
            homePage = await browser.NavigateToPageAsync(new Uri("http://adreskodu.dask.gov.tr/site-element/control/load.ashx"),
                HttpVerb.Post, postData);

            binalar = homePage.Html.SelectNodes("//tbody/tr");
            comboBox6.Items.Clear();
            comboBox6.Items.Add("SEÇİNİZ");

            foreach (var bina in binalar)
            {
                var hucreler = bina.SelectNodes("td");
                var binaIsmi = hucreler[0].InnerText + "-" + hucreler[1].InnerText + "-" + hucreler[2].InnerText + "-" + hucreler[3].InnerText;
                binaIsmi = binaIsmi.Replace("&nbsp;", " ");
                comboBox6.Items.Add(binaIsmi);
            }
            comboBox6.SelectedIndex = 0;

        }

        private async void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            var index = comboBox6.SelectedIndex;
            if (index == 0) return;
            index = index - 1;
            var binakodu = binalar[index].GetAttributeValue("id", "").Substring(1);

            var postData = $"fZwxqvPOpAViHEOXXVuXBaTd+2018072821lryxuVH5Y45L6Yb8Wo05CB46f1vrJzTCd1vDE8EiSLYLbaFSn0O0MhabkTx4t3A+Q==&t=ick&u={binakodu}&term=";
            ScrapingBrowser browser = new ScrapingBrowser();
            browser.Encoding = Encoding.UTF8;
            WebPage homePage = null;
            homePage = await browser.NavigateToPageAsync(new Uri("http://adreskodu.dask.gov.tr/site-element/control/load.ashx"),
                HttpVerb.Post, postData);

            daireler = homePage.Html.SelectNodes("//tbody/tr");
            comboBox7.Items.Clear();
            comboBox7.Items.Add("SEÇİNİZ");

            foreach (var daire in daireler)
            {
                var hucreler = daire.SelectNodes("td");
                var daireIsmi = hucreler[0].InnerText + "-" + hucreler[1].InnerText;
                daireIsmi = daireIsmi.Replace("&nbsp;", " ");
                comboBox7.Items.Add(daireIsmi);

                Debug.WriteLine("Adres Kodu: " + daire.GetAttributeValue("id", "").Substring(1));

            }
            comboBox7.SelectedIndex = 0;

        }

        private async void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            var index = comboBox7.SelectedIndex;
            if (index == 0) return;
            index = index - 1;
            var dairekodu = daireler[index].GetAttributeValue("id", "").Substring(1);

            var postData = $"fZwxqvPOpAViHEOXXVuXBaTd+2018072821lryxuVH5Y45L6Yb8Wo05CB46f1vrJzTCd1vDE8EiSLYLbaFSn0O0MhabkTx4t3A+Q==&t=adr&u={dairekodu}";
            ScrapingBrowser browser = new ScrapingBrowser();
            browser.Encoding = Encoding.UTF8;
            WebPage homePage = null;
            homePage = await browser.NavigateToPageAsync(new Uri("http://adreskodu.dask.gov.tr/site-element/control/load.ashx"),
                HttpVerb.Post, postData);

            var adres = homePage.Html.InnerText;
            adres += "\r\nAdres Kodu:" + dairekodu;
            richTextBox1.Text = adres;


        }
    }
}
