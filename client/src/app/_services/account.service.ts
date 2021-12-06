import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ReplaySubject } from 'rxjs';
import {map} from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { User } from '../_models/user';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseurl = environment.apiUrl;
  private CurrentUserSource  =new ReplaySubject<User>(1);
  currentUser$ = this.CurrentUserSource.asObservable();
  constructor(private http: HttpClient) { }

  login(model: any){   
     return this.http.post<User>(this.baseurl + 'account/login', model).pipe(
       map((responce: User)=>
       {
         const user = responce;
         if(user){
          this.setCurrentUser(user);
         }
       })
     )
  }
  register(model: any)
  {
    return this.http.post(this.baseurl + "account/register", model).pipe(
      map((user: User)=>
      {
        if(user){      
          this.setCurrentUser(user);
        }      
      })
    )
  }
  setCurrentUser(user: User)
  {
    localStorage.setItem('user',JSON.stringify(user));
    this.CurrentUserSource.next(user);
  }
  logout()
  {
    localStorage.removeItem('user');
    this.CurrentUserSource.next(null);
  }
}
