import { Component, Inject, inject, Input, input, OnInit } from '@angular/core';
import { GigService } from '../../../services/gig.service';
import { Gig } from '../../../interface/gig.interface';
import { GigQueryParam } from '../../../interface/gig-query-params.interface';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { FilterComponent } from '../../../shared/filter/filter.component';
import { RouterLink } from '@angular/router';
import { AuthService } from '../../../services/auth.service';
import { UserRole } from '../../../enums/user-role';
import { LoadingComponent } from '../../../components/loading/loading.component';

@Component({
  selector: 'app-gig-list',
  standalone: true,
  imports: [NgbModule, FormsModule, CommonModule, FilterComponent, RouterLink, LoadingComponent],
  templateUrl: './gig-list.component.html',
  styleUrl: './gig-list.component.css',
})
export class GigListComponent implements OnInit {
  private gigService = inject(GigService);
  private authService = inject(AuthService);
  userId = this.authService.getUserId();
  user= this.authService.getUser()
  userRole = this.user?.userRole;
  isServiceProvider = this.userRole == UserRole.SERVICE_PROVIDER;
  isLoading = false;
  showFilter = false;
  filterOptions: string[] = [
    'Graphic Design',
    'Web Development',
    'Copywriting',
  ];
  gigs: Gig[] = [];

  ngOnInit(): void {
    this.queryParams.UserId = this.isServiceProvider?this.userId:null;
    this.fetchGigList();
  }

  queryParams: GigQueryParam = {
    Search: null,
    Limit: null,
    Page: null,
    UserId: this.userId,
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
