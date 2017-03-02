using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HockeyStat.Model.Model;
using Microsoft.EntityFrameworkCore;

namespace HockeyStat.Model.DataAccess
{
    public class HockeyStatDataAccess
    {
        private HockeyStatDbContext dbContext;

        public HockeyStatDataAccess(HockeyStatDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        #region Seasons

        public List<Season> LoadSeasons()
        {
            return this.dbContext.Seasons.ToList();
        }

        public Season LoadSeason(long seasonId)
        {
            return this.dbContext.Seasons.FirstOrDefault(s => s.ID == seasonId);
        }

        public long SaveSeason(Season season)
        {
            this.AttachEntity(season);
            this.dbContext.SaveChanges();
            return season.ID;
        }

        #endregion Seasons

        #region Teams

        public List<Team> LoadTeams()
        {
            return this.dbContext.Teams.ToList();
        }

        public Team LoadTeam(long teamId)
        {
            return this.dbContext.Teams.FirstOrDefault(t => t.ID == teamId);
        }

        public long SaveTeam(Team team)
        {
            this.AttachEntity(team);
            this.dbContext.SaveChanges();
            return team.ID;
        }

        #endregion Teams

        #region Games

        public Game LoadGame(long gameID)
        {
            return this.GetBaseGameQuery().FirstOrDefault(g => g.ID == gameID);
        }

        public List<Game> LoadGames()
        {
            return this.LoadGamesOfSeason(0);
        }

        public List<Game> LoadGamesOfSeason(long seasonID)
        {
            return this.LoadGamesOfSeason(seasonID, DateTime.MinValue, null);
        }

        public List<Game> LoadGamesOfSeason(long seasonID, DateTime untilTo, List<long> teamIDs)
        {
            IQueryable<Game> query = GetBaseGameQuery();
            if (seasonID > 0)
            {
                query = query.Where(g => g.Season.ID == seasonID);
            }
            if (untilTo > DateTime.MinValue)
            {
                query = query.Where(g => g.Date <= untilTo);
            }
            if (teamIDs != null)
            {
                query = query.Where(g => teamIDs.Contains(g.HomeTeam.ID) && teamIDs.Contains(g.GuestTeam.ID));
            }
            return query.ToList();
        }

        private IQueryable<Game> GetBaseGameQuery()
        {
            return this.dbContext.Games.Include(g => g.Season).Include(g => g.HomeTeam).Include(g => g.GuestTeam);
        }

        public List<Game> LoadGamesOfSeasonForTeam(long seasonID, long teamID)
        {
            return this.GetBaseGameQuery().Where(g => (g.Season.ID == seasonID) && ((g.HomeTeam.ID == teamID) || (g.GuestTeam.ID == teamID))).ToList();
        }

        public List<Game> LoadGamesOfTeams(long seasonID, long team1ID, long team2ID)
        {
            IQueryable<Game> query = this.GetBaseGameQuery().Where(g => (g.Season.ID == seasonID) &&
                ((g.HomeTeam.ID == team1ID) || (g.GuestTeam.ID == team1ID)) &&
                ((g.HomeTeam.ID == team2ID) || (g.GuestTeam.ID == team2ID)));

            return query.ToList();
        }

        public long AddGame(Game game)
        {
            this.AttachEntity(game.Season);
            this.AttachEntity(game.HomeTeam);
            this.AttachEntity(game.GuestTeam);
            this.AttachEntity(game);
            this.dbContext.SaveChanges();
            return game.ID;
        }

        public long UpdateGame(Game game)
        {
            this.AttachEntity(game.Season);
            this.AttachEntity(game.HomeTeam);
            this.AttachEntity(game.GuestTeam);
            Game gameToUpdate = this.dbContext.Games.Include(g => g.Season).Include(g => g.HomeTeam).Include(g => g.GuestTeam).FirstOrDefault(g => g.ID == game.ID);
            gameToUpdate.Season = game.Season;
            gameToUpdate.Date = game.Date;
            gameToUpdate.HomeTeam = game.HomeTeam;
            gameToUpdate.GuestTeam = game.GuestTeam;
            gameToUpdate.HomeScore = game.HomeScore;
            gameToUpdate.GuestScore = game.GuestScore;
            gameToUpdate.OTHomeScore = game.OTHomeScore;
            gameToUpdate.OTGuestScore = game.OTGuestScore;
            gameToUpdate.PSHomeScore = game.PSHomeScore;
            gameToUpdate.PSGuestScore = game.PSGuestScore;
            this.dbContext.SaveChanges();
            return game.ID;
        }

        public void DeleteGame(Game game )
        {
            this.dbContext.Games.Remove(game);
            this.dbContext.SaveChanges();
        }

        #endregion Games

        private void AttachEntity(IDBEntity entity)
        {
            if (entity.ID > 0)
            {
                this.dbContext.Attach(entity);
                this.dbContext.Entry(entity).State = EntityState.Modified;
            }
            else
            {
                this.dbContext.Add(entity);
            }
        }




        
    }
}
