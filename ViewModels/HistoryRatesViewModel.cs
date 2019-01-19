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
    public class HistoryRate
    {
        public string No { get; set; }
        public string EffectiveDate { get; set; }
        public float Mid { get; set; }
    }

    class HistoryRatesViewModel
    {
        string historyCurrencyName;

        public string HistoryCurrencyName { get => this.historyCurrencyName; set => this.historyCurrencyName = value; }
        public static Rate RateToDisplay { get => rateToDisplay; set => rateToDisplay = value; }

        static string xml;
        static Rate rateToDisplay;
        public HistoryRatesViewModel()
        {

        }
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

        public static IEnumerable<HistoryRate> getAllCurrencies()
        {
            string nbp = "http://api.nbp.pl/api/exchangerates/rates/a/" + rateToDisplay.Code + "/2012-01-01/2012-01-31/?format=xml";
            if (xml == null)
            {
                xml = GetPageData(nbp);
            }

            XDocument doc = XDocument.Parse(xml);
            IEnumerable<HistoryRate> rates = from r in
                                       doc.Descendants("Rate")
                                      select new HistoryRate()
                                      {
                                          No = (string)r.Element("No"),
                                          EffectiveDate = (string)r.Element("EffectiveDate"),
                                          Mid = (float)r.Element("Mid")
                                      };

            return rates;
        }

        public static ObservableCollection<HistoryRate> HistoryCurrencyRates()
        {
            ObservableCollection<HistoryRate> currencyCollection = new ObservableCollection<HistoryRate>();
            foreach (var item in getAllCurrencies())
            {
                currencyCollection.Add(item);
            }
            return currencyCollection;
        }

        public static string getCurrencyName()
        {
            string nbp = "http://api.nbp.pl/api/exchangerates/rates/a/" + rateToDisplay.Code + "/2012-01-01/2012-01-31/?format=xml";
            if (xml == null)
            {
                xml = GetPageData(nbp);
            }

            XDocument doc = XDocument.Parse(xml);
            IEnumerable<string> name = from r in
                                       doc.Descendants("ExchangeRatesSeries")
                                      select (string)r.Element("Currency");

            return name.First();
        }

        public static string GetHistoryCurrencyName()
        {
            return getCurrencyName();
        }
    }
}
