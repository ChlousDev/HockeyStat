import { Game } from './game';

export class PagedList<T> {
    Page: number;
    PageSize: number;
    TotalItems: number;
    TotalPages: number;
    Items: T[];
}