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
                        ScoreCalculation scoreCalculation = new ScoreCalculation(game);
                        Score score = null;
                        if (game.HomeTeam.ID == team.ID)
                        {
                            score = scoreCalculation.CalculateHomeTeamScore();
                        }
                        else
                        {
                            score = scoreCalculation.CalculateGuestTeamScore();
                        }
                        teamPointSeries.AddPointsOfGame(currentGameNumber, score.Points);
                        currentGameNumber++;
                    }
                    teamPointSeriesList.Add(teamPointSeries);
                }
            }

            //Add entry for theoretical eighth place
            teamPointSeriesList.Add(TeamPointSeries.GetEighthPlaceSeries());

            return new TeamPointChart(this.season, teamPointSeriesList);
        }
        
    }
}
