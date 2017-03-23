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
    public class SeasonStatisticsController : ApiController
    {
        private HockeyStatDataAccess dataAccess;

        public SeasonStatisticsController(HockeyStatDataAccess dataAccess)
        {
            this.dataAccess = dataAccess;
        }

        [HttpGet("Season/{seasonID}")]
        public HttpResponseMessage Get(long seasonID)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            Season season = this.dataAccess.LoadSeason(seasonID);
            if (season == null)
            {
                response = this.Request.CreateResponse(HttpStatusCode.NotFound);
            }
            else
            {
                SeasonAnalizer seasonAnalizer = new SeasonAnalizer(season, this.dataAccess);
                SeasonStatistics statistics = seasonAnalizer.CreateStatistics();
                response = ResponseCreator.CreateNoCacheResponse(this.Request, statistics);
            }
            return response;
        }
    }
}
