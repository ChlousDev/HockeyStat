import {Season} from './season'
import {StandingsEntry} from './standingsEntry'

export class Standings{
    season: Season;
    date: Date;
    entries: StandingsEntry[];
}