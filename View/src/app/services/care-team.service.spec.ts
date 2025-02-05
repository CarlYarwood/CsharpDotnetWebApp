import { TestBed } from '@angular/core/testing';

import { CareTeamService } from './care-team.service';

describe('CareTeamService', () => {
  let service: CareTeamService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CareTeamService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
