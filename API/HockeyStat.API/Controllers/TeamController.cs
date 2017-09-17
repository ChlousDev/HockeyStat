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
using Microsoft.AspNetCore.Http;

namespace HockeyStat.API.Controllers
{
    [Route("api/[controller]")]
    public class TeamController : ControllerBase
    {
        private HockeyStatDataAccess dataAccess;

        public TeamController(HockeyStatDataAccess dataAccess)
        {
            this.dataAccess = dataAccess;
        }

        [HttpGet]
        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        public ObjectResult Get()
        {
            return this.StatusCode(StatusCodes.Status200OK, this.dataAccess.LoadTeams());
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = ConfigurationOptions.AuthenticationScheme, Roles = "admin")]
        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        public ObjectResult Put([FromBody] Team team)
        {
            ObjectResult response = null;
            if (team.ID > 0)
            {
                this.dataAccess.SaveTeam(team);
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
        public ObjectResult Post([FromBody] Team team)
        {
            ObjectResult response = null;
            if (team.ID <= 0)
            {
                long teamID = this.dataAccess.SaveTeam(team);
                response = this.CreatedAtAction("Post", new { id = teamID }, team);
            }
            else
            {
                response = this.StatusCode(StatusCodes.Status400BadRequest, "Existing items can only be Updated with a Http-PUT");
            }
            return response;
        }
    }
}
