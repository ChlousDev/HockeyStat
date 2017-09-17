import { Injectable, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { Observable } from 'rxjs/Rx';

import { environment } from '../environments/environment';

import { ApiErrorHandlingProvider } from './apiErrorHandling.provider';

import { Game } from '../model/game';
import { PagedList } from '../model/pagedList';

@Injectable()
export class GameProvider {

    private gameApiUrl: string;

    constructor(private http: Http, private errorHandling: ApiErrorHandlingProvider) {
        this.gameApiUrl = environment.apiEndpoint + 'game';
    }

    public getGames(seasonID: number, page: number, pageSize: number): Observable<PagedList<Game>> {
        return this.http.get(this.gameApiUrl + '/season/' + seasonID + '/paged/' + page + '/' + pageSize)
            .map((response) => response.json())
            ._catch(this.errorHandling.handleError);
    }

    public saveGame(game: Game): Observable<void> {
        var result: Observable<void> = null;
        if (game.id > 0) {
            result = this.http.put(this.gameApiUrl, game, { withCredentials: true })
                ._catch(this.errorHandling.handleError);
        }
        else {
            result = this.http.post(this.gameApiUrl, game, { withCredentials: true })
                ._catch(this.errorHandling.handleError);
        }
        return result;
    }

    public deleteGame(game: Game): Observable<void> {
        return this.http.delete(this.gameApiUrl + '/' + game.id, { withCredentials: true })
            ._catch(this.errorHandling.handleError);
    }
}