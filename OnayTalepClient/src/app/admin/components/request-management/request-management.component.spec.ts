import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RequestManagementComponent } from './request-management.component';

describe('RequestManagementComponent', () => {
  let component: RequestManagementComponent;
  let fixture: ComponentFixture<RequestManagementComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [RequestManagementComponent]
    });
    fixture = TestBed.createComponent(RequestManagementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
