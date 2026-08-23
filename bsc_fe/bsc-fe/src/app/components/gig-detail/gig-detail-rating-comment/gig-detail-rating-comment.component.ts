import { Component, inject, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { RatingCommentComponent } from '../rating-comment/rating-comment.component';
import { GigService } from '../../../services/gig.service';
import { GigRating } from '../../../interfaces/gig.interface';

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
    this.gigId = +(this.route.snapshot.paramMap.get('gigId') ?? '');
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
