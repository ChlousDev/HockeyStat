import { Game } from './game';

export class PagedList<T> {
    page: number;
    pageSize: number;
    totalItems: number;
    totalPages: number;
    items: T[];
}