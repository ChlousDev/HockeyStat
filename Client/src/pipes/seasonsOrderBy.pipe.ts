import { Pipe } from '@angular/core';

import {Season} from '../model/Season';

@Pipe({
  name: "seasonsorderby",
  pure: true,
})
export class SeasonsOrderByPipe {
  transform(array: Array<Season>, args: string): Array<Season> {
    array.sort((a: Season, b: Season) => {
      if (a.StartYear > b.StartYear) {
        return -1;
      } else if (a.StartYear < b.StartYear) {
        return 1;
      } else {
        return 0;
      }
    });
    return array;
  }
}