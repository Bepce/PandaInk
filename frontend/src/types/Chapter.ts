import { Page } from "./Page";

export interface Chapter {
    id: string;
    seriesId: string;
    title: string;
    content: Page[];
}