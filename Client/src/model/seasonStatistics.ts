import { Season } from './season'

export class SeasonStatistics {
    Season: Season;
    GamesPlayed: number;
    Completion: number;
    GamesDecidedInNormalTime: number;
    GamesDecidedInNormalTimePercent: number;
    GamesDecidedInOverTime: number;
    GamesDecidedInOverTimePercent: number;
    GamesDecidedInPenaltyShots: number;
    GamesDecidedInPenaltyShotsPercent: number;
    HomeWins: number;
    HomeWinsPercent: number;
    GuestWins: number;
    GuestWinsPercent: number;
    GoalsScored: number;
    GoalsScoredPerGame: number;
    GuestGoalsScored: number;
    GuestGoalsScoredPerGame: number;
    GuestPoints: number;
    GuestPointsPerGame: number;
    HomeGoalsScored: number;
    HomeGoalsScoredPerGame: number;
    HomePoints: number;
    HomePointsPerGame: number;
}