import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GigByUserHeaderComponent } from './gig-by-user-header.component';

describe('GigByUserHeaderComponent', () => {
  let component: GigByUserHeaderComponent;
  let fixture: ComponentFixture<GigByUserHeaderComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [GigByUserHeaderComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(GigByUserHeaderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
