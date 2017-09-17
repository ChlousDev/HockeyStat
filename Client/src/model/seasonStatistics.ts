import { Season } from './season'

export class SeasonStatistics {
    season: Season;
    gamesPlayed: number;
    completion: number;
    gamesDecidedInNormalTime: number;
    gamesDecidedInNormalTimePercent: number;
    gamesDecidedInOverTime: number;
    gamesDecidedInOverTimePercent: number;
    gamesDecidedInPenaltyShots: number;
    gamesDecidedInPenaltyShotsPercent: number;
    homeWins: number;
    homeWinsPercent: number;
    guestWins: number;
    guestWinsPercent: number;
    goalsScored: number;
    goalsScoredPerGame: number;
    guestGoalsScored: number;
    guestGoalsScoredPerGame: number;
    guestPoints: number;
    guestPointsPerGame: number;
    homeGoalsScored: number;
    homeGoalsScoredPerGame: number;
    homePoints: number;
    homePointsPerGame: number;
}