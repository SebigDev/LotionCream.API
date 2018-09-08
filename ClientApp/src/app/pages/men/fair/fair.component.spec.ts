/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { FairComponent } from './fair.component';

describe('FairComponent', () => {
  let component: FairComponent;
  let fixture: ComponentFixture<FairComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FairComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FairComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
