using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HockeyStat.Model.DataAccess;
using HockeyStat.Model.Model;

namespace HockeyStat.Model.Logic
{
    public class TeamAnalizer
    {
        private Team team;
        private Season season;
        private HockeyStatDataAccess dataAccess;

        public TeamAnalizer(Season season, Team team, HockeyStatDataAccess dataAccess)
        {
            this.season = season;
            this.team = team;
            this.dataAccess = dataAccess;
        }

        public TeamStatistics CreateStatistics()
        {
            List<Game> games = this.dataAccess.LoadGamesOfSeasonForTeam(this.season.ID, this.team.ID);
            TeamStatistics teamStatistics = new TeamStatistics(this.season, this.team);
            foreach (Game game in games)
            {
                teamStatistics.AddGame(game);
            }
            return teamStatistics;
        }
    }
}
