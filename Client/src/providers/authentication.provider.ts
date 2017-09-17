import { Injectable, Inject } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Observable, Subject } from 'rxjs/Rx';

import { environment } from '../environments/environment';

import { ApiErrorHandlingProvider } from './apiErrorHandling.provider';

import { User } from '../model/user';

@Injectable()
export class AuthenticationProvider {

    private userApiUrl: string;
    private user: User;
    public isAdmin: boolean;
    public isAdminSubject: Subject<boolean> = new Subject();

    constructor(private http: Http, private errorHandling: ApiErrorHandlingProvider) {
        this.userApiUrl = environment.apiEndpoint + 'user';
        this.setIsAdmin();
    }

    public login(userName: string, password: string): Observable<User> {
       var result: Observable<User> = this.http.get(this.userApiUrl + '/login?UserName=' + userName + '&Password=' + password, { withCredentials: true })
            .map((response) => response.json())
            ._catch(this.errorHandling.handleError);
        result.subscribe((user) => {
            this.user = user;
            this.setIsAdmin();
        })
        return result;
    }

    public logout(): Observable<void> {
        var result: Observable<void> = this.http.get(this.userApiUrl + '/logout', { withCredentials: true })
            ._catch(this.errorHandling.handleError);
        result.subscribe(() => {
            this.user = null;
            this.setIsAdmin(); 
        });
        return result;
    }

    public setIsAdmin(): void {
        if (this.user != null) {
            this.isAdmin = this.user.isAdmin
        }
        else {
            this.isAdmin = false;
        }
        this.isAdminSubject.next(this.isAdmin);
    }
}