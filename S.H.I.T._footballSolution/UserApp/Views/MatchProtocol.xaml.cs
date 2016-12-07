using FootballEngine.Domain.Entities;
using FootballEngine.Domain.ValueObjects;
using FootballEngine.Helper;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace UserApp.Views
{
    /// <summary>
    /// Interaction logic for MatchProtocol.xaml
    /// </summary>
    public partial class MatchProtocol : Window
    {
        private MatchDate matchDate;
        private Match match;
        private Team homeTeam, visitorTeam;
        private int homeScore;
        private int visitorScore;
        private bool isPlayed;

        private ObservableCollection<Event> homeGoals, visitorGoals, homeAssists, visitorAssists, homeRedCards, visitorRedCards, homeYellowCards
                                    , visitorYellowCards;

        private ObservableCollection<Guid> homeLineup, visitorLineup;
        private ObservableCollection<Exchange> homeExchanges, visitorExchanges;

        public MatchProtocol(Match _match)
        {
            match = _match;
            InitializeComponent();
            convertListsToObjects();
            matchDateBlock.DataContext = match.Date;
        }

        private void convertListsToObjects()
        {
            matchDate = match.Date;
            homeTeam = ServiceLocator.Instance.TeamService.GetBy(match.HomeTeamId);
            visitorTeam = ServiceLocator.Instance.TeamService.GetBy(match.VisitorTeamId);
            homeScore = match.HomeGoals.Count();
            visitorScore = match.VisitorGoals.Count();
            homeGoals = new ObservableCollection<Event>(match.HomeGoals);
            visitorGoals = new ObservableCollection<Event>(match.VisitorGoals);
            homeAssists = new ObservableCollection<Event>(match.HomeAssists);
            visitorAssists = new ObservableCollection<Event>(match.VisitorAssists);
            homeRedCards = new ObservableCollection<Event>(match.HomeRedCards);
            visitorRedCards = new ObservableCollection<Event>(match.VisitorRedCards);
            homeYellowCards = new ObservableCollection<Event>(match.HomeYellowCards);
            visitorYellowCards = new ObservableCollection<Event>(match.VisitorYellowCards);
            homeLineup = new ObservableCollection<Guid>(match.HomeLineup);
            visitorLineup = new ObservableCollection<Guid>(match.VisitorLineup);
            homeExchanges = new ObservableCollection<Exchange>(match.HomeExchanges);
            visitorExchanges = new ObservableCollection<Exchange>(match.VisitorExchanges);
        }
    }
}