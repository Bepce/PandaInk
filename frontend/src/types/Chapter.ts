export interface Chapter {
  id: string;
  title: string;
  seriesId: string;
  content: string;
  pageNumber: number;
  prevChapterId?: string;
  nextChapterId?: string;
}