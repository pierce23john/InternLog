import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";
import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./app.component";
import { ContentLayoutComponent } from "./layout/content-layout/content-layout.component";
import { FooterComponent } from "./layout/footer/footer.component";
import { IdentityRoutingModule } from "./modules/identity/identity.routing";
import { CoreModule } from "./core/core.module";
import { DataModule } from "./data/data.module";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { HomeComponent } from "./home/home.component";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { NewTimesheetModalComponent } from "./home/new-timesheet-modal.component";
import { IdentityModule } from "./modules/identity/identity.module";
import { SharedModule } from "./shared/shared.module";

@NgModule({
  declarations: [
    AppComponent,
    ContentLayoutComponent,
    FooterComponent,
    HomeComponent,
    NewTimesheetModalComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    IdentityRoutingModule,
    IdentityModule,
    CoreModule,
    DataModule,
    SharedModule,
    BrowserAnimationsModule,
  ],
  exports: [CoreModule, SharedModule],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
