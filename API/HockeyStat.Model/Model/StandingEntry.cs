using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HockeyStat.Model.Model
{
    public class StandingsEntry
    {
        public int Position { get; set; }

        public Team Team { get; set; }

        public int Points { get; set; }

        public int Wins { get; set; }

        public int OTWins { get; set; }

        public int OTLosses { get; set; }

        public int Losses { get; set; }

        public int GoalsScored { get; set; }

        public int GoalsScoredAgainst { get; set; }

        public int GamesPlayed { get; set; }

        public string SortString
        {
            get
            {
                return string.Format("{0:000}/{1:00}", 999 - this.Points, this.GamesPlayed);
            }
        
        }

        public StandingsEntry(Model.Team team)
        {
            this.Team = team;
            this.Points = 0;
            this.Wins = 0;
            this.OTWins = 0;
            this.OTLosses = 0;
            this.Losses = 0;
            this.GoalsScored = 0;
            this.GoalsScoredAgainst = 0;
            this.GamesPlayed = 0;
        }
    }
}
