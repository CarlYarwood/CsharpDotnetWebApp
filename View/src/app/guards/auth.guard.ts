import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';

export const authGuard: CanActivateFn = (_route, _state) => {
  const role = sessionStorage.getItem("User_Role");
  const router = inject(Router);
  if (role == null) {
    router.navigate(['/login'])
    return false;
  }
  else {
    return true;
  }
};
