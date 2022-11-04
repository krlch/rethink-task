import { takeUntil } from 'rxjs/operators';
import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BaseComponent } from '../baseComponent';
import { from } from 'rxjs';

interface IUser {
  id: number;
  firstName: string;
  lastName: string;
  email: string;
}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent extends BaseComponent implements OnInit {
  title = 'Frontend';
  items: IUser[] = [];
  model?: IUser;

  constructor(private http: HttpClient,
    // private modalService: NgbModal) {
  ){
    super();
  }

  ngOnInit(): void {
    const req = from(
      Promise.resolve<IUser[]>([
        {
          id: 1,
          firstName: 'User 1',
          lastName: 'User 1',
          email: 'user1@gmail.com',
        },
        {
          id: 2,
          firstName: 'User 2',
          lastName: 'User 2',
          email: 'user1@gmail.com',
        },
        {
          id: 3,
          firstName: 'User3 ',
          lastName: 'User3',
          email: 'user1@gmail.com',

        },
      ])
    );

    // this.http
    //   .get('/api/users/all')
    req.pipe(takeUntil(this.unsubscribe)).subscribe((x: any) => {
      this.items = x;
    });
  }

  editUser(updated: IUser): void {
    this.http
      .put('/api/users', this.model)
      .pipe(takeUntil(this.unsubscribe))
      .subscribe((x: any) => {
        Object.assign(updated, this.model);
      });
  }

  deleteUser(user: IUser): void {
    console.error('not implemented', user); // confirm modal here
  }

  openEdit(ref: any, user: IUser) {
    this.model = { ...user };
    // this.modalService
    //   .open(ref, { ariaLabelledBy: 'modal-basic-title' })
    //   .result.then(
    //     (_result) => {
    //       this.editUser(this.model as IUser);
    //     }
    //     // (reason) => {}
    //   );
  }
}
