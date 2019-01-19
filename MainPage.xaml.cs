using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private  ObservableCollection<Rate> currencyCollection;

        public ObservableCollection<Rate> CurrencyCollection { get => RatesViewModel.CurrencyRates(); set => currencyCollection = value; }
        public MainPage()
        {
            this.InitializeComponent();



            foreach (Rate rr in RatesViewModel.CurrencyRates())
            {
                Debug.WriteLine(rr.Currency + rr.Code + rr.Mid);
            }

            ///////



            Debug.WriteLine("Done...");

        }


    }

    public class Rate
    {
        public string Currency { get; set; }
        public string Code { get; set; }
        public float Mid { get; set; }
    }

    public class RatesVM
    {
        public RatesVM()
        {
        }

        private static ObservableCollection<Rate> currencyCollection;

        public static ObservableCollection<Rate> CurrencyCollection { get => RatesViewModel.CurrencyRates(); set => currencyCollection = value; }
    }
}
