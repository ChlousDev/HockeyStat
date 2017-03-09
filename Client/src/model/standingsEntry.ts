import { Team } from './team'

export class StandingsEntry {
    Position: number;
    Team: Team;
    Poinst: number;
    Wins: number;
    OTWins: number;
    OTLosses: number;
    Losses: number;
    GoalsScored: number;
    GoalsScoredAgainst: number;
    GamesPlayed: number;
    SortString: string;
}