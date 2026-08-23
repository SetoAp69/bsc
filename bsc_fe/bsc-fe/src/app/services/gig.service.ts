import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Gig, GigDetail, GigRating } from '../interfaces/gig.interface';
import { Observable } from 'rxjs';
import { environment } from '../../environment/env';
import { GigQueryParam } from '../interfaces/gig-query-params.interface';
import { GigRequest } from '../interfaces/gig-request';

@Injectable({
  providedIn: 'root',
})
export class GigService {
  private httpclient = inject(HttpClient);
  getGigs(queryParams: GigQueryParam): Observable<Gig[]> {
    return this.httpclient.get<Gig[]>(`${environment.apiUrl}/gigs`, {
      params: {
        Search: queryParams.Search ?? '',
        Limit: queryParams.Limit ?? 1,
        Page: queryParams.Page ?? 1,
        UserId: queryParams.UserId ?? '',
        Types: queryParams.Types,
      },
    });
  }

  getGigById(id: number) {
    return this.httpclient.get<GigDetail>(`${environment.apiUrl}/gigs/${id}`);
  }

  getGigRatings(id: number) {
    return this.httpclient.get<GigRating[]>(
      `${environment.apiUrl}/gigs/${id}/ratings`,
    );
  }

  createGig(request: GigRequest) {
    return this.httpclient.post(`${environment.apiUrl}/gigs`, request);
  }
}
