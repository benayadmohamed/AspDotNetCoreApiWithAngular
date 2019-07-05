import {Injectable} from '@angular/core';

@Injectable()
export class TokenService {

  constructor() {
  }


  public saveToken(token: any) {
    sessionStorage.setItem("token", token);
  }

  public getToken(token: any) {
    sessionStorage.setItem("token", token);
  }

}
