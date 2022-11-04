
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { BaseComponent } from './components/baseComponent';
import { AppComponent } from './components/home/app.component';

@NgModule({
  declarations: [AppComponent,BaseComponent],
  imports: [BrowserModule, AppRoutingModule],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
