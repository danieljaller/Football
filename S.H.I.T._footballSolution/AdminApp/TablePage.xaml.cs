using FootballEngine.Domain.Entities;
using FootballEngine.Helper;
using System.Windows.Controls;

namespace AdminApp
{
    /// <summary>
    /// Interaction logic for TablePage.xaml
    /// </summary>
    public partial class TablePage : Page
    {
        public TablePage()
        { }
        public TablePage(Serie selectedSerie)
        {
            InitializeComponent();
            SetTableData(selectedSerie);
        }

        private void SetTableData(Serie selectedSerie)
        {
            if (selectedSerie != null)
            {
                tableStatsListbox.ItemsSource = ServiceLocator.Instance.TeamService.GetAllTeamsBySerie(selectedSerie.Id);
            }

        }        
    }
}
