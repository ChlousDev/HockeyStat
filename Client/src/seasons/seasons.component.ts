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
  public isLoading: boolean = false;

  public newSeason: Season;

  constructor(private seasonProvider: SeasonProvider, private authenticationProvider: AuthenticationProvider) {
    this.isAdmin = this.authenticationProvider.isAdmin;
    this.authenticationProvider.isAdminSubject.subscribe((isAdmin) => {
      this.isAdmin = isAdmin;
    });
    this.init();
  }

  private init(): void {
    this.newSeason=new Season();
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
    if (season.startYear && season.startYear > 2010) {
      this.seasonProvider.saveSeason(season).subscribe(() => {
        this.init();
      }, () => {
        this.init();
      })
    }
  }

  public addSeason(): void {
    if (this.newSeason.startYear && this.newSeason.startYear > 2010) {
      this.seasonProvider.saveSeason(this.newSeason).subscribe(() => {
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