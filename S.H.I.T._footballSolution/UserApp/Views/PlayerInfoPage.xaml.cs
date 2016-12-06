﻿using FootballEngine.Domain.Entities;
using FootballEngine.Helper;
using FootballEngine.Services;
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
            DataContext = _selectedPlayer;
        }
    }
}
