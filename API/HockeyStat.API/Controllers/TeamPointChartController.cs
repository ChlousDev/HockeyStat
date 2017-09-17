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
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace HockeyStat.API.Controllers
{
    [Route("api/[controller]")]
    public class TeamPointChartController : ControllerBase
    {
        private HockeyStatDataAccess dataAccess;

        public TeamPointChartController(HockeyStatDataAccess dataAccess)
        {
            this.dataAccess = dataAccess;
        }

        [HttpGet("Season/{seasonID}")]
        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        public ObjectResult Get(long seasonID)
        {
            ObjectResult response = null;
            Season season = this.dataAccess.LoadSeason(seasonID);
            if (season == null)
            {
                response = this.StatusCode(StatusCodes.Status404NotFound, "Not Found");
            }
            else
            {
                TeamPointChartDataProvider teamPointChartDataProvider = new TeamPointChartDataProvider(season, this.dataAccess);
                TeamPointChart teamPointChart = teamPointChartDataProvider.GetTeamPointChart();
                response = this.StatusCode(StatusCodes.Status200OK, teamPointChart);
            }
            return response;
        }
    }
}
