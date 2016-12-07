using FootballEngine.Domain.Entities;
using FootballEngine.Domain.ValueObjects;
using FootballEngine.Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace AdminApp
{
    public partial class AddEvent : Window
    {
        public Event Result { get; set; }
        public MatchMinute TimeOfEvent;
        private List<Player> PlayerList;
        private List<Exchange> Exchanges;
        private int MatchLength;

        public AddEvent(ObservableCollection<Guid> lineup, ObservableCollection<Exchange> exchanges)
            : this(90, lineup, exchanges)
        {
        }

        public AddEvent(int matchLength, ObservableCollection<Guid> lineup, ObservableCollection<Exchange> exchanges)
        {
            InitializeComponent();
            Exchanges = exchanges.ToList();
            MatchLength = matchLength;
            List<Player> allPlayers = ServiceLocator.Instance.PlayerService.GetAll().ToList();
            PlayerList = allPlayers.Where(p => (lineup.Contains(p.Id) || exchanges.Select(ex => ex.PlayerInId).Contains(p.Id))).ToList();
            playerListbox.ItemsSource = PlayerList;
        }

        public AddEvent(List<MatchMinute> matchMinutes, ObservableCollection<Guid> lineup, ObservableCollection<Exchange> exchanges)
        {
            InitializeComponent();
            var allPlayers = ServiceLocator.Instance.PlayerService.GetAll();
            PlayerList = allPlayers.Where(p => (lineup.Contains(p.Id) || exchanges.Select(ex => ex.PlayerInId).Contains(p.Id))).ToList();
            playerListbox.ItemsSource = PlayerList;
            timeBox.ItemsSource = matchMinutes;
        }

        private void playerListbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Player selectedPlayer = new Player();
            selectedPlayer = (Player)playerListbox.SelectedItem;
            List<Guid> PlayerInIdsList = new List<Guid>(Exchanges.Select(ex => ex.PlayerInId).ToList());
            List<Guid> PlayerOutIdsList = new List<Guid>(Exchanges.Select(ex => ex.PlayerOutId).ToList());
            List<MatchMinute> minutes = new List<MatchMinute>();

            for (int i = 1; i <= MatchLength; i++)
            {
                MatchMinute min = new MatchMinute();
                min.Value = i;
                minutes.Add(min);
            }

            if (PlayerInIdsList.Contains(selectedPlayer.Id))
            {
                Exchange activeExchange = Exchanges.Find(ex => ex.PlayerInId == selectedPlayer.Id);
                minutes = minutes.Where(m => m.Value > activeExchange.TimeOfExchange.Value).ToList();
            }

            if (PlayerOutIdsList.Contains(selectedPlayer.Id))
            {
                Exchange activeExchange = Exchanges.Find(ex => ex.PlayerOutId == selectedPlayer.Id);
                minutes = minutes.Where(m => m.Value < activeExchange.TimeOfExchange.Value).ToList();
            }

            timeBox.ItemsSource = minutes;
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            Player player = (Player)playerListbox.SelectedItem;
            TimeOfEvent = (MatchMinute)timeBox.SelectedItem;
            Result = new Event(player.Id, TimeOfEvent);
            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void timeBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            timeBox.IsDropDownOpen = false;
        }
    }
}