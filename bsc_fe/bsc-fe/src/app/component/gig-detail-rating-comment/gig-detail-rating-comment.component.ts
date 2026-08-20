import { Component, inject, Input, OnInit } from '@angular/core';
import { GigService } from '../../services/gig.service';
import { GigRating } from '../../interface/gig.interface';
import { RatingCommentComponent } from "../rating-comment/rating-comment.component";

@Component({
  selector: 'app-gig-detail-rating-comment',
  standalone: true,
  imports: [RatingCommentComponent],
  templateUrl: './gig-detail-rating-comment.component.html',
  styleUrl: './gig-detail-rating-comment.component.css',
})
export class GigDetailRatingCommentComponent implements OnInit {
  gigService = inject(GigService);
  isLoading = false;
  ratings: GigRating[] = [];
  ngOnInit(): void {
    this.fetchRatings();
  }
  @Input() id: number = 0;

  fetchRatings() {
    this.isLoading;
    this.gigService.getGigRatings(this.id).subscribe({
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
