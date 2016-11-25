﻿using FootballEngine.Domain.Entities;
using FootballEngine.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace AdminApp.Converters
{
    public class SearchResultToStringConverter : IValueConverter
    {
        TeamService teamService;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.GetType() == typeof(Player))
            {
                var player = (Player)value;
                return player.FullName;
            }
            if (value.GetType() == typeof(Team))
            {
                var team = (Team)value;
                return team.Name;
            }
            if (value.GetType() == typeof(Serie))
            {
                var serie = (Serie)value;
                return serie.Name;
            }
            if (value.GetType() == typeof(Match))
            {
                var match = (Match)value;
                teamService = new TeamService();
                var teamHome = teamService.GetBy(match.HomeTeamId);
                var teamVisitor = teamService.GetBy(match.VisitorTeamId);
                return $"{teamHome.Name} - {teamVisitor.Name}";
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}