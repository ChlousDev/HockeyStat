import { Season } from './season';
import { Team } from './team';
import { Game } from './game';

export class TeamStatistics {
    season: Season;
    team: Team;
    points: number;
    pointsPerGame: number;
    goalsScored: number;
    goalsScoredPerGame: number;
    goalsScoredAgainst: number;
    goalsScoredAgainstPerGame: number;
    gamesPlayed: Game[];
    wins: number;
    otWins: number;
    psWins: number;
    losses: number;
    otlosses: number;
    pslosses: number;
    winsPercent: number;
    otWinsPercent: number;
    psWinsPercent: number;
    lossesPercent: number;
    otLossesPercent: number;
    psLossesPercent: number;
}