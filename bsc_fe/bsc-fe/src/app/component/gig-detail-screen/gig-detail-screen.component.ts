import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { GigDetail, GigRating } from '../../interface/gig.interface';
import { GigService } from '../../services/gig.service';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import { GigDetailRatingCommentComponent } from "../gig-detail-rating-comment/gig-detail-rating-comment.component";

@Component({
  selector: 'app-gig-detail-screen',
  standalone: true,
  imports: [CommonModule, GigDetailRatingCommentComponent, RouterLink],
  templateUrl: './gig-detail-screen.component.html',
  styleUrl: './gig-detail-screen.component.css',
})
export class GigDetailScreenComponent implements OnInit {
  ngOnInit(): void {
    this.fetchDetail(this.id);
  }
  route = inject(ActivatedRoute);
  id = +(this.route.snapshot.paramMap.get('id') ?? '');
  gigService = inject(GigService);
  isDetailFailed = false;
  isDetailLoading = false;
  isRatingsLoading = false;
  gigDetail: GigDetail = {
    id: 0,
    name: '',
    description: '',
    duration: 0,
    price: 0,
    stars: 0,
    gigCreator: {
      id: 0,
      name: '',
    },
    types: [],
  };
  ratings: GigRating[] = [];
  fetchDetail(id: number) {
    this.isDetailLoading = true;
    this.gigService.getGigById(id).subscribe({
      next: (res) => {
        this.isDetailLoading = false;
        this.isDetailFailed = false;
        this.gigDetail = res;
      },
      error: (res) => {
        this.isDetailFailed = true;
      },
    });
  }
}
