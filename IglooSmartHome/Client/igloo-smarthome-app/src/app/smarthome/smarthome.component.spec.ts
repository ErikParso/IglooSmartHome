import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SmarthomeComponent } from './smarthome.component';

describe('SmarthomeComponent', () => {
  let component: SmarthomeComponent;
  let fixture: ComponentFixture<SmarthomeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SmarthomeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SmarthomeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
