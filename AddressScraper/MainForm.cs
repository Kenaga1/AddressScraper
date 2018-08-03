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

        ReturnData _iller;
        ReturnData _ilceler;
        ReturnData _koyler;
        ReturnData _mahalleler;
        HtmlNodeCollection _caddeler;
        HtmlNodeCollection _binalar;
        HtmlNodeCollection _daireler;
        string _ilKodu;
        string _il;
        string _ilceKodu;
        string _ilce;
        string _koyKodu;
        string _koy;
        string _mahalleKodu;
        string _mahalle;
        bool _stopDownload;
        List<Adres> _adresListesi;

        public class Yt
        {
            public string value { get; set; }
            public string text { get; set; }
        }

        public class ReturnData
        {
            public List<Yt> yt { get; set; }
        }
        public class Adres
        {
            public string ilKodu { get; set; }
            public string il { get; set; }
            public string ilceKodu { get; set; }
            public string ilce { get; set; }
            public string koyKodu { get; set; }
            public string koy { get; set; }
            public string mahalleKodu { get; set; }
            public string mahalle { get; set; }
            public string caddeTuru { get; set; }
            public string caddeAdi { get; set; }
            public string binaNo { get; set; }
            public string binaAdi { get; set; }
            public string siteAdi { get; set; }
            public string apartmanAdi { get; set; }
            public string daireTuru { get; set; }
            public string icKapiNo { get; set; }
            public string adresKodu { get; set; }
            
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
            _iller = JsonConvert.DeserializeObject<ReturnData>(homePage.Html.OuterHtml);
            comboBox1.Items.Clear();
            foreach (var city in _iller.yt)
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
            var ilkodu = _iller.yt[index].value;
            var postData = "fZwxqvPOpAViHEOXXVuXBaTd+2018072821lryxuVH5Y45L6Yb8Wo05CB46f1vrJzTCd1vDE8EiSLYLbaFSn0O0MhabkTx4t3A+Q==&t=ce&u=";
            ScrapingBrowser browser = new ScrapingBrowser();
            browser.Encoding = Encoding.UTF8;
            WebPage homePage = null;
            homePage = await browser.NavigateToPageAsync(new Uri("http://adreskodu.dask.gov.tr/site-element/control/load.ashx"),
                HttpVerb.Post, postData + ilkodu);
            _ilceler = JsonConvert.DeserializeObject<ReturnData>(homePage.Html.OuterHtml);
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            comboBox4.Items.Clear();
            foreach (var ilce in _ilceler.yt)
            {
                comboBox2.Items.Add(ilce.text);
            }
            comboBox2.SelectedIndex = 0;
            _ilKodu = ilkodu;
            _il = _iller.yt[index].text;
        }

        private async void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            var index = comboBox2.SelectedIndex;
            if (index == 0) return;
            var ilcekodu = _ilceler.yt[index].value;
            var postData = "fZwxqvPOpAViHEOXXVuXBaTd+2018072821lryxuVH5Y45L6Yb8Wo05CB46f1vrJzTCd1vDE8EiSLYLbaFSn0O0MhabkTx4t3A+Q==&t=vl&u=";
            ScrapingBrowser browser = new ScrapingBrowser();
            browser.Encoding = Encoding.UTF8;
            WebPage homePage = null;
            homePage = await browser.NavigateToPageAsync(new Uri("http://adreskodu.dask.gov.tr/site-element/control/load.ashx"),
                HttpVerb.Post, postData + ilcekodu);
            _koyler = JsonConvert.DeserializeObject<ReturnData>(homePage.Html.OuterHtml);
            comboBox3.Items.Clear();
            comboBox4.Items.Clear();
            foreach (var koy in _koyler.yt)
            {
                comboBox3.Items.Add(koy.text);
            }
            comboBox3.SelectedIndex = 0;
            _ilceKodu = ilcekodu;
            _ilce = _ilceler.yt[index].text;
        }

        private async void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            var index = comboBox3.SelectedIndex;
            if (index == 0) return;
            var koykodu = _koyler.yt[index].value;
            var postData = "fZwxqvPOpAViHEOXXVuXBaTd+2018072821lryxuVH5Y45L6Yb8Wo05CB46f1vrJzTCd1vDE8EiSLYLbaFSn0O0MhabkTx4t3A+Q==&t=mh&u=";
            ScrapingBrowser browser = new ScrapingBrowser();
            browser.Encoding = Encoding.UTF8;
            WebPage homePage = null;
            homePage = await browser.NavigateToPageAsync(new Uri("http://adreskodu.dask.gov.tr/site-element/control/load.ashx"),
                HttpVerb.Post, postData + koykodu);
            _mahalleler = JsonConvert.DeserializeObject<ReturnData>(homePage.Html.OuterHtml);
            comboBox4.Items.Clear();
            foreach (var mahalle in _mahalleler.yt)
            {
                comboBox4.Items.Add(mahalle.text);
            }
            comboBox4.SelectedIndex = 0;
            _koyKodu = koykodu;
            _koy = _koyler.yt[index].text;
        }

        private async void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            var index = comboBox4.SelectedIndex;
            if (index == 0) return;
            var mahallekodu = _mahalleler.yt[index].value;
            var postData = $"fZwxqvPOpAViHEOXXVuXBaTd+2018072821lryxuVH5Y45L6Yb8Wo05CB46f1vrJzTCd1vDE8EiSLYLbaFSn0O0MhabkTx4t3A+Q==&t=sf&u={mahallekodu}&term=";
            ScrapingBrowser browser = new ScrapingBrowser();
            browser.Encoding = Encoding.UTF8;
            WebPage homePage = null;
            homePage = await browser.NavigateToPageAsync(new Uri("http://adreskodu.dask.gov.tr/site-element/control/load.ashx"),
                HttpVerb.Post, postData);

            _caddeler = homePage.Html.SelectNodes("//tbody/tr");
            comboBox5.Items.Clear();
            comboBox5.Items.Add("SEÇİNİZ");

            foreach (var cadde in _caddeler)
            {
                var hucreler = cadde.SelectNodes("td");
                var caddeIsmi = hucreler[0].InnerText + "-" + hucreler[1].InnerText;
                caddeIsmi = caddeIsmi.Replace("&nbsp;", " ");
                comboBox5.Items.Add(caddeIsmi);
            }
            comboBox5.SelectedIndex = 0;
            _mahalleKodu = mahallekodu;
            _mahalle = _mahalleler.yt[index].text;
        }

        private async void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            var index = comboBox5.SelectedIndex;
            if (index == 0) return;
            index = index - 1;
            var caddekodu = _caddeler[index].GetAttributeValue("id", "").Substring(1);

            var postData = $"fZwxqvPOpAViHEOXXVuXBaTd+2018072821lryxuVH5Y45L6Yb8Wo05CB46f1vrJzTCd1vDE8EiSLYLbaFSn0O0MhabkTx4t3A+Q==&t=dk&u={caddekodu}&term=";
            ScrapingBrowser browser = new ScrapingBrowser();
            browser.Encoding = Encoding.UTF8;
            WebPage homePage = null;
            homePage = await browser.NavigateToPageAsync(new Uri("http://adreskodu.dask.gov.tr/site-element/control/load.ashx"),
                HttpVerb.Post, postData);

            _binalar = homePage.Html.SelectNodes("//tbody/tr");
            comboBox6.Items.Clear();
            comboBox6.Items.Add("SEÇİNİZ");

            foreach (var bina in _binalar)
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
            var binakodu = _binalar[index].GetAttributeValue("id", "").Substring(1);

            var postData = $"fZwxqvPOpAViHEOXXVuXBaTd+2018072821lryxuVH5Y45L6Yb8Wo05CB46f1vrJzTCd1vDE8EiSLYLbaFSn0O0MhabkTx4t3A+Q==&t=ick&u={binakodu}&term=";
            ScrapingBrowser browser = new ScrapingBrowser();
            browser.Encoding = Encoding.UTF8;
            WebPage homePage = null;
            homePage = await browser.NavigateToPageAsync(new Uri("http://adreskodu.dask.gov.tr/site-element/control/load.ashx"),
                HttpVerb.Post, postData);

            _daireler = homePage.Html.SelectNodes("//tbody/tr");
            comboBox7.Items.Clear();
            comboBox7.Items.Add("SEÇİNİZ");

            foreach (var daire in _daireler)
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
            var dairekodu = _daireler[index].GetAttributeValue("id", "").Substring(1);

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

        private async void MainForm_Load(object sender, EventArgs e)
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
            _iller = JsonConvert.DeserializeObject<ReturnData>(homePage.Html.OuterHtml);
            comboBox1.Items.Clear();
            foreach (var city in _iller.yt)
            {
                comboBox1.Items.Add(city.text);
            }
            comboBox1.SelectedIndex = 0;

            //HtmlNode[] resultsLinks = resultsPage.Html.CssSelect("div.sb_tlst h3 a").ToArray();

            //WebPage blogPage = resultsPage.FindLinks(By.Text("romcyber blog | Just another WordPress site")).Single().Click();
        }


        private void buttonStart_Click(object sender, EventArgs e)
        {
            _adresListesi = new List<Adres>();
            _stopDownload = false;
            var t = new Task(delegate 
            {
                var caddeSayisi = _caddeler.Count;
                Invoke(new MethodInvoker(delegate
                {
                    progressBarCadde.Value = 0;
                    progressBarCadde.Maximum = caddeSayisi;
                }));

                ScrapingBrowser browser = new ScrapingBrowser();
                browser.Encoding = Encoding.UTF8;
                WebPage homePage = null;

                foreach (var cadde in _caddeler)
                {
                    if (_stopDownload) break;
                    Invoke(new MethodInvoker(delegate
                    {
                        progressBarCadde.Value += 1;
                    }));

                    var hucreler = cadde.SelectNodes("td");
                    var caddeIsmi = hucreler[0].InnerText + "-" + hucreler[1].InnerText;
                    caddeIsmi = caddeIsmi.Replace("&nbsp;", " ");

                    var caddekodu = cadde.GetAttributeValue("id", "").Substring(1);

                    var postData = $"fZwxqvPOpAViHEOXXVuXBaTd+2018072821lryxuVH5Y45L6Yb8Wo05CB46f1vrJzTCd1vDE8EiSLYLbaFSn0O0MhabkTx4t3A+Q==&t=dk&u={caddekodu}&term=";
                    homePage = browser.NavigateToPage(new Uri("http://adreskodu.dask.gov.tr/site-element/control/load.ashx"),
                        HttpVerb.Post, postData);

                    _binalar = homePage.Html.SelectNodes("//tbody/tr");
                    var binaSayisi = _binalar.Count;
                    Invoke(new MethodInvoker(delegate
                    {
                        progressBarBina.Value = 0;
                        progressBarBina.Maximum = binaSayisi;
                    }));

                    foreach (var bina in _binalar)
                    {
                        if (_stopDownload) break;

                        //var hucreler2 = bina.SelectNodes("td");
                        //var binaIsmi = hucreler2[0].InnerText + "-" + hucreler2[1].InnerText + "-" + hucreler2[2].InnerText + "-" + hucreler2[3].InnerText;
                        //binaIsmi = binaIsmi.Replace("&nbsp;", " ");
                        Invoke(new MethodInvoker(delegate
                        {
                            progressBarBina.Value += 1;
                        }));

                        var binakodu = bina.GetAttributeValue("id", "").Substring(1);

                        postData = $"fZwxqvPOpAViHEOXXVuXBaTd+2018072821lryxuVH5Y45L6Yb8Wo05CB46f1vrJzTCd1vDE8EiSLYLbaFSn0O0MhabkTx4t3A+Q==&t=ick&u={binakodu}&term=";
                        homePage = browser.NavigateToPage(new Uri("http://adreskodu.dask.gov.tr/site-element/control/load.ashx"),
                            HttpVerb.Post, postData);

                        _daireler = homePage.Html.SelectNodes("//tbody/tr");
                        var daireSayisi = _daireler.Count;
                        Invoke(new MethodInvoker(delegate
                        {
                            progressBarDaire.Value = 0;
                            progressBarDaire.Maximum = daireSayisi;
                        }));

                        foreach (var daire in _daireler)
                        {
                            if (_stopDownload) break;

                            Invoke(new MethodInvoker(delegate
                            {
                                progressBarDaire.Value += 1;
                            }));
                            //var hucreler3 = daire.SelectNodes("td");
                            //var daireIsmi = hucreler3[0].InnerText + "-" + hucreler3[1].InnerText;
                            //daireIsmi = daireIsmi.Replace("&nbsp;", " ");


                            //Debug.WriteLine("Adres Kodu: " + daire.GetAttributeValue("id", "").Substring(1));
                            var adres = new Adres();
                            adres.ilKodu = _ilKodu;
                            adres.il = _il;
                            adres.ilKodu = _ilKodu;
                            adres.ilKodu = _ilKodu;
                            adres.ilKodu = _ilKodu;
                            adres.ilKodu = _ilKodu;
                            _adresListesi.Add(adres);
                        }

                    }


                }
            });
            t.Start();
        }

        //await Task.Run(() => ...);

        private void buttonStop_Click(object sender, EventArgs e)
        {
            _stopDownload = true;
        }
    }
}
