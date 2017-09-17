import { Team } from './Team';
import { Season } from './Season';

export class Game {
    id: number;
    date: Date;
    homeScore: number;
    guestScore: number;
    otHomeScore: number;
    otGuestScore: number;
    psHomeScore: number;
    psGuestScore: number;
    homeTeam: Team;
    guestTeam: Team;
    season: Season;

    constructor(date: Date) {
        this.date = date;
        this.guestScore = 0;
        this.homeScore = 0;
        this.otGuestScore = 0;
        this.otHomeScore = 0;
        this.psGuestScore = 0;
        this.psHomeScore = 0;
    }
}

