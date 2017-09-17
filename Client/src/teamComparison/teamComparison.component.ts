import { Component } from '@angular/core';

import { SeasonsOrderByPipe } from '../pipes/seasonsOrderBy.pipe';

import { TeamComparisonProvider } from '../providers/teamComparison.provider';
import { TeamProvider } from '../providers/team.provider';
import { SeasonProvider } from '../providers/season.provider';

import { Season } from '../model/season';
import { Team } from '../model/team';
import { TeamComparison } from '../model/teamComparison';

@Component({
  templateUrl: './teamComparison.component.html'
})



export class TeamComparisonComponent {
  public selectedSeason: Season;
  public seasons: Season[];
  public team1: Team;
  public team2: Team;
  public teams: Team[];

  public isLoadingCatalogData: boolean = false;
  public isLoadingTeamComparison: boolean = false;

  public teamComparison: TeamComparison;

  constructor(private teamComparisonProvider: TeamComparisonProvider, private seasonProvider: SeasonProvider, private teamProvider: TeamProvider) {
    this.init();
  }

  private init(): void {
    this.loadSeasons();
  }

  private loadSeasons(): void {
    this.isLoadingCatalogData = true;
    this.seasonProvider.getSeasons().subscribe(seasons => {
      this.seasons = new SeasonsOrderByPipe().transform(seasons, null);
      this.selectedSeason = this.seasons[0];
      this.isLoadingCatalogData = false;
      this.loadTeams();
    })
  }

  private loadTeams(): void {
    this.isLoadingCatalogData = true;
    this.teamProvider.getTeams().subscribe(teams => {
      this.teams = teams;
      this.isLoadingCatalogData = false;
      this.loadTeamComparison();
    })
  }

  private loadTeamComparison(): void {
    if ((this.team1) && (this.team2) && (this.selectedSeason)) {
      this.isLoadingTeamComparison = true;
      this.teamComparisonProvider.getTeamComparison(this.selectedSeason.id, this.team1.id, this.team2.id).subscribe(teamComparison => {
        this.teamComparison = teamComparison;
        this.isLoadingTeamComparison = false;
      })
    }
  }
}