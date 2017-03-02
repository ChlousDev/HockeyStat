using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HockeyStat.Model.DataAccess;
using HockeyStat.Model.Model;

namespace HockeyStat.Model.Logic
{
    public class TeamComparator
    {
        private Team team1;
        private Team team2;
        private Season season;
        private HockeyStatDataAccess dataAccess;

        public TeamComparator(Season season, Team team1, Team team2, HockeyStatDataAccess dataAccess)
        {
                   this.season = season;
            this.team1 = team1;
            this.team2 = team2;
            this.dataAccess = dataAccess;
        }

        public TeamComparison CompareTeams()
        {
            List<Game> games = this.dataAccess.LoadGamesOfTeams(this.season.ID, this.team1.ID, this.team2.ID);
            TeamComparison teamComparision = new TeamComparison(this.season, this.team1, this.team2);
            foreach (Game game in games)
            {
                teamComparision.AddGame(game);
            }
            return teamComparision;
        }
        
    }
}
