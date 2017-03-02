using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HockeyStat.Model.Model
{
    public class Game : IDBEntity
    {
        [Key]
        public long ID { get; set; }

        [Column(TypeName="datetime2")]
        public DateTime Date { get; set; }

        public int HomeScore { get; set; }

        public int GuestScore { get; set; }

        public int OTHomeScore { get; set; }

        public int OTGuestScore { get; set; }

        public int PSHomeScore { get; set; }

        public int PSGuestScore { get; set; }

        [ForeignKey("HomeTeam_ID")]
        public Team HomeTeam { get; set; }

        [ForeignKey("GuestTeam_ID")]
        public Team GuestTeam { get; set; }

        [ForeignKey("Season_ID")]
        public Season Season { get; set; }
    }
}
