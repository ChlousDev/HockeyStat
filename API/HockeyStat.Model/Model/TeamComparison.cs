using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HockeyStat.Model.Logic;

namespace HockeyStat.Model.Model
{
    public class TeamComparison
    {
        public Season Season { get; set; }

        public Team Team1 { get; set; }

        public Team Team2 { get; set; }

        public List<Game> GamesPlayed { get; set; }

        public int PointsTeam1 { get; set; }

        public int PointsTeam2 { get; set; }

        public int WinsTeam1 { get; set; }

        public int OTWinsTeam1 { get; set; }

        public int PSWinsTeam1 { get; set; }

        public int WinsTeam2 { get; set; }

        public int OTWinsTeam2 { get; set; }

        public int PSWinsTeam2 { get; set; }

        public int GoalsTeam1 { get; set; }

        public int GoalsTeam2 { get; set; }


        public TeamComparison(Season season, Team team1, Team team2)
        {
            this.Season = season;
            this.Team1 = team1;
            this.Team2 = team2;
            this.GamesPlayed = new List<Game>();
            this.PointsTeam1 = 0;
            this.PointsTeam2 = 0;
            this.WinsTeam1 = 0;
            this.WinsTeam2 = 0;
            this.OTWinsTeam1 = 0;
            this.OTWinsTeam2 = 0;
            this.GoalsTeam1 = 0;
            this.GoalsTeam2 = 0;
        }

        public void AddGame(Game game)
        {
            this.GamesPlayed.Add(game);
            ScoreCalculation scoreCalculation = new ScoreCalculation(game);
            this.AddScore(scoreCalculation.CalculateHomeTeamScore());
            this.AddScore(scoreCalculation.CalculateGuestTeamScore());
        }

        private void AddScore(Score score)
        {
            if (score.Team.ID == this.Team1.ID)
            {
                this.PointsTeam1 = this.PointsTeam1 + score.Points;
                this.GoalsTeam1 = this.GoalsTeam1 + score.Goals;
                switch (score.Result)
                {
                    case EScoreResult.Win:
                        this.WinsTeam1++;
                        break;
                    case EScoreResult.OTWin:
                        this.OTWinsTeam1++;
                        break;
                    case EScoreResult.PSWin:
                        this.PSWinsTeam1++;
                        break;
                }
            }
            if (score.Team.ID == this.Team2.ID)
            {
                this.PointsTeam2 = this.PointsTeam2 + score.Points;
                this.GoalsTeam2 = this.GoalsTeam2 + score.Goals;
                switch (score.Result)
                {
                    case EScoreResult.Win:
                        this.WinsTeam2++;
                        break;
                    case EScoreResult.OTWin:
                        this.OTWinsTeam2++;
                        break;
                    case EScoreResult.PSWin:
                        this.PSWinsTeam2++;
                        break;
                }
            }
        }
    }
}
