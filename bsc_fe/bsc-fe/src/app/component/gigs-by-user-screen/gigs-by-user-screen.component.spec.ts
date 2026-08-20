import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GigsByUserScreenComponent } from './gigs-by-user-screen.component';

describe('GigsByUserScreenComponent', () => {
  let component: GigsByUserScreenComponent;
  let fixture: ComponentFixture<GigsByUserScreenComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [GigsByUserScreenComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(GigsByUserScreenComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
