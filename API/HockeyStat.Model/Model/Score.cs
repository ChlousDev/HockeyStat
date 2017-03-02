using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HockeyStat.Model.Model
{
    public class Score
    {
        public Team Team { get; set; }

        public EScoreResult Result { get; set; }

        public int Goals { get; set; }

        public int GoalsAgainst { get; set; }

        public int Points { get; set; }

        public Score(Team team)
        {
            this.Team = team;
            this.Result = EScoreResult.Loss;
            this.Goals = 0;
            this.GoalsAgainst = 0;
            this.Points = 0;
        }
    }
}
