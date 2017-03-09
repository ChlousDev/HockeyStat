import { Component } from '@angular/core';

import { SeasonsOrderByPipe } from '../pipes/seasonsOrderBy.pipe';

import { StandingsProvider } from '../providers/standings.provider';
import { SeasonProvider } from '../providers/season.provider';

import { Season } from '../model/season';
import { Standings } from '../model/standings';

@Component({
  templateUrl: './standings.component.html'
})

export class StandingsComponent {
  public selectedSeason: Season;
  public date: Date;
  public seasons: Season[];

  public isLoadingCatalogData: boolean = false;
  public isLoadingStandings: boolean = false;

  public standings: Standings;

  constructor(private standingsProvider: StandingsProvider, private seasonProvider: SeasonProvider) {
    this.init();
  }

  private init(): void {
    this.date = new Date();
    this.loadSeasons();
  }

  private loadSeasons(): void {
    this.isLoadingCatalogData = true;
    this.seasonProvider.getSeasons().subscribe(seasons => {
      this.seasons = new SeasonsOrderByPipe().transform(seasons, null);
      this.selectedSeason = this.seasons[0];
      this.isLoadingCatalogData = false;
      this.loadStandings();
    })
  }

  private loadStandings(): void {
    if ((this.date) && (this.selectedSeason))
      this.isLoadingStandings = true;
    this.standingsProvider.getStandings(this.selectedSeason.ID, this.date).subscribe(standings => {
      this.standings = standings;
      this.isLoadingStandings = false;
    })
  }
}