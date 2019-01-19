using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace IPM_Proj
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HistoryPage : Page
    {

        private ObservableCollection<HistoryRate> historyCurrencyCollection;

        public ObservableCollection<HistoryRate> HistoryCurrencyCollection { get => HistoryRatesViewModel.HistoryCurrencyRates(); set => historyCurrencyCollection = value; }

        Rate rateToDisplay;
        public HistoryPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            rateToDisplay = (Rate)e.Parameter;

            HistoryRatesViewModel.RateToDisplay = rateToDisplay;

            historyCurrencyNameUWP.Text = HistoryRatesViewModel.getCurrencyName();
            // parameters.Name
            // parameters.Text
            // ...
        }

        private void TextBlock_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }
    }


}
