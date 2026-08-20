import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GigDetailRatingCommentComponent } from './gig-detail-rating-comment.component';

describe('GigDetailRatingCommentComponent', () => {
  let component: GigDetailRatingCommentComponent;
  let fixture: ComponentFixture<GigDetailRatingCommentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [GigDetailRatingCommentComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(GigDetailRatingCommentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
