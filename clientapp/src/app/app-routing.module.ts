import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { AuthGuard } from "./core/guard/auth.guard";
import { HomeComponent } from "./home/home.component";
import { ContentLayoutComponent } from "./layout/content-layout/content-layout.component";
import { TimesheetsResolver } from "./shared/timesheets.resolver";

const routes: Routes = [
  {
    path: "app",
    component: ContentLayoutComponent,
    canActivate: [AuthGuard], // Should be replaced with actual auth guard
    children: [
      {
        path: "home",
        component: HomeComponent,
        resolve: { timesheets: TimesheetsResolver },
      },
    ],
  },
  {
    path: "identity",
    component: ContentLayoutComponent,
    loadChildren: () =>
      import("@modules/identity/identity.module").then((m) => m.IdentityModule),
  },
  // Fallback when no prior routes is matched
  { path: "**", redirectTo: "/identity/login", pathMatch: "full" },
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { enableTracing: false })],
  exports: [RouterModule],
})
export class AppRoutingModule {}
