﻿using FootballEngine.Domain.Entities;
using FootballEngine.Domain.ValueObjects;
using FootballEngine.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using FootballEngine.Factories;

namespace AdminApp
{
    /// <summary>
    /// Interaction logic for PickedSeriesPage.xaml
    /// </summary>
    public partial class NewSeriesPage : Page
    {
        HashSet<Team> teamList = new HashSet<Team> { };
        bool teamsAreValid;
        bool nameIsValid;
        bool dateIsValid;

        public NewSeriesPage()
        {
            InitializeComponent();
            teamsList.ItemsSource = ServiceLocator.Instance.TeamService.GetAll();
            serieDatePicker.BlackoutDates.AddDatesInPast();
        }

        private void NewTeamButton_Click(object sender, RoutedEventArgs e)
        {

            NavigationService.Navigate(new Uri("NewTeamPage.xaml", UriKind.Relative));
        }

        private void CreateMatchScheduleButton_Click(object sender, RoutedEventArgs e)
        {
            var teamIds = teamList.Select(x => x.Id).ToList();
            var matchSchedule = MatchTableFactory.CreateMatchTable(teamList, Convert.ToDateTime(serieDatePicker.SelectedDate));

            foreach (Match match in matchSchedule)
                ServiceLocator.Instance.MatchService.Add(match);

            newSerieFrame.Content = new CreateSchedulePage(matchSchedule.Select(ms => ms.Id).ToHashSet(), serieName.Text, teamList);
        }

        private void teamCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var team = ServiceLocator.Instance.TeamService.GetBy(((CheckBox)sender).Content.ToString());
            teamList.Add(team);
            teamsCheckedList.ItemsSource = teamList;
            if (teamList.Count == 16)
            {
                teamsAreValid = true;
            }
            if (teamList.Count > 16)
            {
                teamsAreValid = false;
            }
            teamsCheckedList.Items.Refresh();
            ToggleCreateMatchScheduleButton();
        }

        private void teamCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            var team = ServiceLocator.Instance.TeamService.GetBy(((CheckBox)sender).Content.ToString());
            teamList.Remove(team);
            teamsCheckedList.ItemsSource = teamList;
            if (teamList.Count < 16)
            {
                teamsAreValid = false;
            }
            if (teamList.Count == 16)
            {
                teamsAreValid = true;
            }
            teamsCheckedList.Items.Refresh();
            ToggleCreateMatchScheduleButton();
        }

        private void serieName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(serieName.Text))
            {
                nameIsValid = false;
            }

            else
            {
                GeneralName result;
                if (GeneralName.TryParse(serieName.Text, out result))
                    nameIsValid = true;
                else
                    nameIsValid = false;
            }
            ToggleCreateMatchScheduleButton();
        }

        private void serieDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (serieDatePicker.SelectedDate == null)
            {
                dateIsValid = false;
            }
            else
            {
                dateIsValid = true;
            }
            ToggleCreateMatchScheduleButton();
        }

        private void ToggleCreateMatchScheduleButton()
        {
            if (teamsAreValid && nameIsValid && dateIsValid)
            {
                CreateMatchScheduleButton.IsEnabled = true;
            }
            else
            {
                CreateMatchScheduleButton.IsEnabled = false;
            }
        }
        private void AutoFill()
        {
            teamList = ServiceLocator.Instance.TeamService.GetAll().Take(16).ToHashSet();
            teamsCheckedList.ItemsSource = teamList;
            serieName.Text = "AutoSerie";
            serieDatePicker.SelectedDate = DateTime.Today.AddDays(2);

            teamsAreValid = true;
            nameIsValid = true;
            dateIsValid = true;
            ToggleCreateMatchScheduleButton();
        }

        private void AutoFillButton_Click(object sender, RoutedEventArgs e)
        {
            AutoFill();
        }
    }
}
