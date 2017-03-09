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
    public class TeamComparisonController : ApiController
    {
        private HockeyStatDataAccess dataAccess;

        public TeamComparisonController(HockeyStatDataAccess dataAccess)
        {
            this.dataAccess = dataAccess;
        }

        [HttpGet("Season/{seasonID}/Team1/{team1ID}/Team2/{team2ID}")]
        public HttpResponseMessage Get(long seasonId, long team1Id, long team2Id)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            Season season = this.dataAccess.LoadSeason(seasonId);
            Team team1 = this.dataAccess.LoadTeam(team1Id);
            Team team2 = this.dataAccess.LoadTeam(team2Id);
            if ((season == null) || (team1 == null) || (team2 == null))
            {
                response = this.Request.CreateResponse(HttpStatusCode.NotFound);
            }
            else
            {
                TeamComparator comparator = new TeamComparator(season, team1, team2, this.dataAccess);
                TeamComparison comparison = comparator.CompareTeams();
                response = ResponseCreator.CreateNoCacheResponse(this.Request, comparison);
            }
            return response;
        }
    }
}
