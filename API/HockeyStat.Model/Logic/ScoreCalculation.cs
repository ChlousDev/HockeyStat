using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HockeyStat.Model.Model;

namespace HockeyStat.Model.Logic
{
    public class ScoreCalculation
    {
        private Game game;

        public ScoreCalculation(Game game)
        {
            this.game = game;
        }

        public Score CalculateHomeTeamScore()
        {
            Score score = new Score(this.game.HomeTeam);
            score.Goals = game.HomeScore + game.OTHomeScore;
            if (game.PSHomeScore > game.PSGuestScore)
            {
                score.Goals++;
            }       
            score.GoalsAgainst = game.GuestScore + game.OTGuestScore;
            if (game.PSGuestScore > game.PSHomeScore)
            {
                score.GoalsAgainst++;
            }
            if (game.HomeScore > game.GuestScore)
            {
                score.Points = 3;
                score.Result = EScoreResult.Win;
            }
            else if (game.HomeScore < game.GuestScore)
            {
                score.Result = EScoreResult.Loss;
            }
            else if (game.OTHomeScore > game.OTGuestScore) 
            {
                score.Points = 2;
                score.Result = EScoreResult.OTWin;
            }
            else if (game.OTHomeScore < game.OTGuestScore) 
            {
                score.Points = 1;
                score.Result = EScoreResult.OTLoss;
            }
            else if (game.PSHomeScore > game.PSGuestScore)
            {
                score.Points = 2;
                score.Result = EScoreResult.PSWin;
            }
            else if (game.PSHomeScore < game.PSGuestScore)
            {
                score.Points = 1;
                score.Result = EScoreResult.PSLoss;
            }
            else
            {
                throw new Exception(string.Format("Inconsistent score for game (ID: {0}, Result {1}: {2} {3}:{4}; OT {5}:{6}; PS {7}:{8})",
                    game.ID, game.HomeTeam.ShortName, game.GuestTeam.ShortName, game.HomeScore, game.GuestScore, game.OTHomeScore,
                    game.OTGuestScore, game.PSHomeScore, game.PSGuestScore));
            }
            return score;
        }

        public Score CalculateGuestTeamScore()
        {
            Score score = new Score(this.game.GuestTeam);
            score.Goals = game.GuestScore + game.OTGuestScore;
            if (game.PSGuestScore > game.PSHomeScore)
            {
                score.Goals++;
            }
            score.GoalsAgainst = game.HomeScore + game.OTHomeScore;
            if (game.PSHomeScore > game.PSGuestScore)
            {
                score.GoalsAgainst++;
            }
            
            if (game.GuestScore > game.HomeScore)
            {
                score.Points = 3;
                score.Result = EScoreResult.Win;
            }
            else if (game.GuestScore < game.HomeScore)
            {
                score.Result = EScoreResult.Loss;
            }
            else if (game.OTGuestScore > game.OTHomeScore) 
            {
                score.Points = 2;
                score.Result = EScoreResult.OTWin;
            }
            else if (game.OTGuestScore < game.OTHomeScore)
            {
                score.Points = 1;
                score.Result = EScoreResult.OTLoss;
            }
            else if (game.PSGuestScore > game.PSHomeScore)
            {
                score.Points = 2;
                score.Result = EScoreResult.PSWin;
            }
            else if (game.PSGuestScore < game.PSHomeScore)
            {
                score.Points = 1;
                score.Result = EScoreResult.PSLoss;
            }
            else
            {
                throw new Exception(string.Format("Inconsistent score for game (ID: {0}, Result {1}: {2} {3}:{4}; OT {5}:{6}; PS {7}:{8})",
                    game.ID, game.HomeTeam.ShortName, game.GuestTeam.ShortName, game.HomeScore, game.GuestScore, game.OTHomeScore,
                    game.OTGuestScore, game.PSHomeScore, game.PSGuestScore));
            }
            return score;
        }
    }
}
