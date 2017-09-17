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
      this.gameProvider.getGames(this.selectedSeason.id, this.currentPage, 15).subscribe(pagedList => {
        this.games = pagedList.items;
        this.totalPages = pagedList.totalPages;
        this.games.forEach(game => {
          game.homeTeam = this.teams.find(t => t.id == game.homeTeam.id);
          game.guestTeam = this.teams.find(t => t.id == game.guestTeam.id);
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
    if ((this.newGame.date) && (this.selectedSeason) && (this.newGame.homeTeam) && (this.newGame.guestTeam) && (this.newGame.homeScore !== null)
      && (this.newGame.guestScore !== null) && (this.newGame.otHomeScore !== null) && (this.newGame.otGuestScore !== null) && (this.newGame.psHomeScore !== null)
      && (this.newGame.psGuestScore !== null)) {
        this.newGame.season = this.selectedSeason;
        this.lastGameDate = this.newGame.date;
        this.gameProvider.saveGame(this.newGame).subscribe(() => {
        this.reload();
      })
    }
  }

  public updateGame(game: Game): void {
    if ((game.date) && (this.selectedSeason) && (game.homeTeam) && (game.guestTeam) && (game.homeScore !== null)
      && (game.guestScore !== null) && (game.otHomeScore !== null) && (game.otGuestScore !== null) && (game.psHomeScore !== null)
      && (game.psGuestScore !== null)) {
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