import { Component } from '@angular/core';

import { SeasonsOrderByPipe } from '../pipes/seasonsOrderBy.pipe';

import { GameProvider } from '../providers/game.provider';
import { TeamProvider } from '../providers/team.provider';
import { SeasonProvider } from '../providers/season.provider';
import { AuthenticationProvider } from '../providers/authentication.provider';

import { Game } from '../model/game';
import { Season } from '../model/season';
import { Team } from '../model/team';

@Component({
  templateUrl: './games.component.html'
})

export class GamesComponent {
  public seasons: Season[];
  public selectedSeason: Season;
  public teams: Team[];
  public games: Game[];

  public currentPage: number;
  public totalPages: number;

  public isAdmin: boolean;
  public isLoadingCatalogData: boolean = false;
  public isLoadingGames: boolean = false;

  public newGame: Game;
  private lastGameDate: Date = new Date();

  constructor(private gameProvider: GameProvider, private teamProvider: TeamProvider, private seasonProvider: SeasonProvider,
    private authenticationProvider: AuthenticationProvider) {
    this.isAdmin = this.authenticationProvider.isAdmin;
    this.authenticationProvider.isAdminSubject.subscribe((isAdmin) => {
      this.isAdmin = isAdmin;
    });
    this.init();
  }

  private init(): void {
    this.newGame = new Game(this.lastGameDate);
    this.currentPage = 0;
    this.totalPages = 0;
    this.loadSeasons();
  }

  private reload(): void {
    this.newGame = new Game(this.lastGameDate);
    this.loadGames();
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
      this.loadGames();
    })
  }

  private loadGames(): void {
    if ((this.teams) && (this.selectedSeason)) {
      this.isLoadingGames = true;
      this.gameProvider.getGames(this.selectedSeason.ID, this.currentPage, 15).subscribe(pagedList => {
        this.games = pagedList.Items;
        this.totalPages = pagedList.TotalPages;
        this.games.forEach(game => {
          game.HomeTeam = this.teams.find(t => t.ID == game.HomeTeam.ID);
          game.GuestTeam = this.teams.find(t => t.ID == game.GuestTeam.ID);
        });
        this.isLoadingGames = false;
      })
    }
  }

  public previousPage(): void {
    if (this.currentPage > 0) {
      this.currentPage = this.currentPage - 1;
      this.loadGames();
    }
  }

  public nextPage(): void {
    if (this.currentPage < this.totalPages - 1) {
      this.currentPage = this.currentPage + 1;
      this.loadGames();
    }
  }

  public selectedSeasonChanged(): void {
    this.loadGames();
  }

  public convertToNumber(event): number {
    return +event;
  }

  public addGame(): void {
    if ((this.newGame.Date) && (this.selectedSeason) && (this.newGame.HomeTeam) && (this.newGame.GuestTeam) && (this.newGame.HomeScore !== null)
      && (this.newGame.GuestScore !== null) && (this.newGame.OTHomeScore !== null) && (this.newGame.OTGuestScore !== null) && (this.newGame.PSHomeScore !== null)
      && (this.newGame.PSGuestScore !== null)) {
        this.newGame.Season = this.selectedSeason;
        this.lastGameDate = this.newGame.Date;
        this.gameProvider.saveGame(this.newGame).subscribe(() => {
        this.reload();
      })
    }
  }

  public updateGame(game: Game): void {
    if ((game.Date) && (this.selectedSeason) && (game.HomeTeam) && (game.GuestTeam) && (game.HomeScore !== null)
      && (game.GuestScore !== null) && (game.OTHomeScore !== null) && (game.OTGuestScore !== null) && (game.PSHomeScore !== null)
      && (game.PSGuestScore !== null)) {
        this.gameProvider.saveGame(game).subscribe(() => {
        this.reload();
      })
    }
  }

  public deleteGame(game: Game): void {
    this.gameProvider.deleteGame(game).subscribe(() => {
      this.reload();
    });
  }
}