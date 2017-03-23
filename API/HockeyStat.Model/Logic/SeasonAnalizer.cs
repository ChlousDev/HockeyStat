using HockeyStat.Model.DataAccess;
using HockeyStat.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HockeyStat.Model.Logic
{
    public class SeasonAnalizer
    {
        private Season season;
        private HockeyStatDataAccess dataAccess;

        public SeasonAnalizer(Season season, HockeyStatDataAccess dataAccess)
        {
            this.season = season;
            this.dataAccess = dataAccess;
        }

        public SeasonStatistics CreateStatistics()
        {
            List<Game> games = this.dataAccess.LoadGamesOfSeason(this.season.ID);
            SeasonStatistics seasonStatistics = new SeasonStatistics(this.season);
            foreach (Game game in games)
            {
                seasonStatistics.AddGame(game);
            }
            return seasonStatistics;
        }
    }
}
