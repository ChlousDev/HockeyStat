import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { MaterialModule } from '@angular/material';
import { RouterModule } from '@angular/router';
import { FlexLayoutModule } from "@angular/flex-layout";

import { MainComponent } from '../main/main.component';
import { ComparisonComponent } from '../comparison/comparison.component';
import { GamesComponent } from '../games/games.component';
import { SeasonsComponent } from '../seasons/seasons.component';
import { StandingsComponent } from '../standings/standings.component';
import { TeamPointChartComponent } from '../teamPointChart/teamPointChart.component';
import { TeamsComponent } from '../teams/teams.component';
import { TeamStatisticsComponent } from '../teamStatistics/teamStatistics.component';

@NgModule({
  declarations: [
    MainComponent,
    ComparisonComponent,
    GamesComponent,
    SeasonsComponent,
    TeamPointChartComponent,
    StandingsComponent,
    TeamPointChartComponent,
    TeamsComponent,
    TeamStatisticsComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule,
    MaterialModule.forRoot(),
    FlexLayoutModule.forRoot(),
    RouterModule.forRoot([
      {
        path: 'comparison',
        component: ComparisonComponent,
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
  providers: [],
  bootstrap: [ MainComponent, ]
})
export class AppModule { }
