export interface User {
  id: number;
  username: string;
  name: string;
  email: string;
  about: string | null;
  location: string | null;
  userRole: string;
  rating: Rating | null;
}

export interface Rating {
  stars: number;
  count: number;
}
