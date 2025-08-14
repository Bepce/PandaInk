export interface SeriesDetails {
  id: string;
  title: string;
  genre: string;
  descriptionm: string;
  author: string;
  releaseDate: string;
  score: string;
  coverImage: string;
  chapters?: { id: string; title: string }[];
}