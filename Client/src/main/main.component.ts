import { Component } from '@angular/core';
import { MdDialog, MdDialogRef } from '@angular/material';
import { TranslateService } from '@ngx-translate/core';

import { LoginComponent } from '../login/login.component';
import { AuthenticationProvider } from '../providers/authentication.provider';

@Component({
  selector: 'app-root',
  templateUrl: './main.component.html'
})

export class MainComponent {

  public isAdmin: boolean;
  public currentLang: string;

  constructor(private translateService: TranslateService, private dialog: MdDialog, private authenticationProvider: AuthenticationProvider) {
    this.isAdmin = this.authenticationProvider.isAdmin;
    this.authenticationProvider.isAdminSubject.subscribe(isAdmin => {
      this.isAdmin = isAdmin;
    });
    this.translateService.setDefaultLang('en');
    this.setLanguage('de');
  }

  public openLoginDialog(): void {
    var dialogRef = this.dialog.open(LoginComponent);
  }

  public logout() {
    this.authenticationProvider.logout().subscribe(() => {
    });
  }

  public setLanguage(lang: string): void {
    this.currentLang = lang;
    this.translateService.use(lang);
  }

}
