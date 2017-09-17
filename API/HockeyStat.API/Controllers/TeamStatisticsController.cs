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
    public class TeamStatisticsController : ControllerBase
    {
        private HockeyStatDataAccess dataAccess;

        public TeamStatisticsController(HockeyStatDataAccess dataAccess)
        {
            this.dataAccess = dataAccess;
        }

        [HttpGet("Season/{seasonID}/Team/{teamID}")]
        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        public ObjectResult Get(long seasonID, long teamID)
        {
            ObjectResult response = null;
            Season season = this.dataAccess.LoadSeason(seasonID);
            Team team = this.dataAccess.LoadTeam(teamID);
            if ((season == null) || (team == null))
            {
                response = this.StatusCode(StatusCodes.Status404NotFound, "Not Found");
            }
            else
            {
                TeamAnalizer teamAnalizer = new TeamAnalizer(season, team, this.dataAccess);
                TeamStatistics statistics = teamAnalizer.CreateStatistics();
                response = this.StatusCode(StatusCodes.Status200OK, statistics);
            }
            return response;
        }
    }
}
