import { Directive, OnDestroy } from '@angular/core';
import { Subject } from 'rxjs';

@Directive({})
export class BaseComponent implements OnDestroy {
  protected unsubscribe = new Subject<void>();

  ngOnDestroy() {
    this.unsubscribe.next();
    this.unsubscribe.complete();
  }
}
