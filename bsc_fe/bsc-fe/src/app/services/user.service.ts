import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../environment/env';
import { Observable } from 'rxjs';
import { User } from '../interfaces/user.interface';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private baseUrl = `${environment.apiUrl}/user`;
  private httpclient = inject(HttpClient);
  getUserProfile(id: number): Observable<User> {
    return this.httpclient.get<User>(`${this.baseUrl}/${id}`);
  }
}
