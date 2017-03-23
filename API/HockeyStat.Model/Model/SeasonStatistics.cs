﻿using HockeyStat.Model.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HockeyStat.Model.Model
{
    public class SeasonStatistics
    {
        private const int NumberOfGamesPerSeason = 300;

        public Season Season { get; set; }

        public int GamesPlayed { get; set; }

        public float Completion { get; set; }

        public int GamesDecidedInNormalTime { get; set; }

        public float GamesDecidedInNormalTimePercent { get; set; }

        public int GamesDecidedInOverTime { get; set; }

        public float GamesDecidedInOverTimePercent { get; set; }

        public int GamesDecidedInPenaltyShots { get; set; }

        public float GamesDecidedInPenaltyShotsPercent { get; set; }

        public int HomeWins { get; set; }

        public float HomeWinsPercent { get; set; }

        public int GuestWins { get; set; }

        public float GuestWinsPercent { get; set; }

        public int GoalsScored { get; set; }

        public float GoalsScoredPerGame { get; set; }

        public SeasonStatistics(Season season)
        {
            this.Season = season;
            this.GamesPlayed = 0;
            this.Completion = 0;
            this.GamesDecidedInNormalTime = 0;
            this.GamesDecidedInNormalTimePercent = 0;
            this.GamesDecidedInOverTime = 0;
            this.GamesDecidedInOverTimePercent = 0;
            this.GamesDecidedInPenaltyShots = 0;
            this.GamesDecidedInPenaltyShotsPercent = 0;
            this.HomeWins = 0;
            this.HomeWinsPercent = 0;
            this.GuestWins = 0;
            this.GuestWinsPercent = 0;
            this.GoalsScored = 0;
            this.GoalsScoredPerGame = 0;
        }

        public void AddGame(Game game)
        {
            this.GamesPlayed++;
            this.GoalsScored = this.GoalsScored + game.HomeScore + game.GuestScore;
            ScoreCalculation scoreCalculation = new ScoreCalculation(game);
            Score score = scoreCalculation.CalculateHomeTeamScore();
            switch (score.Result)
            {
                case EScoreResult.Loss:
                    this.GuestWins++;
                    this.GamesDecidedInNormalTime++;
                    break;
                case EScoreResult.OTLoss:
                    this.GuestWins++;
                    this.GamesDecidedInOverTime++;
                    break;
                case EScoreResult.PSLoss:
                    this.GuestWins++;
                    this.GamesDecidedInPenaltyShots++;
                    break;
                case EScoreResult.Win:
                    this.HomeWins++;
                    this.GamesDecidedInNormalTime++;
                    break;
                case EScoreResult.OTWin:
                    this.HomeWins++;
                    this.GamesDecidedInOverTime++;
                    break;
                case EScoreResult.PSWin:
                    this.HomeWins++;
                    this.GamesDecidedInPenaltyShots++;
                    break;
            }
            this.Completion = SeasonStatistics.CalculateRelativeValue(this.GamesPlayed, SeasonStatistics.NumberOfGamesPerSeason, 2) * 100;
            this.GamesDecidedInNormalTimePercent = SeasonStatistics.CalculateRelativeValue(this.GamesDecidedInNormalTime, this.GamesPlayed, 2) * 100;
            this.GamesDecidedInOverTimePercent = SeasonStatistics.CalculateRelativeValue(this.GamesDecidedInOverTime, this.GamesPlayed, 2) * 100;
            this.GamesDecidedInPenaltyShotsPercent = SeasonStatistics.CalculateRelativeValue(this.GamesDecidedInPenaltyShots, this.GamesPlayed, 2) * 100;
            this.HomeWinsPercent = SeasonStatistics.CalculateRelativeValue(this.HomeWins, this.GamesPlayed, 2) * 100;
            this.GuestWinsPercent = SeasonStatistics.CalculateRelativeValue(this.GuestWins, this.GamesPlayed, 2) * 100;
            this.GoalsScoredPerGame = SeasonStatistics.CalculateRelativeValue(this.GoalsScored, this.GamesPlayed, 1);
        }

        private static float CalculateRelativeValue(int value, int totalValue, int decimals)
        {
            float perGameValue = 0F;
            if (totalValue > 0)
            {
                perGameValue = (float)Math.Round((float)value / (float)totalValue, decimals, MidpointRounding.AwayFromZero);
            }
            return perGameValue;
        }
    }
}
