import { Pipe } from '@angular/core';

import {Game} from '../model/game';

@Pipe({
  name: "gamesorderby",
  pure: true,
})
export class GamesOrderByPipe {
  transform(array: Array<Game>, args: string): Array<Game> {
    array.sort((a: Game, b: Game) => {
      if (a.date > b.date) {
        return -1;
      } else if (a.date < b.date) {
        return 1;
      } else {
        return 0;
      }
    });
    return array;
  }
}