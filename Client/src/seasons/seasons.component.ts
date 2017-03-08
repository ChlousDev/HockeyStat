import { Component } from '@angular/core';

import { SeasonProvider } from '../providers/season.provider';
import { AuthenticationProvider } from '../providers/authentication.provider';

import { Season } from '../model/season';

@Component({
  templateUrl: './seasons.component.html'
})

export class SeasonsComponent {

  public seasons: Season[];
  public isAdmin: boolean;
  public newSeasonStartYear: number;
  public isLoading: boolean = false;

  constructor(private seasonProvider: SeasonProvider, private authenticationProvider: AuthenticationProvider) {
    this.isAdmin = this.authenticationProvider.isAdmin;
    this.authenticationProvider.isAdminSubject.subscribe((isAdmin) => {
      this.isAdmin = isAdmin;
    });
    this.init();
  }

  private init(): void {
    this.newSeasonStartYear = null;
    this.loadSeasons();
  }

  private loadSeasons(): void {
    this.isLoading = true;
    this.seasonProvider.getSeasons().subscribe(seasons => {
      this.seasons = seasons;
      this.isLoading = false;
    })
  }

  public updateSeason(season: Season): void {
    if (season.StartYear && season.StartYear > 2010) {
      this.seasonProvider.saveSeason(season).subscribe(() => {
        this.init();
      }, () => {
        this.init();
      })
    }
  }

  public addSeason(): void {
    if (this.newSeasonStartYear && this.newSeasonStartYear > 2010) {
      this.seasonProvider.saveSeason({ ID: 0, StartYear: this.newSeasonStartYear }).subscribe(() => {
        this.init();
      }, () => {
        this.init();
      })
    }
  }

  public convertToNumber(event): number {
    return +event;
  }

}