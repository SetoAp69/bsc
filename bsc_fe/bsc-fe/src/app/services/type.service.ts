import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../environments/environment';
import { Type } from '../interfaces/type';

@Injectable({
  providedIn: 'root'
})
export class TypeService {
  http = inject(HttpClient)
  getTypes() {
    return this.http.get<Type[]>(environment.apiUrl + '/types')
  }
}
