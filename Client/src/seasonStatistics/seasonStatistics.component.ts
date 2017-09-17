import { Component } from '@angular/core';

import { SeasonsOrderByPipe } from '../pipes/seasonsOrderBy.pipe';

import { SeasonStatisticsProvider } from '../providers/seasonStatistics.provider';
import { SeasonProvider } from '../providers/season.provider';

import { Season } from '../model/season';
import { SeasonStatistics } from '../model/seasonStatistics';

@Component({
  templateUrl: './seasonStatistics.component.html'
})

export class SeasonStatisticsComponent {
  public selectedSeason: Season;
  public seasons: Season[];
  
  public isLoadingCatalogData: boolean = false;
  public isLoadingSeasonStatistics: boolean = false;

  public seasonStatistics: SeasonStatistics;

  constructor(private seasonStatisticsProvider: SeasonStatisticsProvider, private seasonProvider: SeasonProvider) {
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
      this.loadSeasonStatistics();
    })
  }

  private loadSeasonStatistics(): void {
    if (this.selectedSeason) {
      this.isLoadingSeasonStatistics = true;
      this.seasonStatisticsProvider.getSeasonStatistics(this.selectedSeason.id).subscribe(seasonStatistics => {
        this.seasonStatistics = seasonStatistics;
        this.isLoadingSeasonStatistics = false;
      })
    }
  }
}

