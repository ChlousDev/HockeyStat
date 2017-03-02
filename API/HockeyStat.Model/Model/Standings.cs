using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HockeyStat.Model.Model
{
    public class Standings
    {
        
        public Season Season { get; set; }

        public DateTime Date { get; set; }

        public List<StandingsEntry> Entries { get; set; }

        public Standings(DateTime date, Model.Season season, List<StandingsEntry> entries)
        {
            // TODO: Complete member initialization
            this.Date = date;
            this.Season = season;
            this.Entries = entries;
        }
    }
}
