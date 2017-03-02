using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HockeyStat.Model.Model
{
    public class Season : IDBEntity
    {
        [Key]
        public long ID { get; set; }

        public int StartYear { get; set; }
    }
}
