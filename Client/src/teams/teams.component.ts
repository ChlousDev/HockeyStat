import { Component } from '@angular/core';

import { TeamProvider } from '../providers/team.provider';
import { AuthenticationProvider } from "../providers/authentication.provider"

import { Team } from '../model/team';

@Component({
  templateUrl: './teams.component.html'
})

export class TeamsComponent {

  public teams: Team[];
  public isAdmin: boolean;
  public newTeamShortName: string;
  public newTeamName: string;
  public isLoading: boolean = false;

  constructor(private teamProvider: TeamProvider, private authenticationProvider: AuthenticationProvider) {
    this.isAdmin = this.authenticationProvider.isAdmin;
    this.authenticationProvider.isAdminSubject.subscribe((isAdmin) => {
      this.isAdmin = isAdmin;
    })
    this.init();
  }

  private init(): void {
    this.newTeamName="";
    this.newTeamShortName="";
    this.loadTeams();
  }

  private loadTeams(): void {
    this.isLoading = true;
    this.teamProvider.getTeams().subscribe(teams => {
      this.teams = teams;
      this.isLoading = false;
    })
  }

  public updateTeam(team: Team): void {
    this.teamProvider.saveTeam(team).subscribe(() => {
      this.init();
    }, () => {
      this.init();
    })
  }

  public addTeam(): void {
    this.teamProvider.saveTeam({ ID: 0, Name: this.newTeamName, ShortName: this.newTeamShortName }).subscribe(() => {
      this.init();
    }, () => {
      this.init();
    })
  }
}