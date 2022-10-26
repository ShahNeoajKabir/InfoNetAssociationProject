import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DateControlComponentComponent } from './date-control-component.component';

describe('DateControlComponentComponent', () => {
  let component: DateControlComponentComponent;
  let fixture: ComponentFixture<DateControlComponentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DateControlComponentComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DateControlComponentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
