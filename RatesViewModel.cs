using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace IPM_Proj
{
    public class RatesViewModel
    {
        public RatesViewModel()
        {
        }

        private static ObservableCollection<Rate> currencyCollection;

        public static ObservableCollection<Rate> CurrencyCollection { get => CurrencyRates(); set => currencyCollection = value; }

        public static string GetPageData(string link)
        {
            HttpClient _client = new HttpClient() { Timeout = TimeSpan.FromSeconds(200) };
            HttpRequestMessage _request = new HttpRequestMessage(HttpMethod.Get, link);
            _request.Headers.Add("User-Agent", "Chrome/21.0.1180.89");
            _request.Headers.Add("Accept", "text/html");


            var readTask = _client.GetStringAsync(link);
            readTask.Wait();
            return readTask.Result;
        }

        public static IEnumerable<Rate> getAllCurrencies()
        {
            string nbp = "http://api.nbp.pl/api/exchangerates/tables/a/?format=xml";
            string xml = GetPageData(nbp);

            XDocument doc = XDocument.Parse(xml);
            IEnumerable<Rate> rates = from r in
                                       doc.Descendants("Rate")
                                      select new Rate()
                                      {
                                          Currency = (string)r.Element("Currency"),
                                          Code = (string)r.Element("Code"),
                                          Mid = (float)r.Element("Mid")
                                      };

            return rates;
        }

        public static ObservableCollection<Rate> CurrencyRates()
        {
            ObservableCollection<Rate> currencyCollection = new ObservableCollection<Rate>();
            foreach (var item in getAllCurrencies())
            {
                currencyCollection.Add(item);
            }
            return currencyCollection;
        }
    }
}
