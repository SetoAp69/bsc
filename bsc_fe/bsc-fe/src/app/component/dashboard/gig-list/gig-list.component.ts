import { Component, Inject, inject, OnInit } from '@angular/core';
import { GigService } from '../../../services/gig.service';
import { Gig } from '../../../interface/gig.interface';
import { GigQueryParam } from '../../../interface/gig-query-params.interface';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { FilterComponent } from '../../../shared/filter/filter.component';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-gig-list',
  standalone: true,
  imports: [NgbModule, FormsModule, CommonModule, FilterComponent, RouterLink],
  templateUrl: './gig-list.component.html',
  styleUrl: './gig-list.component.css',
})
export class GigListComponent implements OnInit {
  private gigService = inject(GigService);
  isLoading = false;
  showFilter = false;
  filterOptions: string[] = [
    'Graphic Design',
    'Web Development',
    'Copywriting',
  ];
  gigs: Gig[] = [];

  ngOnInit(): void {
    this.fetchGigList();
  }

  queryParams: GigQueryParam = {
    Search: null,
    Limit: null,
    Page: null,
    UserId: null,
    Types: [],
  };

  search() {
    this.fetchGigList();
  }

  updateFilter(filter: string[]) {
    this.queryParams.Types = filter;
    this.showFilter = false;
    this.fetchGigList();
  }

  fetchGigList() {
    this.isLoading = true;
    this.gigService.getGigs(this.queryParams).subscribe({
      next: (res) => {
        this.isLoading = false;
        this.gigs = res;
      },
    });
  }
}
