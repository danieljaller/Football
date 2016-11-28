using FootballEngine.Domain.Entities;
using FootballEngine.Domain.ValueObjects;
using FootballEngine.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace AdminApp
{
    /// <summary>
    /// Interaction logic for MatchProtocol.xaml
    /// </summary>
    public partial class MatchProtocol : Window
    {
        MatchDate matchDate;
        MatchService matchService;
        TeamService teamService;
        PlayerService playerService;
        Match match;
        Team homeTeam;
        Team visitorTeam;
        int homeScore;
        int visitorScore;
        bool isPlayed;
        ObservableCollection<Event> homeGoals;
        ObservableCollection<Event> visitorGoals;
        ObservableCollection<Event> homeAssists;
        ObservableCollection<Event> visitorAssists;
        ObservableCollection<Event> homeRedCards;
        ObservableCollection<Event> visitorRedCards;
        ObservableCollection<Event> homeYellowCards;
        ObservableCollection<Event> visitorYellowCards;
        ObservableCollection<Guid> homeLineup;
        ObservableCollection<Guid> visitorLineup;
        ObservableCollection<Exchange> homeExchanges;
        ObservableCollection<Exchange> visitorExchanges;

        public MatchProtocol(Match _match)
        {
            match = _match;
            matchService = new MatchService();
            teamService = new TeamService();
            playerService = new PlayerService(teamService);
            InitializeComponent();
            convertListsToObjects();
            matchDateBlock.DataContext = match.Date;
        }

        private void convertListsToObjects()
        {         
            matchDate = match.Date;
            isPlayed = match.IsPlayed;
            homeTeam = teamService.GetBy(match.HomeTeamId);
            visitorTeam = teamService.GetBy(match.VisitorTeamId);
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
