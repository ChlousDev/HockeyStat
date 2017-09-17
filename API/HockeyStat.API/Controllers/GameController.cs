using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HockeyStat.Model.DataAccess;
using HockeyStat.Model.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace HockeyStat.API.Controllers
{
    [Route("api/[controller]")]
    public class GameController : ControllerBase
    {

        private HockeyStatDataAccess dataAccess;

        public GameController(HockeyStatDataAccess dataAccess)
        {
            this.dataAccess = dataAccess;
        }

        [HttpGet]
        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        public ObjectResult Get()
        {
            return this.StatusCode(StatusCodes.Status200OK, this.dataAccess.LoadGames());
        }

        [HttpGet("{id}")]
        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        public ObjectResult Get(int id)
        {
            return this.StatusCode(StatusCodes.Status200OK, this.dataAccess.LoadGames());
        }

        [HttpGet("season/{seasonID}/paged/{page}/{pageSize}")]
        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        public ObjectResult Get(long seasonID, int page, int pageSize)
        {
            List<Game> games = this.dataAccess.LoadGamesOfSeason(seasonID).OrderByDescending(g => g.Date).ToList();
            int totalPages = games.Count / pageSize;
            if (games.Count % pageSize > 0)
            {
                totalPages = totalPages + 1;
            }
            PagedList<Game> pagedGames = new PagedList<Game>()
            {
                Page = page,
                PageSize = pageSize,
                TotalItems = games.Count,
                TotalPages = totalPages,
                Items = games.Skip(page * pageSize).Take(pageSize).ToList(),
            };
            return this.StatusCode(StatusCodes.Status200OK, pagedGames);
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = ConfigurationOptions.AuthenticationScheme, Roles = "admin")]
        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        public ObjectResult Put([FromBody] Game game)
        {
            ObjectResult response = null;
            if (game.ID > 0)
            {
                this.dataAccess.UpdateGame(game);
                response = this.StatusCode(StatusCodes.Status200OK, "OK");
            }
            else
            {
                response = this.StatusCode(StatusCodes.Status400BadRequest, "New items have to be created with a Http-POST");
            }
            return response;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = ConfigurationOptions.AuthenticationScheme, Roles = "admin")]
        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        public ObjectResult Post([FromBody] Game game)
        {
            ObjectResult response = null;
            if (game.ID <= 0)
            {
                long gameID = this.dataAccess.AddGame(game);
                response = this.CreatedAtAction("Post", new { id = gameID }, game);
            }
            else
            {
                response = this.StatusCode(StatusCodes.Status400BadRequest, "Existing items can only be Updated with a Http-PUT");
            }
            return response;
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = ConfigurationOptions.AuthenticationScheme, Roles = "admin")]
        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        public ObjectResult Delete(long id)
        {
            ObjectResult response = null;
            Game game = this.dataAccess.LoadGame(id);
            if (game == null)
            {
                response = this.StatusCode(StatusCodes.Status404NotFound, "Not Found");
            }
            else
            {
                dataAccess.DeleteGame(game);
                response = this.StatusCode(StatusCodes.Status200OK, "OK");
            }
            return response;
        }
    }
}
