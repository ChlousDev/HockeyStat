using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HockeyStat.Model.DataAccess;
using HockeyStat.Model.Model;

namespace HockeyStat.Model.Logic
{
    public class TeamPointChartDataProvider
    {

        private Season season;
        private HockeyStatDataAccess dataAccess;


        public TeamPointChartDataProvider(Season season, HockeyStatDataAccess dataAccess)
        {
            this.season = season;
            this.dataAccess = dataAccess;
        }

        public TeamPointChart GetTeamPointChart()
        {
            List<TeamPointSeries> teamPointSeriesList = new List<TeamPointSeries>();
            foreach (Team team in this.dataAccess.LoadTeams())
            {
                List<Game> games = this.dataAccess.LoadGamesOfSeasonForTeam(this.season.ID, team.ID);
                if (games.Count() > 0)
                {
                    TeamPointSeries teamPointSeries = new TeamPointSeries(team);
                    int currentGameNumber=1;
                    foreach (Game game in games.OrderBy(g=>g.Date))
                    {
                        teamPointSeries.AddPointsOfGame(currentGameNumber, TeamPointChartDataProvider.GetPoints(game, team));
                        currentGameNumber++;
                    }
                    teamPointSeriesList.Add(teamPointSeries);
                }
            }

            //Add entry for theoretical eighth place
            teamPointSeriesList.Add(TeamPointSeries.GetEighthPlaceSeries());

            return new TeamPointChart(this.season, teamPointSeriesList);
        }

        private static int GetPoints(Game game, Team team)
        {
            int points = 0;
            int scoreDifference = game.HomeScore - game.GuestScore;
            int OTScoreDifference = game.OTHomeScore - game.OTGuestScore;
            int PSScoreDifference = game.PSHomeScore - game.PSGuestScore;
            if(game.GuestTeam.ID==team.ID)
            {
                scoreDifference = scoreDifference * (-1);
                OTScoreDifference = OTScoreDifference * (-1);
                PSScoreDifference = PSScoreDifference * (-1);
            }
            if(scoreDifference>0)
            {
                points = 3;
            }
            else if(scoreDifference<0)
            {
                points = 0;
            }
            else if ((OTScoreDifference > 0) || (PSScoreDifference>0))
            {
                points = 2;
            }
            else if ((OTScoreDifference < 0) || (PSScoreDifference<0))
            {
                points = 1;
            }
            else
            {
                throw new Exception(string.Format("Inconsistent score for game (ID: {0}, Result {1}: {2} {3}:{4}; OT {5}:{6}; PS {7}:{8})",
                    game.ID, game.HomeTeam.ShortName, game.GuestTeam.ShortName, game.HomeScore, game.GuestScore, game.OTHomeScore,
                    game.OTGuestScore, game.PSHomeScore, game.PSGuestScore));
            }

            return points;
        }
    }
}
