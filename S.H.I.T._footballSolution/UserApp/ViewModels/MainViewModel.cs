using FootballEngine.Helper;
using FootballEngine.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UserApp.Utilities;

namespace UserApp.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
                return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        private string _searchTextBox;
        public string SearchTextBox
        {
            get { return _searchTextBox; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    SearchResultItems = null;
                    SelectedSearchResultItem = null;
                }
                else
                {
                    SearchResultItems = new ObservableCollection<object>(ServiceLocator.Default.SearchService.Search(value, matchDateSearch, playerSearch, serieSearch, teamSearch, true).Take(10).ToList().Take(10).ToList());
                }
                SetField(ref _searchTextBox, value);
            }
        }
        private ObservableCollection<object> _searchResultItems;
        public ObservableCollection<object> SearchResultItems
        {
            get { return _searchResultItems; }
            set { SetField(ref _searchResultItems, value); }
        }
        private object _selectedSearchResultItem;
        public object SelectedSearchResultItem
        {
            get { return _selectedSearchResultItem; }
            set
            {
                SetField(ref _selectedSearchResultItem, value);
            }
        }
        private bool matchDateSearch, playerSearch, serieSearch, teamSearch;
        public ICommand ToogleMatchDateSearchCommand { get; set; }
        public ICommand TooglePlayerSearchCommand { get; set; }
        public ICommand ToogleSerieSearchCommand { get; set; }
        public ICommand ToogleTeamSearchCommand { get; set; }

        public MainViewModel()
        {
            LoadCommands();
        }

        private void LoadCommands()
        {
            ToogleMatchDateSearchCommand = new CustomCommand(ToogleMatchDateSearch, CanToogleMatchDateSearch);
            TooglePlayerSearchCommand = new CustomCommand(ToogleSerieSearch, CanTooglePlayerSearch);
            ToogleSerieSearchCommand = new CustomCommand(ToogleSerieSearch, CanToogleSerieSearch);
            ToogleTeamSearchCommand = new CustomCommand(ToogleTeamSearch, CanToogleTeamSearch);
        }

        

        public void ToogleMatchDateSearch(object obj)
        {
            matchDateSearch = (matchDateSearch) ? false : true;
            //if (matchDateSearch)
            //    matchDateSearch = false;
            //else
            //    matchDateSearch = true;
        }
        private bool CanToogleMatchDateSearch(object obj) { return true; }

        public void TooglePlayerSearch()
        {
            if (playerSearch)
                playerSearch = false;
            else
                playerSearch = true;
        }
        private bool CanTooglePlayerSearch(object obj) { return true; }

        public void ToogleSerieSearch(object obj)
        {
            if (serieSearch)
                serieSearch = false;
            else
                serieSearch = true;
        }
        private bool CanToogleSerieSearch(object obj) { return true; }

        public void ToogleTeamSearch(object obj)
        {
            if (teamSearch)
                teamSearch = false;
            else
                teamSearch = true;
        }
        private bool CanToogleTeamSearch(object obj) { return true; }
    }
}
