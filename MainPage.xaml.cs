using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace IPM_Proj
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            string nbp = "http://api.nbp.pl/api/exchangerates/tables/a/?format=xml";
            string xml;

            Debug.WriteLine("Downloading...");

            xml = GetPageData(nbp);

            Debug.WriteLine(xml);


            ////////
            ///
            //IEnumerable<Rate> rates = from customers in
            //                        XDocument.Parse(xml)
            //                                  .Descendants("Rate")
            //                            select customers.Element("Currency").Value;
            XDocument doc = XDocument.Parse(xml);
            IEnumerable<Rate> rates = from r in
                                       doc.Descendants("Rate")
                                      select new Rate()
                                      {
                                          Currency = (string)r.Element("Currency"),
                                          Code = (string)r.Element("Code"),
                                          Mid = (float)r.Element("Mid")
                                      };

            foreach (Rate rr in rates)
            {
                Debug.WriteLine(rr.Currency + rr.Code + rr.Mid);
            }

            ///////



            Debug.WriteLine("Done...");

        }

        public string GetPageData(string link)
        {
            HttpClient _client = new HttpClient() { Timeout = TimeSpan.FromSeconds(200) };
            HttpRequestMessage _request = new HttpRequestMessage(HttpMethod.Get, link);
            _request.Headers.Add("User-Agent", "Chrome/21.0.1180.89");
            _request.Headers.Add("Accept", "text/html");


            var readTask = _client.GetStringAsync(link);
            readTask.Wait();
            return readTask.Result;
        }
    }

    class Rate
    {
        public string Currency { get; set; }
        public string Code { get; set; }
        public float Mid { get; set; }
    }
}
