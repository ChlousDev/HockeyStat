import { Team } from './team'

export class StandingsEntry {
    position: number;
    team: Team;
    points: number;
    wins: number;
    otWins: number;
    otLosses: number;
    losses: number;
    goalsScored: number;
    goalsScoredAgainst: number;
    gamesPlayed: number;
    sortString: string;
}