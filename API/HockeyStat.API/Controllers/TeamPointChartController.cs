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
    public class TeamPointChartController : ApiController
    {
        private HockeyStatDataAccess dataAccess;

        public TeamPointChartController(HockeyStatDataAccess dataAccess)
        {
            this.dataAccess = dataAccess;
        }

        [HttpGet]
        public HttpResponseMessage Get(long seasonId)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            Season season = this.dataAccess.LoadSeason(seasonId);
            if (season == null)
            {
                response = this.Request.CreateResponse(HttpStatusCode.NotFound);
            }
            else
            {
                TeamPointChartDataProvider teamPointChartDataProvider = new TeamPointChartDataProvider(season, this.dataAccess);
                TeamPointChart teamPointChart = teamPointChartDataProvider.GetTeamPointChart();
                response = ResponseCreator.CreateNoCacheResponse(this.Request, teamPointChart);
            }
            return response;
        }
    }
}
