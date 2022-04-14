import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router, UrlTree, CanDeactivate } from '@angular/router';
import { LocalStorageUtils } from './utils/localstorage';

@Injectable({
  providedIn: 'root'
})
export class AppGuardGuard implements CanActivate {

  private isAutenticated: boolean;

  localStorageUtils = new LocalStorageUtils();
  constructor(
    private router: Router
  ) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): any {

    // console.log(route);
    // console.log(state);

    const usuario = this.localStorageUtils.lerUsuario();
    if (usuario != null) {
      const roles = usuario.claims.filter(r => r.type === 'role');
      const claims = usuario.claims.filter(r => r.type !== 'role');
      this.isAutenticated = true;
    }
    else {
      this.router.navigateByUrl(`/login?navigate=${this.router.url}`);
      this.isAutenticated = false;
    }
    return this.isAutenticated;
  }
}
