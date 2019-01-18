using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPM_Proj
{
    class Rates
    {
        public Rates()
        {
        }

        public static List<Rate> CurrencyRates(IEnumerable<Rate> rates)
        {
            return rates.ToList<Rate>();
        }
    }
}
