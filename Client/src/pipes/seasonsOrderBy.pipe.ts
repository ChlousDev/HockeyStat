import { Pipe } from '@angular/core';

import {Season} from '../model/season';

@Pipe({
  name: "seasonsorderby",
  pure: true,
})
export class SeasonsOrderByPipe {
  transform(array: Array<Season>, args: string): Array<Season> {
    array.sort((a: Season, b: Season) => {
      if (a.startYear > b.startYear) {
        return -1;
      } else if (a.startYear < b.startYear) {
        return 1;
      } else {
        return 0;
      }
    });
    return array;
  }
}