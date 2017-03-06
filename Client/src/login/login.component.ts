import { Component } from '@angular/core';

import { AuthenticationProvider } from '../providers/authentication.provider';

@Component({
    templateUrl: './login.component.html'    
})

export class LoginComponent {

    public userName: string;
    public password: string;

    constructor(private authenticationProvider: AuthenticationProvider) {
    }

    private login(): void {
        this.authenticationProvider.login(this.userName, this.password).subscribe(user => {
        })
    }
    
}