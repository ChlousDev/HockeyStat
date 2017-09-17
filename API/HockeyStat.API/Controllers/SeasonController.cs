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
    public class SeasonController : ControllerBase
    {
        private HockeyStatDataAccess dataAccess;

        public SeasonController(HockeyStatDataAccess dataAccess)
        {
            this.dataAccess = dataAccess;
        }

        [HttpGet]
        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        public ObjectResult Get()
        {
            return this.StatusCode(StatusCodes.Status200OK, this.dataAccess.LoadSeasons());
        }

        [HttpPut]
        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        [Authorize(AuthenticationSchemes = ConfigurationOptions.AuthenticationScheme, Roles = "admin")]
        public ObjectResult Put([FromBody] Season season)
        {
            ObjectResult response = null;
            if (season.ID > 0)
            {
                this.dataAccess.SaveSeason(season);
                response = this.StatusCode(StatusCodes.Status200OK,"OK");
            }
            else
            {
                response = this.StatusCode(StatusCodes.Status400BadRequest, "New items have to be created with a Http-POST");
            }
            return response;
        }

        [HttpPost]
        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        [Authorize(AuthenticationSchemes = ConfigurationOptions.AuthenticationScheme, Roles = "admin")]
        public ObjectResult Post([FromBody] Season season)
        {
            ObjectResult response = null;
            if (season.ID <= 0)
            {
                long seasonID = this.dataAccess.SaveSeason(season);
                response = this.CreatedAtAction("Post", new { id = seasonID }, season);
            }
            else
            {
                response = this.StatusCode(StatusCodes.Status400BadRequest, "Existing items can only be Updated with a Http-PUT");
            }
            return response;
        }
    }
}
