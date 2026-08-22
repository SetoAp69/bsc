import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GigCreateScreenComponent } from './gig-create-screen.component';

describe('GigCreateScreenComponent', () => {
  let component: GigCreateScreenComponent;
  let fixture: ComponentFixture<GigCreateScreenComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [GigCreateScreenComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(GigCreateScreenComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
