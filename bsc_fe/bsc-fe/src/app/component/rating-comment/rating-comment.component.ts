import { Component, Input } from '@angular/core';
import { GigRating } from '../../interface/gig.interface';

@Component({
  selector: 'app-rating-comment',
  standalone: true,
  imports: [],
  templateUrl: './rating-comment.component.html',
  styleUrl: './rating-comment.component.css',
})
export class RatingCommentComponent {
  @Input() rating: GigRating = {
    id: 0,
    userName: 'John Doe',
    rating: 1.5,
    comment: 'Hehehe'
  }
  
}
