using FootballEngine.Domain.Entities;
using FootballEngine.Domain.ValueObjects;
using FootballEngine.Helper;
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

namespace UserApp.Views
{
    /// <summary>
    /// Interaction logic for MatchProtocol.xaml
    /// </summary>
    public partial class MatchProtocol : Window
    {
        

        MatchDate matchDate;
        Match match;
        Team homeTeam, visitorTeam;
        int homeScore;
        int visitorScore;
        bool isPlayed;
        ObservableCollection<Event> homeGoals, visitorGoals, homeAssists, visitorAssists, homeRedCards, visitorRedCards, homeYellowCards
                                    , visitorYellowCards;
        ObservableCollection<Guid> homeLineup, visitorLineup;
        ObservableCollection<Exchange> homeExchanges, visitorExchanges;

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
