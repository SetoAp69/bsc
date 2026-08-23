import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GigTypeSelectorComponent } from './gig-type-selector.component';

describe('GigTypeSelectorComponent', () => {
  let component: GigTypeSelectorComponent;
  let fixture: ComponentFixture<GigTypeSelectorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [GigTypeSelectorComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(GigTypeSelectorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
