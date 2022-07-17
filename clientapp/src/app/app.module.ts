import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";

import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./app.component";
import { ContentLayoutComponent } from "./layout/content-layout/content-layout.component";
import { FooterComponent } from "./layout/footer/footer.component";
import { NavbarComponent } from "./layout/navbar/navbar.component";
import { FontAwesomeModule } from "@fortawesome/angular-fontawesome";
import { IdentityRoutingModule } from "./modules/identity/identity.routing";
import { CoreModule } from "./core/core.module";
import { HttpClientModule } from "@angular/common/http";
import { DataModule } from "./data/data.module";
import { MdbAccordionModule } from "mdb-angular-ui-kit/accordion";
import { MdbCarouselModule } from "mdb-angular-ui-kit/carousel";
import { MdbCheckboxModule } from "mdb-angular-ui-kit/checkbox";
import { MdbCollapseModule } from "mdb-angular-ui-kit/collapse";
import { MdbDropdownModule } from "mdb-angular-ui-kit/dropdown";
import { MdbFormsModule } from "mdb-angular-ui-kit/forms";
import { MdbModalModule } from "mdb-angular-ui-kit/modal";
import { MdbPopoverModule } from "mdb-angular-ui-kit/popover";
import { MdbRadioModule } from "mdb-angular-ui-kit/radio";
import { MdbRangeModule } from "mdb-angular-ui-kit/range";
import { MdbRippleModule } from "mdb-angular-ui-kit/ripple";
import { MdbScrollspyModule } from "mdb-angular-ui-kit/scrollspy";
import { MdbTabsModule } from "mdb-angular-ui-kit/tabs";
import { MdbTooltipModule } from "mdb-angular-ui-kit/tooltip";
import { MdbValidationModule } from "mdb-angular-ui-kit/validation";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { HomeComponent } from "./home/home.component";
import { FormsModule } from "@angular/forms";
import { NewTimesheetModalComponent } from "./home/new-timesheet-modal.component";
import { IdentityModule } from "./modules/identity/identity.module";
import { MatDatepickerModule } from "@angular/material/datepicker";
import { MatFormFieldModule, MatHint } from "@angular/material/form-field";
import { MatNativeDateModule } from "@angular/material/core";
import { MatInputModule } from "@angular/material/input";

@NgModule({
  declarations: [
    AppComponent,
    ContentLayoutComponent,
    NavbarComponent,
    FooterComponent,
    HomeComponent,
    NewTimesheetModalComponent,
  ],
  imports: [
    HttpClientModule,
    BrowserModule,
    AppRoutingModule,
    FontAwesomeModule,
    IdentityRoutingModule,
    IdentityModule,
    CoreModule,
    DataModule,
    FormsModule,
    MdbAccordionModule,
    MdbCarouselModule,
    MdbCheckboxModule,
    MdbCollapseModule,
    MdbDropdownModule,
    MdbFormsModule,
    MdbModalModule,
    MdbPopoverModule,
    MdbRadioModule,
    MdbRangeModule,
    MdbRippleModule,
    MdbScrollspyModule,
    MdbTabsModule,
    MdbTooltipModule,
    MdbValidationModule,
    BrowserAnimationsModule,
    MatDatepickerModule,
    MatFormFieldModule,
    MatNativeDateModule,
    MatInputModule
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
