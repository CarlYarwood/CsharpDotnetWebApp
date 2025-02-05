import { TestBed } from '@angular/core/testing';

import { UserCareTeamService } from './user-care-team.service';

describe('UserCareTeamService', () => {
  let service: UserCareTeamService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(UserCareTeamService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
