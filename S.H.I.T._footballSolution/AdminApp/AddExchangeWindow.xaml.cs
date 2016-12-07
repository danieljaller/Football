using FootballEngine.Domain.Entities;
using FootballEngine.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using FootballEngine.Helper;

namespace AdminApp
{
    /// <summary>
    /// Interaction logic for AddExchangeWindow.xaml
    /// </summary>
    public partial class AddExchangeWindow : Window
    {
        int LastMinute, FirstMinute;
        public Exchange Result { get; set; }
        public MatchMinute TimeOfEvent;
        Player PlayerOut;
        Player PlayerIn;
        ObservableCollection<Event> Goals, Assists, RedCards, YellowCards;

        public AddExchangeWindow(ObservableCollection<Guid> lineup, IEnumerable<Guid> playerOutIds, IEnumerable<Guid> playerInIds,
                                ObservableCollection<Event> goals, ObservableCollection<Event> assists, ObservableCollection<Event> redCards, ObservableCollection<Event> yellowCards)
            :this(lineup, 90, playerOutIds, playerInIds, goals, assists, redCards, yellowCards)
        { }

        public AddExchangeWindow(ObservableCollection<Guid> lineup, int matchLength, IEnumerable<Guid> playerOutIds, IEnumerable<Guid> playerInIds, 
                                ObservableCollection<Event> goals, ObservableCollection<Event> assists, ObservableCollection<Event> redCards, ObservableCollection<Event> yellowCards)
        {
            Goals = goals;
            Assists = assists;
            RedCards = redCards;
            YellowCards = yellowCards;
            InitializeComponent();
            IEnumerable<Player> allPlayers = ServiceLocator.Instance.PlayerService.GetAll();
            List<Player> activePlayers = new List<Player>();
            List<Player> availablePlayers = new List<Player>();
            activePlayers = allPlayers.Where(p => (lineup.Contains(p.Id) || playerInIds.Contains(p.Id))
                                                                && !playerOutIds.Contains(p.Id)).ToList();
            availablePlayers = allPlayers.Where(p => !lineup.Contains(p.Id) && !playerInIds.Contains(p.Id) && p.Playable).ToList();

            playerOutListBox.ItemsSource = activePlayers;
            playerInListBox.ItemsSource = availablePlayers;
            FirstMinute = 1;
            LastMinute = matchLength;

            SetTimeBox(FirstMinute, LastMinute);
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            MatchMinute.TryParse(timeBox.SelectedIndex + 1, out TimeOfEvent);
            Result = new Exchange(PlayerOut.Id, PlayerIn.Id, TimeOfEvent);
            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void playerOutListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PlayerOut = (Player)playerOutListBox.SelectedItem;
            CheckForEventsOut(out FirstMinute);
            SetTimeBox(FirstMinute, LastMinute);
        }

        private void playerInListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PlayerIn = (Player)playerInListBox.SelectedItem;
        }

        private void timeBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            timeBox.IsDropDownOpen = false;
        }

        private void CheckForEventsOut(out int firstMinute)
        {
            Event lastEvent = new Event();
            if (PlayerOut != null)
            {
                List<Event> allEvents = Goals.Concat(Assists.Concat(RedCards.Concat(YellowCards))).ToList();
                lastEvent = allEvents.OrderByDescending(x => x.TimeOfEvent.Value).ToList().Find(x => x.PlayerId == PlayerOut.Id);
            }
            if (lastEvent != null)
            {
                firstMinute = lastEvent.TimeOfEvent.Value;
            }
            else
            {
                firstMinute = FirstMinute;
            }
        }

        private void SetTimeBox(int first, int last)
        {
            List<string> minutes = new List<string>();
            for (int i = first; i <= last; i++)
            {
                minutes.Add(i.ToString());
            }
            
            timeBox.ItemsSource = minutes;
        }
    }
}
