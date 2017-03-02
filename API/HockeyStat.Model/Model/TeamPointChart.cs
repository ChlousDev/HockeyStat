using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HockeyStat.Model.Model
{
    public class TeamPointChart
    {
        public Season Season { get; set; }

        public List<TeamPointSeries> Series { get; set; }

        public TeamPointChart(Season season,  List<TeamPointSeries> series)
        {
            this.Season = season;
            this.Series = series;
        }
    }
}
