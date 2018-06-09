using HockeyStat.Model.DataAccess;
using HockeyStat.Model.Model;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HockeyStat.MoveData
{
    class Program
    {
        static void Main(string[] args)
        {
            Configuration config = JsonConvert.DeserializeObject<Configuration>(File.ReadAllText("secretsettings.json"));

            DbContextOptionsBuilder<HockeyStatDbContext> sqlOptionsBuilder = new DbContextOptionsBuilder<HockeyStatDbContext>();
            sqlOptionsBuilder.UseSqlServer(config.SQLConnectionString);
            HockeyStatDbContext sqlDbContext = new HockeyStatDbContext(sqlOptionsBuilder.Options);

            DbContextOptionsBuilder<HockeyStatDbContext> sqliteOptionsBuilder = new DbContextOptionsBuilder<HockeyStatDbContext>();
            SqliteConnection connection = new SqliteConnection(config.SQLiteConnectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT quote($password);";
            command.Parameters.AddWithValue("$password", config.SQLitePassword);
            var quotedPassword = (string)command.ExecuteScalar();

            command.CommandText = "PRAGMA key = " + quotedPassword;
            command.Parameters.Clear();
            command.ExecuteNonQuery();

            sqliteOptionsBuilder.UseSqlite(connection);
            HockeyStatDbContext sqliteDbContext = new HockeyStatDbContext(sqliteOptionsBuilder.Options);

            foreach (Season season in sqlDbContext.Seasons)
            {
                sqliteDbContext.Seasons.Add(new Season() { ID = season.ID, StartYear = season.StartYear });
                sqliteDbContext.SaveChanges();
            }

            foreach (Team team in sqlDbContext.Teams)
            {
                sqliteDbContext.Teams.Add(new Team() { ID = team.ID, Name = team.Name, ShortName = team.ShortName });
                sqliteDbContext.SaveChanges();
            }

            foreach (Game game in sqlDbContext.Games.Include(g => g.GuestTeam).Include(g => g.HomeTeam).Include(g => g.Season))
            {
                Team guestTeam = sqliteDbContext.Teams.FirstOrDefault(t => t.ID == game.GuestTeam.ID);
                Team homeTeam = sqliteDbContext.Teams.FirstOrDefault(t => t.ID == game.HomeTeam.ID);
                Season season = sqliteDbContext.Seasons.FirstOrDefault(s => s.ID == game.Season.ID);
                sqliteDbContext.Games.Add(new Game() { ID = game.ID, Date = game.Date, GuestScore = game.GuestScore, GuestTeam = guestTeam, HomeScore = game.HomeScore, HomeTeam = homeTeam, OTGuestScore = game.OTGuestScore, OTHomeScore = game.OTHomeScore, PSGuestScore = game.PSGuestScore, PSHomeScore = game.PSHomeScore, Season = season });
                sqliteDbContext.SaveChanges();
            }

        }
    }
}
