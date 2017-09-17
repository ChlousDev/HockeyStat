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
    public class TeamComparisonController : ControllerBase
    {
        private HockeyStatDataAccess dataAccess;

        public TeamComparisonController(HockeyStatDataAccess dataAccess)
        {
            this.dataAccess = dataAccess;
        }

        [HttpGet("Season/{seasonID}/Team1/{team1ID}/Team2/{team2ID}")]
        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        public ObjectResult Get(long seasonID, long team1ID, long team2ID)
        {
            ObjectResult response = null;
            Season season = this.dataAccess.LoadSeason(seasonID);
            Team team1 = this.dataAccess.LoadTeam(team1ID);
            Team team2 = this.dataAccess.LoadTeam(team2ID);
            if ((season == null) || (team1 == null) || (team2 == null))
            {
                response = this.StatusCode(StatusCodes.Status404NotFound, "Not Found");
            }
            else
            {
                TeamComparator comparator = new TeamComparator(season, team1, team2, this.dataAccess);
                TeamComparison comparison = comparator.CompareTeams();
                response = this.StatusCode(StatusCodes.Status200OK, comparison);
            }
            return response;
        }
    }
}
