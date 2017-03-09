import { Season } from './season';
import { Team } from './team';
import { Game } from './game';

export class TeamComparison {
    Season: Season;
    Team1: Team;
    Team2: Team;
    GamesPlayed: Game[];
    PointsTeam1: number;
    PointsTeam2: number;
    WinsTeam1: number;
    OTWinsTeam1: number;
    PSWinsTeam1: number;
    WinsTeam2: number;
    OTWinsTeam2: number;
    PSWinsTeam2: number;
    GoalsTeam1: number;
    GoalsTeam2: number;
}