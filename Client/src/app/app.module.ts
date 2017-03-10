import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { MaterialModule } from '@angular/material';
import { RouterModule } from '@angular/router';
import { FlexLayoutModule } from "@angular/flex-layout";
import { Md2Module } from 'md2';

import { AppConfig } from './app.config';
import { APP_CONFIG } from './app.config.token';

import { TeamProvider } from '../providers/team.provider';
import { SeasonProvider } from '../providers/season.provider';
import { GameProvider } from '../providers/game.provider';
import { StandingsProvider } from '../providers/standings.provider';
import { TeamComparisonProvider} from '../providers/teamComparison.provider';
import { TeamStatisticsProvider} from '../providers/teamStatistics.provider';
import { AuthenticationProvider } from '../providers/authentication.provider';
import { ApiErrorHandlingProvider } from '../providers/apiErrorHandling.provider';

import { MainComponent } from '../main/main.component';
import { TeamComparisonComponent } from '../teamComparison/teamComparison.component';
import { GamesComponent } from '../games/games.component';
import { SeasonsComponent } from '../seasons/seasons.component';
import { StandingsComponent } from '../standings/standings.component';
import { TeamPointChartComponent } from '../teamPointChart/teamPointChart.component';
import { TeamsComponent } from '../teams/teams.component';
import { TeamStatisticsComponent } from '../teamStatistics/teamStatistics.component';
import { LoginComponent } from '../login/login.component';

import { SeasonsOrderByPipe } from '../pipes/seasonsOrderBy.pipe';
import { TeamsOrderByPipe } from '../pipes/teamsOrderBy.pipe';
import { GamesOrderByPipe } from '../pipes/gamesOrderBy.pipe';


@NgModule({
  declarations: [
    MainComponent,
    TeamComparisonComponent,
    GamesComponent,
    SeasonsComponent,
    TeamPointChartComponent,
    StandingsComponent,
    TeamPointChartComponent,
    TeamsComponent,
    TeamStatisticsComponent,
    LoginComponent,
    SeasonsOrderByPipe,
    TeamsOrderByPipe,
    GamesOrderByPipe
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule,
    MaterialModule.forRoot(),
    FlexLayoutModule.forRoot(),
    Md2Module.forRoot(),
    RouterModule.forRoot([
      {
        path: 'teamComparison',
        component: TeamComparisonComponent,
      },
      {
        path: 'games',
        component: GamesComponent,
      },
      {
        path: 'seasons',
        component: SeasonsComponent,
      },
      {
        path: 'standings',
        component: StandingsComponent,
      },
      {
        path: 'teamPointChart',
        component: TeamPointChartComponent,
      },
      {
        path: 'teams',
        component: TeamsComponent,
      },
      {
        path: 'teamStatistics',
        component: TeamStatisticsComponent,
      },
      {
        path: '',
        redirectTo: '/standings',
        pathMatch: 'full'
      },
      {
        path: '**',
        redirectTo: '/standings'
      },
    ])
  ],
  providers: [
    { provide: APP_CONFIG, useValue: AppConfig },
    ApiErrorHandlingProvider,
    TeamProvider,
    SeasonProvider,
    GameProvider,
    StandingsProvider,
    TeamComparisonProvider,
    TeamStatisticsProvider,
    AuthenticationProvider
  ],
  entryComponents: [LoginComponent],
  bootstrap: [MainComponent]
})
export class AppModule { }
