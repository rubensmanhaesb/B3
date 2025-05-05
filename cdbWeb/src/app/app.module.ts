import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CdbFormComponent } from './cdb-form/cdb-form.component';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [AppComponent],
  imports: [
      BrowserModule, 
      AppRoutingModule, 
      ReactiveFormsModule, 
      HttpClientModule, 
      CdbFormComponent
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {}


