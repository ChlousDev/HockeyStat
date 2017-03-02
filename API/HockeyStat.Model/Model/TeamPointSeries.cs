using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HockeyStat.Model.Model
{
    public class TeamPointSeries
    {
        private const int NumberOfGamesPerSeason = 50;
        private const float pointsNeededForEighthPlace = 70.0f;

        public Team Team { get; set; }

        public float[] Points { get; set; }

        public TeamPointSeries(Team team)
        {
            this.Team = team;
            this.Points = new float[TeamPointSeries.NumberOfGamesPerSeason + 1];
        }

        public static TeamPointSeries GetEighthPlaceSeries()
        {
            TeamPointSeries eighthPlaceSeries = new TeamPointSeries(null);
            for (int i = 0; i <= TeamPointSeries.NumberOfGamesPerSeason; i++)
            {
                eighthPlaceSeries.Points[i] = (float)Math.Round((TeamPointSeries.pointsNeededForEighthPlace / TeamPointSeries.NumberOfGamesPerSeason) * i, 1, MidpointRounding.AwayFromZero);
            }
            return eighthPlaceSeries;
        }

        public void AddPointsOfGame(int game, float points)
        {
            float totalPoints = points;
            if ((game > 0) && (game <= TeamPointSeries.NumberOfGamesPerSeason))
            {
                totalPoints = this.Points.ElementAt(game) + points;
                for (int i = game; i <= TeamPointSeries.NumberOfGamesPerSeason; i++)
                {
                    this.Points[i] = totalPoints;
                }
            }
        }
    }
}
