import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule, Http } from '@angular/http';
import { MaterialModule } from '@angular/material';
import { RouterModule } from '@angular/router';
import { FlexLayoutModule } from "@angular/flex-layout";
import { Md2Module } from 'md2';
import { ChartsModule } from 'ng2-charts';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';

import { AppConfig } from './app.config';
import { APP_CONFIG } from './app.config.token';

import { TeamProvider } from '../providers/team.provider';
import { SeasonProvider } from '../providers/season.provider';
import { GameProvider } from '../providers/game.provider';
import { StandingsProvider } from '../providers/standings.provider';
import { TeamComparisonProvider } from '../providers/teamComparison.provider';
import { TeamStatisticsProvider } from '../providers/teamStatistics.provider';
import { TeamPointChartProvider } from '../providers/teamPointChart.provider';
import { SeasonStatisticsProvider } from '../providers/seasonStatistics.provider';
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
import { SeasonStatisticsComponent } from '../seasonStatistics/seasonStatistics.component';
import { LoginComponent } from '../login/login.component';

import { SeasonsOrderByPipe } from '../pipes/seasonsOrderBy.pipe';
import { TeamsOrderByPipe } from '../pipes/teamsOrderBy.pipe';
import { GamesOrderByPipe } from '../pipes/gamesOrderBy.pipe';
import { TeamSelectionOrderByPipe } from '../pipes/teamSelectionOrderBy.pipe';

export function HttpLoaderFactory(http: Http) {
  return new TranslateHttpLoader(http, './assets/translations/texts.', '.json');
}

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
    SeasonStatisticsComponent,
    LoginComponent,
    SeasonsOrderByPipe,
    TeamsOrderByPipe,
    GamesOrderByPipe,
    TeamSelectionOrderByPipe
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule,
    ChartsModule,
    MaterialModule.forRoot(),
    FlexLayoutModule.forRoot(),
    Md2Module.forRoot(),
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: HttpLoaderFactory,
        deps: [Http]
      }
    }),
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
        path: 'seasonStatistics',
        component: SeasonStatisticsComponent,
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
    TeamPointChartProvider,
    SeasonStatisticsProvider,
    AuthenticationProvider
  ],
  entryComponents: [LoginComponent],
  bootstrap: [MainComponent]
})
export class AppModule { }
