import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GigDetailScreenComponent } from './gig-detail-screen.component';

describe('GigDetailScreenComponent', () => {
  let component: GigDetailScreenComponent;
  let fixture: ComponentFixture<GigDetailScreenComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [GigDetailScreenComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(GigDetailScreenComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
