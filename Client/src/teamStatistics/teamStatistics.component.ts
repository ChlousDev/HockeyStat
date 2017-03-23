import { Component } from '@angular/core';

import { SeasonsOrderByPipe } from '../pipes/seasonsOrderBy.pipe';

import { TeamStatisticsProvider } from '../providers/teamStatistics.provider';
import { TeamProvider } from '../providers/team.provider';
import { SeasonProvider } from '../providers/season.provider';

import { Season } from '../model/season';
import { Team } from '../model/team';
import { TeamStatistics } from '../model/teamStatistics';

@Component({
  templateUrl: './teamStatistics.component.html'
})

export class TeamStatisticsComponent {
  public selectedSeason: Season;
  public seasons: Season[];
  public selectedTeam: Team;
  public teams: Team[];

  public isLoadingCatalogData: boolean = false;
  public isLoadingTeamStatistics: boolean = false;

  public teamStatistics: TeamStatistics;

  constructor(private teamStatisticsProvider: TeamStatisticsProvider, private seasonProvider: SeasonProvider, private teamProvider: TeamProvider) {
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
      this.loadTeamStatistics();
    })
  }

  private loadTeamStatistics(): void {
    if ((this.selectedTeam) && (this.selectedSeason)) {
      this.isLoadingTeamStatistics = true;
      this.teamStatisticsProvider.getTeamStatistics(this.selectedSeason.ID, this.selectedTeam.ID).subscribe(teamStatistics => {
        this.teamStatistics = teamStatistics;
        this.isLoadingTeamStatistics = false;
      })
    }
  }
}

