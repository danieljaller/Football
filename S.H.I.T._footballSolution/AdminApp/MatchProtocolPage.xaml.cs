using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AdminApp
{
    /// <summary>
    /// Interaction logic for MatchProtocolPage.xaml
    /// </summary>
    public partial class MatchProtocolPage : Page
    {
        public MatchProtocolPage()
        {
            InitializeComponent();
            matchDatePicker.DisplayDateStart = DateTime.Today;
        }

        private void removeGoalHome_Click(object sender, RoutedEventArgs e)
        {

        }

        private void addGoalHome_Click(object sender, RoutedEventArgs e)
        {
            var addEventWindow = new AddEvent();
            var addEvent = addEventWindow.ShowDialog();
        }

        private void addGoalAway_Click(object sender, RoutedEventArgs e)
        {
            var addEventWindow = new AddEvent();
            var addEvent = addEventWindow.ShowDialog();
        }

        private void removeGoalAway_Click(object sender, RoutedEventArgs e)
        {

        }

        private void removeAssistHome_Click(object sender, RoutedEventArgs e)
        {

        }

        private void addAssistHome_Click(object sender, RoutedEventArgs e)
        {
            var addEventWindow = new AddEvent();
            var addEvent = addEventWindow.ShowDialog();
        }

        private void addAssistAway_Click(object sender, RoutedEventArgs e)
        {
            var addEventWindow = new AddEvent();
            var addEvent = addEventWindow.ShowDialog();
        }

        private void removeAssistAway_Click(object sender, RoutedEventArgs e)
        {

        }

        private void removeRedCardsHome_Click(object sender, RoutedEventArgs e)
        {

        }

        private void addRedCardsHome_Click(object sender, RoutedEventArgs e)
        {
            var addEventWindow = new AddEvent();
            var addEvent = addEventWindow.ShowDialog();
        }

        private void addRedCardsAway_Click(object sender, RoutedEventArgs e)
        {
            var addEventWindow = new AddEvent();
            var addEvent = addEventWindow.ShowDialog();
        }

        private void removeRedCardsAway_Click(object sender, RoutedEventArgs e)
        {

        }

        private void removeYellowCardsHome_Click(object sender, RoutedEventArgs e)
        {

        }

        private void addYellowCardsHome_Click(object sender, RoutedEventArgs e)
        {
            var addEventWindow = new AddEvent();
            var addEvent = addEventWindow.ShowDialog();
        }

        private void addYellowCardsAway_Click(object sender, RoutedEventArgs e)
        {
            var addEventWindow = new AddEvent();
            var addEvent = addEventWindow.ShowDialog();
        }

        private void removeYellowCardsAway_Click(object sender, RoutedEventArgs e)
        {

        }

        private void removePlayerHome_Click(object sender, RoutedEventArgs e)
        {

        }

        private void addPlayerHome_Click(object sender, RoutedEventArgs e)
        {
            var addPlayerWindow = new AddPlayerWindow();
            var addExchange = addPlayerWindow.ShowDialog();
        }

        private void addPlayerAway_Click(object sender, RoutedEventArgs e)
        {
            var addExchangeWindow = new AddPlayerWindow();
            var addExchange = addExchangeWindow.ShowDialog();
        }

        private void removePlayerAway_Click(object sender, RoutedEventArgs e)
        {

        }

        private void removeExchangeHome_Click(object sender, RoutedEventArgs e)
        {

        }

        private void addExchangeHome_Click(object sender, RoutedEventArgs e)
        {
            var addExchangeWindow = new AddExchangeWindow();
            var addExchange = addExchangeWindow.ShowDialog();
        }

        private void addExchangeAway_Click(object sender, RoutedEventArgs e)
        {
            var addExchangeWindow = new AddExchangeWindow();
            var addExchange = addExchangeWindow.ShowDialog();
        }

        private void removeExchangeAway_Click(object sender, RoutedEventArgs e)
        {

        }

        
    }
}
