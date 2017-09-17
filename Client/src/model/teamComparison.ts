import { Season } from './season';
import { Team } from './team';
import { Game } from './game';

export class TeamComparison {
    season: Season;
    team1: Team;
    team2: Team;
    gamesPlayed: Game[];
    pointsTeam1: number;
    pointsTeam2: number;
    winsTeam1: number;
    otWinsTeam1: number;
    psWinsTeam1: number;
    winsTeam2: number;
    otWinsTeam2: number;
    psWinsTeam2: number;
    goalsTeam1: number;
    goalsTeam2: number;
}