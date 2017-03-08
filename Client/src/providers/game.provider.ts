import { Injectable, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { Observable } from 'rxjs/Rx';

import { APP_CONFIG } from '../app/app.config.token';
import { IAppConfig } from '../app/app.config.interface';

import { ApiErrorHandlingProvider } from './apiErrorHandling.provider';

import { Game } from '../model/game';
import { PagedList } from '../model/pagedList';

@Injectable()
export class GameProvider {

    private gameApiUrl: string;

    constructor(private http: Http, @Inject(APP_CONFIG) private config: IAppConfig, private errorHandling: ApiErrorHandlingProvider) {
        this.gameApiUrl = this.config.apiEndpoint + 'game';
    }

    public getGames(seasonID: number, page: number, pageSize: number): Observable<PagedList<Game>> {
        return this.http.get(this.gameApiUrl + '/season/' + seasonID + '/paged/' + page + '/' + pageSize)
            .map((response) => response.json())
            ._catch(this.errorHandling.handleError);
    }

    public saveGame(game: Game): Observable<void> {
        var result: Observable<void> = null;
        if (game.ID > 0) {
            result = this.http.put(this.gameApiUrl, game, { withCredentials: true })
                .map((response) => response.json())
                ._catch(this.errorHandling.handleError);
        }
        else {
            result = this.http.post(this.gameApiUrl, game, { withCredentials: true })
                .map((response) => response.json())
                ._catch(this.errorHandling.handleError);
        }
        return result;
    }

    public deleteGame(game: Game): Observable<void> {
        return this.http.delete(this.gameApiUrl + '/' + game.ID, { withCredentials: true })
            .map((response) => response.json())
            ._catch(this.errorHandling.handleError);
    }
}