import { Component, inject, Input, OnInit } from '@angular/core';
import { GigService } from '../../services/gig.service';
import { GigRating } from '../../interface/gig.interface';
import { RatingCommentComponent } from '../rating-comment/rating-comment.component';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-gig-detail-rating-comment',
  standalone: true,
  imports: [RatingCommentComponent],
  templateUrl: './gig-detail-rating-comment.component.html',
  styleUrl: './gig-detail-rating-comment.component.css',
})
export class GigDetailRatingCommentComponent implements OnInit {
  gigService = inject(GigService);
  route = inject(ActivatedRoute);
  isLoading = false;
  ratings: GigRating[] = [];
  gigId: number = 0;
  ngOnInit(): void {
    this.gigId = +(this.route.parent?.snapshot.paramMap.get('gigId') ?? '');
    this.fetchRatings();
  }

  fetchRatings() {
    this.isLoading;
    this.gigService.getGigRatings(this.gigId).subscribe({
      next: (res) => {
        this.isLoading = false;
        this.ratings = res;
      },
      error: (er) => {
        console.log(er);
        this.isLoading = false;
      },
    });
  }
}
