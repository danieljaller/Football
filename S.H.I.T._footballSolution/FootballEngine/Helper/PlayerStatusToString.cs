using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FootballEngine.Domain.Entities.Player;

namespace FootballEngine.Helper
{
    public static class PlayerStatusToString
    {
        public static string ToSwedishString(this Status status)
        {
            switch (status)
            {
                case Status.Available:
                    return "Spelbar";
                case Status.Suspended:
                    return "Avstängd";
                case Status.Injured:
                    return "Skadad";
                case Status.NationalTeam:
                    return "Landslagsuppdrag";
                case Status.Other:
                    return "Övrig frånvaro";
                default:
                    return "Okänd status";
            }
        }
    }
}
