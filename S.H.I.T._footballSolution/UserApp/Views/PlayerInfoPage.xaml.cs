using FootballEngine.Domain.Entities;
using System.Windows.Controls;

namespace UserApp.Views
{
    /// <summary>
    /// Interaction logic for PlayerInfoPage.xaml
    /// </summary>
    public partial class PlayerInfoPage : Page
    {
        public PlayerInfoPage()
        { InitializeComponent(); }

        public PlayerInfoPage(Player _selectedPlayer)
        {
            InitializeComponent();
            name.Text = $"{_selectedPlayer.FirstName} {_selectedPlayer.LastName}";
            DataContext = _selectedPlayer;
        }
    }
}