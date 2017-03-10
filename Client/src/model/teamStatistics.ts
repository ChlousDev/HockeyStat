import { Season } from './season';
import { Team } from './team';
import { Game } from './game';

export class TeamStatistics {
    Season: Season;
    Team: Team;
    Points: number;
    PointsPerGame: number;
    GoalsScored: number;
    GoalsScoredPerGame: number;
    GoalsScoredAgainst: number;
    GoalsScoredAgainstPerGame: number;
    GamesPlayed: Game[];
    Wins: number;
    OTWins: number;
    PSWins: number;
    Losses: number;
    OTLosses: number;
    PSLosses: number;
    WinsPercent: number;
    OTWinsPercent: number;
    PSWinsPercent: number;
    LossesPercent: number;
    OTLossesPercent: number;
    PSLossesPercent: number;
}