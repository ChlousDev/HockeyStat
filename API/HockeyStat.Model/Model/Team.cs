using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HockeyStat.Model.Model
{
    public class Team : IDBEntity
    {
        [Key]
        public long ID { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(50)]
        public string ShortName { get; set; }
    }
}
