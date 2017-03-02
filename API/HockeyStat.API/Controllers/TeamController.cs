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
    public class TeamController : ApiController
    {
        private HockeyStatDataAccess dataAccess;

        public TeamController(HockeyStatDataAccess dataAccess)
        {
            this.dataAccess = dataAccess;
        }

        [HttpGet]
        public HttpResponseMessage Get()
        {
            return ResponseCreator.CreateNoCacheResponse(this.Request, this.dataAccess.LoadTeams());
        }

        [Authorize(Roles = "admin")]
        [HttpPut]
        public HttpResponseMessage Put([FromBody] Team team)
        {
            HttpResponseMessage response = null;
            if (team.ID > 0)
            {
                this.dataAccess.SaveTeam(team);
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
        public HttpResponseMessage Post([FromBody] Team team)
        {
            HttpResponseMessage response = null;
            if (team.ID <= 0)
            {
                long teamID = this.dataAccess.SaveTeam(team);
                response = this.Request.CreateResponse(HttpStatusCode.Created);
                response.Headers.Location = new Uri(this.Request.RequestUri + teamID.ToString());
            }
            else
            {
                response = this.Request.CreateResponse(HttpStatusCode.BadRequest, "Existing items can only be Updated with a Http-PUT");
            }
            return response;
        }
    }
}
