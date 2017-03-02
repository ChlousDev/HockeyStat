using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HockeyStat.Model.Model;
using Microsoft.EntityFrameworkCore;

namespace HockeyStat.Model.DataAccess
{
    public class HockeyStatDbContext: DbContext
    {

        public DbSet<Season> Seasons { get; set; }

        public DbSet<Team> Teams { get; set; }

        public DbSet<Game> Games { get; set; }

        public HockeyStatDbContext(DbContextOptions<HockeyStatDbContext> options):base(options)
        { }
        
    }
}
