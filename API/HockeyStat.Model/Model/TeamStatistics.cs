using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using HockeyStat.Model.Logic;

namespace HockeyStat.Model.Model
{
    public class TeamStatistics
    {
        public Season Season { get; set; }

        public Team Team { get; set; }

        public int Points { get; set; }

        public float PointsPerGame { get; set; }

        public int GoalsScored { get; set; }

        public float GoalsScoredPerGame { get; set; }

        public int GoalsScoredAgainst { get; set; }

        public float GoalsScoredAgainstPerGame { get; set; }

        public List<Game> GamesPlayed { get; set; }

        public int Wins { get; set; }

        public int OTWins { get; set; }

        public int PSWins { get; set; }

        public int Losses { get; set; }

        public int OTLosses { get; set; }

        public int PSLosses { get; set; }

        public float WinsPercent { get; set; }

        public float OTWinsPercent { get; set; }

        public float PSWinsPercent { get; set; }

        public float LossesPercent { get; set; }

        public float OTLossesPercent { get; set; }

        public float PSLossesPercent { get; set; }

        public TeamStatistics(Season season, Model.Team team)
        {
            this.Season = season;
            this.Team = team;
            this.GamesPlayed = new List<Game>();
            this.GoalsScored = 0;
            this.GoalsScoredPerGame = 0;
            this.GoalsScoredAgainst = 0;
            this.GoalsScoredAgainstPerGame = 0;
            this.Wins = 0;
            this.OTWins = 0;
            this.PSWins = 0;
            this.Losses = 0;
            this.OTLosses = 0;
            this.PSLosses = 0;
            this.WinsPercent = 0;
            this.OTWinsPercent = 0;
            this.PSWinsPercent = 0;
            this.LossesPercent = 0;
            this.OTLossesPercent = 0;
            this.PSLossesPercent = 0;
            this.Points = 0;
            this.PointsPerGame = 0;
        }

        public void AddGame(Game game)
        {
            this.GamesPlayed.Add(game);
            ScoreCalculation scoreCalculation = new ScoreCalculation(game);
            if (game.HomeTeam.ID == this.Team.ID)
            {
                this.AddScore(scoreCalculation.CalculateHomeTeamScore());
            }
            else
            {
                this.AddScore(scoreCalculation.CalculateGuestTeamScore());
            }
        }

        private void AddScore(Score score)
        {
            this.Points = this.Points + score.Points;
            this.GoalsScored = this.GoalsScored + score.Goals;
            this.GoalsScoredAgainst = this.GoalsScoredAgainst + score.GoalsAgainst;
            switch (score.Result)
            {
                case EScoreResult.Win:
                    this.Wins++;
                    break;
                case EScoreResult.OTWin:
                    this.OTWins++;
                    break;
                case EScoreResult.PSWin:
                    this.PSWins++;
                    break;
                case EScoreResult.Loss:
                    this.Losses++;
                    break;
                case EScoreResult.OTLoss:
                    this.OTLosses++;
                    break;
                case EScoreResult.PSLoss:
                    this.PSLosses++;
                    break;
            }
            this.CalculatePerGameValues();
            this.CalculatePercentValues();
        }

        private void CalculatePerGameValues()
        {
            this.GoalsScoredPerGame = this.CalculatePerGameValue(this.GoalsScored, 1);
            this.GoalsScoredAgainstPerGame = this.CalculatePerGameValue(this.GoalsScoredAgainst, 1);
            this.PointsPerGame = this.CalculatePerGameValue(this.Points, 1);
        }

        private void CalculatePercentValues()
        {
            this.WinsPercent = this.CalculatePerGameValue(this.Wins, 2) * 100;
            this.LossesPercent = this.CalculatePerGameValue(this.Losses, 2) * 100;
            this.OTWinsPercent = this.CalculatePerGameValue(this.OTWins, 2) * 100;
            this.OTLossesPercent = this.CalculatePerGameValue(this.OTLosses, 2) * 100;
            this.PSWinsPercent = this.CalculatePerGameValue(this.PSWins, 2) * 100;
            this.PSLossesPercent = this.CalculatePerGameValue(this.PSLosses, 2) * 100;
        }

        private float CalculatePerGameValue(int value, int decimals)
        {
            float perGameValue = 0F;
            if (this.GamesPlayed.Count > 0)
            {
                perGameValue = (float)Math.Round((float)value / (float)this.GamesPlayed.Count, decimals, MidpointRounding.AwayFromZero);
            }
            return perGameValue;
        }
    }
}
