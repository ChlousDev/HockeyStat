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
    public class StandingsController : ControllerBase
    {
        private HockeyStatDataAccess dataAccess;

        public StandingsController(HockeyStatDataAccess dataAccess)
        {
            this.dataAccess = dataAccess;
        }

        [HttpGet("Season/{seasonID}/Date/{date}")]
        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        public ObjectResult Get(long seasonID, DateTime date)
        {
            ObjectResult response = null;
            Season season = this.dataAccess.LoadSeason(seasonID);
            if(season==null)
            {
                response = this.StatusCode(StatusCodes.Status404NotFound, "Not Found");
            }
            else
            {
                StandingsCalculation standingsCalculation = new StandingsCalculation(season, date, this.dataAccess);
                Standings standings = standingsCalculation.CalculateStanding();
                response = this.StatusCode(StatusCodes.Status200OK, standings);
            }
            return response;
        }
    }
}
