import { Pipe } from '@angular/core';

import {Team} from '../model/Team';

@Pipe({
  name: "teamsorderby",
  pure: true
})
export class TeamsOrderByPipe {
  transform(array: Array<Team>, args: string): Array<Team> {
    array.sort((a: Team, b: Team) => {
      if (a.name < b.name) {
        return -1;
      } else if (a.name > b.name) {
        return 1;
      } else {
        return 0;
      }
    });
    return array;
  }
}