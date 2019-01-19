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

    public class DateRates
    {
        public string EffectiveDate { get; set; }
    }

    class LatestDatesViewModel
    {

        static ObservableCollection<DateRates> currencyWithDateCollection = new ObservableCollection<DateRates>();

        public LatestDatesViewModel()
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

        public static IEnumerable<DateRates> getAllCurrenciesWithDate()
        {
            string nbp = "http://api.nbp.pl/api/exchangerates/tables/A/last/10/?format=xml";
            string xml = GetPageData(nbp);

            XDocument doc = XDocument.Parse(xml);
            IEnumerable<DateRates> rates = from r in
                                       doc.Descendants("ExchangeRatesTable")
                                           select new DateRates()
                                           {
                                               EffectiveDate = (string)r.Element("EffectiveDate")
                                           };

            return rates;
        }

        public static ObservableCollection<string> DateCurrencyRates()
        {
            ObservableCollection<string> dates = new ObservableCollection<string>();
            foreach (DateRates item in getAllCurrenciesWithDate())
            {
                dates.Add(item.EffectiveDate);
            }

            return dates;
        }
    }
}
