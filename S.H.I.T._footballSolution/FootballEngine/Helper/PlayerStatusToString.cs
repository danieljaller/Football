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
                    return "Tillgänglig";
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
