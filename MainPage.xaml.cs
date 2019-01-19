using Microsoft.Toolkit.Uwp.UI.Controls;
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
            //currencyGridUWP;
            currencyGridUWP.SelectedItem = null;
            currencyGridUWP.SelectionChanged += currencyGridUWP_CellSelected;
        }


        private void currencyGridUWP_CellSelected(object sender, SelectionChangedEventArgs e)
        {
            Debug.Write(((Rate)e.AddedItems[0]).Currency);
            this.Frame.Navigate(typeof(HistoryPage), ((Rate)e.AddedItems[0]));
        }

    }
}
