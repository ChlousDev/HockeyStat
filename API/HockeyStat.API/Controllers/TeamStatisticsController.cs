using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HockeyStat.Model.DataAccess;
using HockeyStat.Model.Logic;
using HockeyStat.Model.Model;
using HockeyStat.API.Util;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http;

namespace HockeyStat.API.Controllers
{
    [Route("api/[controller]")]
    public class TeamStatisticsController : ApiController
    {
        private HockeyStatDataAccess dataAccess;

        public TeamStatisticsController(HockeyStatDataAccess dataAccess)
        {
            this.dataAccess = dataAccess;
        }

        [HttpGet("Season/{seasonID}/Team/{teamID}")]
        public HttpResponseMessage Get(long seasonId, long teamId)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            Season season = this.dataAccess.LoadSeason(seasonId);
            Team team = this.dataAccess.LoadTeam(teamId);
            if ((season == null) || (team == null))
            {
                response = this.Request.CreateResponse(HttpStatusCode.NotFound);
            }
            else
            {
                TeamAnalizer teamAnalizer = new TeamAnalizer(season, team, this.dataAccess);
                TeamStatistics statistics = teamAnalizer.CreateStatistics();
                response = ResponseCreator.CreateNoCacheResponse(this.Request, statistics);
            }
            return response;
        }
    }
}
