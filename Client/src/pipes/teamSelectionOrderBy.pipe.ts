import { Pipe } from '@angular/core';

import {TeamSelection} from '../model/TeamSelection';

@Pipe({
  name: "teamsselectionorderby",
  pure: true
})
export class TeamSelectionOrderByPipe {
  transform(array: Array<TeamSelection>, args: string): Array<TeamSelection> {
    array.sort((a: TeamSelection, b: TeamSelection) => {
      if (a.teamName < b.teamName) {
        return -1;
      } else if (a.teamName > b.teamName) {
        return 1;
      } else {
        return 0;
      }
    });
    return array;
  }
}