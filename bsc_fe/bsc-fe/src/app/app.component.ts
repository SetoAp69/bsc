import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { NavbarComponent } from "./component/navbar/navbar.component";
import { FilterComponent } from "./shared/filter/filter.component";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, NgbModule, NavbarComponent, FilterComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent {
  title = 'bsc-fe';
}
