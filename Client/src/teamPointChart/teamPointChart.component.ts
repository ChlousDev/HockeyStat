import { Component } from '@angular/core';

import { TranslateService } from '@ngx-translate/core';

import { SeasonsOrderByPipe } from '../pipes/seasonsOrderBy.pipe';

import { TeamPointChartProvider } from '../providers/teamPointChart.provider';
import { TeamProvider } from '../providers/team.provider';
import { SeasonProvider } from '../providers/season.provider';

import { Season } from '../model/season';
import { TeamSelection } from '../model/TeamSelection';
import { TeamPointChart } from '../model/TeamPointChart';


@Component({
  templateUrl: './teamPointChart.component.html'
})

export class TeamPointChartComponent {
  public selectedSeason: Season;
  public seasons: Season[];
  public teams: TeamSelection[];
  public teamPointChart: TeamPointChart;
  public showEightPlace: boolean = false;
  public showAll: boolean;

  public isLoadingCatalogData: boolean = false;
  public isLoadingTeamPointChart: boolean = false;

  public showChart: boolean;
  public lineChartData: Array<any>;
  public lineChartLabels: Array<any>;
  public chartOptions: any = { responsive: true, animation: false, legend: { position: 'bottom', fill: false, labels: { usePointStyle: true } }, elements: { line: { tension: 0, backgroundColor: null, fill: false } } };
  private eightPlaceLabel: string;

  constructor(private teamPointChartProvider: TeamPointChartProvider, private teamProvider: TeamProvider, private seasonProvider: SeasonProvider, private translateService: TranslateService) {
    this.init();
    this.translateService.onLangChange.subscribe((event)=>{
      this.loadLabels();
    })
  }

  private loadLabels(): void {
    this.translateService.get("EightPlace").subscribe(eightPlaceLabel => {
      this.eightPlaceLabel = eightPlaceLabel;
      this.setChartData();
    })
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
      this.teams = [];
      teams.forEach((team) => { this.teams.push({ TeamName: team.ShortName, TeamID: team.ID, IsSelected: true }); });
      this.isLoadingCatalogData = false;
      this.loadTeamPointChart();
    })
  }

  private loadTeamPointChart(): void {
    if ((this.selectedSeason)) {
      this.isLoadingTeamPointChart = true;
      this.teamPointChartProvider.getTeamPointChart(this.selectedSeason.ID).subscribe(teamPointChart => {
        this.teamPointChart = teamPointChart;
        this.loadLabels();
      })
    }
  }

  public toggleShowAll(): void {
    this.teams.forEach(t => t.IsSelected = this.showAll);
    this.setChartData();
  }

  public setChartData(): void {
    this.showChart = false;
    this.showAll = true;
    var lineChartData = [];
    var lineChartLabels = [];
    var maxLabel: number = -1;
    var showSerie: boolean;
    var serieLabel: string;
    this.teamPointChart.Series.forEach(serie => {
      if (serie.Team) {
        showSerie = this.teams.findIndex(t => t.TeamID == serie.Team.ID && t.IsSelected) >= 0;
        serieLabel = serie.Team.ShortName;
        this.showAll = this.showAll && showSerie;
      }
      else {
        showSerie = this.showEightPlace;
        serieLabel = this.eightPlaceLabel;
      }
      if (showSerie) {
        var data: number[] = [];
        var label: number = 0;
        serie.Points.forEach(points => {
          data.push(points);
          if (label > maxLabel) {
            lineChartLabels.push('' + label);
            maxLabel = label;
          }
          label++;
        });
        lineChartData.push({ data: data, label: serieLabel });
        this.showChart = true;
      }
      else {
        lineChartData.push({ data: [], label: serieLabel });
      }
    });
    if (this.showChart) {
      this.lineChartData = lineChartData;
      this.lineChartLabels = lineChartLabels
    }
    this.isLoadingTeamPointChart = false;
  }
}