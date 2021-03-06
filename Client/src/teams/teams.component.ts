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
  public isLoading: boolean = false;

  public newTeam: Team;

  constructor(private teamProvider: TeamProvider, private authenticationProvider: AuthenticationProvider) {
    this.isAdmin = this.authenticationProvider.isAdmin;
    this.authenticationProvider.isAdminSubject.subscribe((isAdmin) => {
      this.isAdmin = isAdmin;
    });
    this.init();
  }

  private init(): void {
    this.newTeam = new Team();
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
    if ((team.shortName && team.shortName.length > 0) && (team.name && team.name.length > 0)) {
      this.teamProvider.saveTeam(team).subscribe(() => {
        this.init();
      }, () => {
        this.init();
      })
    }
  }

  public addTeam(): void {
    if ((this.newTeam.shortName && this.newTeam.shortName.length > 0) && (this.newTeam.name && this.newTeam.name.length > 0)) {
      this.teamProvider.saveTeam(this.newTeam).subscribe(() => {
        this.init();
      }, () => {
        this.init();
      })
    }
  }
}