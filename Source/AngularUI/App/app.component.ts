import {Component} from "angular2/core";
import {RouteConfig, ROUTER_DIRECTIVES} from "angular2/router";

import {EmployeeService} from "./services/employee.service";
import {EmployeeListComponent} from "./components/employee-list.component"
import {SearchResultListComponent} from "./components/search-result-list.component"
import {EmployeeDetailComponent} from "./components/employee-detail.component"
import {FeedbackReportComponent} from "./components/feedback-report.component"

@RouteConfig([
    { path: "/", name: "EmployeeList", component: EmployeeListComponent, useAsDefault: true },
    { path: "/search", name: "SearchResultList", component: SearchResultListComponent },
    { path: "/employee/:id", name: "EmployeeDetail", component: EmployeeDetailComponent },
    { path: "/report", name: "FeedbackReport", component: FeedbackReportComponent }
])

@Component({
    selector: "positive-feedback-app",
    templateUrl: "app/app.html",
    directives: [ROUTER_DIRECTIVES]
})

export class AppComponent
{
}