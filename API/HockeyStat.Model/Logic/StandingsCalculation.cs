using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HockeyStat.Model.DataAccess;
using HockeyStat.Model.Model;

namespace HockeyStat.Model.Logic
{
    public class StandingsCalculation
    {
        private Season season;
        private DateTime date;
        private HockeyStatDataAccess dataAccess;

        public StandingsCalculation(Season season, DateTime date, HockeyStatDataAccess dataAccess)
        {
            this.season = season;
            this.date = date;
            this.dataAccess = dataAccess;
        }

        public Standings CalculateStanding()
        {
            List<Game> games = this.dataAccess.LoadGamesOfSeason(this.season.ID, this.date, null);
            Dictionary<long, StandingsEntry> standingsEntries = new Dictionary<long, StandingsEntry>();
            foreach (Game game in games)
            {
                ScoreCalculation scoreCalculation = new ScoreCalculation(game);
                Score score = scoreCalculation.CalculateGuestTeamScore();
                StandingsCalculation.AddEntryValuesForTeam(standingsEntries, scoreCalculation.CalculateHomeTeamScore(), game.HomeTeam);
                StandingsCalculation.AddEntryValuesForTeam(standingsEntries, scoreCalculation.CalculateGuestTeamScore(), game.GuestTeam);
            }

            List<StandingsEntry> sortedEntries = new List<StandingsEntry>();
            foreach (var entryGroup in standingsEntries.Values.GroupBy(se => se.SortString).OrderBy(eg => eg.Key))
            {
                if (entryGroup.Count() == 1)
                {
                    sortedEntries.Add(entryGroup.First());
                }
                else
                {
                    Standings groupStandings = this.CalculateStandingForTeams(entryGroup.Select(eg => eg.Team).ToList());
                    foreach (var groupStandingsEntryGroup in groupStandings.Entries.GroupBy(se => se.Position))
                    {
                        if (groupStandingsEntryGroup.Count() == 1)
                        {
                            sortedEntries.Add(entryGroup.First(se => se.Team.ID == groupStandingsEntryGroup.First().Team.ID));
                        }
                        else
                        {
                            List<StandingsEntry> entriesToInsert = new List<StandingsEntry>();
                            foreach(StandingsEntry entry in groupStandingsEntryGroup)
                            {
                                entriesToInsert.Add(entryGroup.First(se => se.Team.ID == entry.Team.ID));
                            }
                            List<StandingsEntry> sortedGroupEntries = StandingsCalculation.SortStandingsEntries(entriesToInsert);
                            foreach (StandingsEntry entryToInsert in sortedGroupEntries)
                            {
                                sortedEntries.Add(entryToInsert);
                            }
                        }
                    }
                }
            }

            int position = 1;
            foreach (StandingsEntry entry in sortedEntries)
            {
                entry.Position = position;
                position++;
            }

            return new Standings(this.date, this.season, sortedEntries);
        }

        private Standings CalculateStandingForTeams(List<Team> teams)
        {
            List<Game> games = this.dataAccess.LoadGamesOfSeason(this.season.ID, this.date, teams.Select(t => t.ID).ToList());
            Dictionary<long, StandingsEntry> standingsEntries = new Dictionary<long, StandingsEntry>();
            foreach (Game game in games)
            {
                ScoreCalculation scoreCalculation = new ScoreCalculation(game);
                Score score = scoreCalculation.CalculateGuestTeamScore();
                StandingsCalculation.AddEntryValuesForTeam(standingsEntries, scoreCalculation.CalculateHomeTeamScore(), game.HomeTeam);
                StandingsCalculation.AddEntryValuesForTeam(standingsEntries, scoreCalculation.CalculateGuestTeamScore(), game.GuestTeam);
            }

            //make sure that there is an entry for each team
            foreach (Team team in teams)
            {
                GetStandingsEntryForTeam(standingsEntries, team);
            }

            List<StandingsEntry> sortedEntries = StandingsCalculation.SortStandingsEntries(standingsEntries.Values.ToList());

            StandingsCalculation.SetPosition(sortedEntries);

            return new Standings(this.date, this.season, sortedEntries);
        }

        private static List<StandingsEntry> SortStandingsEntries(List<StandingsEntry> standingsEntries)
        {
            List<StandingsEntry> sortedEntries = standingsEntries.OrderByDescending(se => se.Points)
                .ThenBy(se => se.GamesPlayed)
                .ThenByDescending(se => se.GoalsScored - se.GoalsScoredAgainst)
                .ThenByDescending(se => se.GoalsScored).ToList();
            return sortedEntries;
        }

        private static void SetPosition(List<StandingsEntry> sortedEntries)
        {
            int position = 0;
            for (int i = 0; i < sortedEntries.Count(); i++)
            {
                StandingsEntry entry = sortedEntries.ElementAt(i);
                if (i == 0)
                {
                    position = i + 1;
                }
                else
                {
                    StandingsEntry previousEntry = sortedEntries.ElementAt(i - 1);
                    if ((previousEntry.Points != entry.Points) ||
                                ((previousEntry.GoalsScored - previousEntry.GoalsScoredAgainst) != (entry.GoalsScored - entry.GoalsScoredAgainst)) ||
                                (previousEntry.GoalsScored != entry.GoalsScored) ||
                                (previousEntry.GamesPlayed != entry.GamesPlayed))
                    {
                        position = i + 1;
                    }
                }

                entry.Position = position;
            }
        }

        private static void AddEntryValuesForTeam(Dictionary<long, StandingsEntry> standingsEntries, Score score, Team team)
        {
            StandingsEntry standingsEntry = StandingsCalculation.GetStandingsEntryForTeam(standingsEntries, team);
            standingsEntry.GamesPlayed++;
            standingsEntry.GoalsScored = standingsEntry.GoalsScored + score.Goals;
            standingsEntry.GoalsScoredAgainst = standingsEntry.GoalsScoredAgainst + score.GoalsAgainst;
            standingsEntry.Points = standingsEntry.Points + score.Points;
            switch (score.Result)
            {
                case EScoreResult.Win:
                    standingsEntry.Wins++;
                    break;
                case EScoreResult.OTWin:
                case EScoreResult.PSWin:
                    standingsEntry.OTWins++;
                    break;
                case EScoreResult.OTLoss:
                case EScoreResult.PSLoss:
                    standingsEntry.OTLosses++;
                    break;
                case EScoreResult.Loss:
                    standingsEntry.Losses++;
                    break;
            }
        }

        private static StandingsEntry GetStandingsEntryForTeam(Dictionary<long, StandingsEntry> standingsEntries, Team team)
        {
            if (!standingsEntries.ContainsKey(team.ID))
            {
                standingsEntries.Add(team.ID, new StandingsEntry(team));
            }
            return standingsEntries[team.ID];
        }
    }
}
