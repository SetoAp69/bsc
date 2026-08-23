import { Component, EventEmitter, inject, Input, OnInit, Output } from '@angular/core';
import { Rating } from '../../../interfaces/rating';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';

@Component({
  selector: 'app-edit-rating',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './edit-rating.component.html',
  styleUrl: './edit-rating.component.css'
})
export class EditRatingComponent implements OnInit {
  @Output() ratingChanged = new EventEmitter<Rating>();
  @Input() initialRating: Rating | null = null;
  currentRating: Rating | null = null;
  fb = inject(FormBuilder);
  editRatingForm = this.fb.group({
    rating: [0, [Validators.required, Validators.min(1), Validators.max(5)]],
    comment: ['', [Validators.required, Validators.minLength(5)]]
  })

  ngOnInit(): void {
    if (this.initialRating) {
      this.currentRating = { ...this.initialRating };
      this.editRatingForm.patchValue({
        rating: this.currentRating.rating,
        comment: this.currentRating.comment
      });
    }
  }
  onSubmit(): void {
    this.currentRating = {
      id: this.currentRating?.id || 0,
      rating: this.editRatingForm.value.rating || 0,
      comment: this.editRatingForm.value.comment || ''
    };
    this.ratingChanged.emit(this.currentRating);
  }
}


