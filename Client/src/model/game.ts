import { Team } from './Team';
import { Season } from './Season';

export class Game {
    ID: number;
    Date: Date;
    HomeScore: number;
    GuestScore: number;
    OTHomeScore: number;
    OTGuestScore: number;
    PSHomeScore: number;
    PSGuestScore: number;
    HomeTeam: Team;
    GuestTeam: Team;
    Season: Season;

    constructor(date: Date) {
        this.Date = date;
        this.GuestScore = 0;
        this.HomeScore = 0;
        this.OTGuestScore = 0;
        this.OTHomeScore = 0;
        this.PSGuestScore = 0;
        this.PSHomeScore = 0;
    }
}

