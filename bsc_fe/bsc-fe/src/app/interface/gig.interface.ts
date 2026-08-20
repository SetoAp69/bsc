export interface Gig {
  id: number;
  name: string;
  price: number;
  stars: number;
  gigCreator: string;
  types: string[];
}

export interface GigDetail {
  id: number;
  name: string;
  description: string;
  duration: number;
  price: number;
  stars: number;
  gigCreator: GigCreator;
  types: Type[];
}

export interface GigCreator {
  id: number;
  name: string;
}

export interface Type {
  id: number;
  name: string;
}

export interface GigRating {
  id: number
  userName: string
  rating: number
  comment: string
}