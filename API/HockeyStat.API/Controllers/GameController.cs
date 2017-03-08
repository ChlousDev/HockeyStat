using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HockeyStat.Model.DataAccess;
using HockeyStat.Model.Model;
using HockeyStat.API.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http;

namespace HockeyStat.API.Controllers
{
    [Route("api/[controller]")]
    public class GameController : ApiController
    {

        private HockeyStatDataAccess dataAccess;

        public GameController(HockeyStatDataAccess dataAccess)
        {
            this.dataAccess = dataAccess;
        }

        [HttpGet]
        public HttpResponseMessage Get()
        {
            return ResponseCreator.CreateNoCacheResponse(this.Request, this.dataAccess.LoadGames());
        }

        [HttpGet("{id}")]
        public HttpResponseMessage Get(int id)
        {
            return ResponseCreator.CreateNoCacheResponse(this.Request, this.dataAccess.LoadGames());
        }

        [HttpGet("season/{seasonID}/paged/{page}/{pageSize}")]
        public HttpResponseMessage Get(long seasonID, int page, int pageSize)
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
            return ResponseCreator.CreateNoCacheResponse(this.Request, pagedGames);
        }

        [HttpPut]
        [Authorize(Roles = "admin")]
        public HttpResponseMessage Put([FromBody] Game game)
        {
            HttpResponseMessage response = null;
            if (game.ID > 0)
            {
                this.dataAccess.UpdateGame(game);
                response = this.Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                response = this.Request.CreateResponse(HttpStatusCode.BadRequest, "New items have to be created with a Http-POST");
            }
            return response;
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public HttpResponseMessage Post([FromBody] Game game)
        {
            HttpResponseMessage response = null;
            if (game.ID <= 0)
            {
                long gameID = this.dataAccess.AddGame(game);
                response = this.Request.CreateResponse(HttpStatusCode.Created);
                response.Headers.Location = new Uri(this.Request.RequestUri + "/" + gameID.ToString());
            }
            else
            {
                response = this.Request.CreateResponse(HttpStatusCode.BadRequest, "Existing items can only be Updated with a Http-PUT");
            }
            return response;
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public HttpResponseMessage Delete(long id)
        {
            HttpResponseMessage response = null;
            Game game = this.dataAccess.LoadGame(id);
            if (game == null)
            {
                response = this.Request.CreateResponse(HttpStatusCode.NotFound);
            }
            else
            {
                dataAccess.DeleteGame(game);
                response = this.Request.CreateResponse(HttpStatusCode.OK);
            }
            return response;
        }
    }
}
