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
    public class StandingsController : ApiController
    {
        private HockeyStatDataAccess dataAccess;

        public StandingsController(HockeyStatDataAccess dataAccess)
        {
            this.dataAccess = dataAccess;
        }

        [HttpGet("Season/{seasonId}/Date/{date}")]
        public HttpResponseMessage Get(long seasonId, DateTime date)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            Season season = this.dataAccess.LoadSeason(seasonId);
            if(season==null)
            {
                response = this.Request.CreateResponse(HttpStatusCode.NotFound);
            }
            else
            {
                StandingsCalculation standingsCalculation = new StandingsCalculation(season, date, this.dataAccess);
                Standings standings = standingsCalculation.CalculateStanding();
                response = ResponseCreator.CreateNoCacheResponse(this.Request, standings);
            }
            return response;
        }
    }
}
