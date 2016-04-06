import {Component} from "angular2/core";
import {RouteConfig, ROUTER_DIRECTIVES} from "angular2/router";

import {EmployeeService} from "./services/employee.service";
import {EmployeeListComponent} from "./components/employee-list.component"
import {EmployeeDetailComponent} from "./components/employee-detail.component"
import {FeedbackReportComponent} from "./components/feedback-report.component"
import {FeedbackPresentationComponent} from "./components/feedback-presentation.component"

@RouteConfig([
    { path: "/", name: "EmployeeList", component: EmployeeListComponent, useAsDefault: true },
    { path: "/employee/:id", name: "EmployeeDetail", component: EmployeeDetailComponent },
    { path: "/E34EB2CE-B573-48CE-BE8C-A01947D91378", name: "FeedbackReport", component: FeedbackReportComponent },
    { path: "/presentation", name: "FeedbackPresentation", component: FeedbackPresentationComponent }
])

@Component({
    selector: "positive-feedback-app",
    templateUrl: "app/app.html",
    directives: [ROUTER_DIRECTIVES]
})

export class AppComponent
{
}