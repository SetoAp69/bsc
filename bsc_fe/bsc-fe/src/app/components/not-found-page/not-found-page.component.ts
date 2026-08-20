import { Component, inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { delay, of, switchMap } from 'rxjs';

@Component({
  selector: 'app-not-found-page',
  standalone: true,
  imports: [],
  templateUrl: './not-found-page.component.html',
  styleUrl: './not-found-page.component.css'
})
export class NotFoundPageComponent implements OnInit {
  route = inject(Router);

  ngOnInit(): void {
    of(null).pipe(
      delay(3000),
      switchMap(() => {
        this.route.navigate(['/login']);
        return of(null);
      })
    ).subscribe();
  }
}
