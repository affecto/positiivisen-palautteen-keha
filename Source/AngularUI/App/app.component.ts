import {Component} from "angular2/core";
import {RouteConfig, ROUTER_DIRECTIVES} from "angular2/router";

import {EmployeeService} from "./services/employee.service";
import {EmployeeListComponent} from "./components/employee-list.component"
import {EmployeeDetailComponent} from "./components/employee-detail.component"

@RouteConfig([
    { path: "/",                name: "EmployeeList",   component: EmployeeListComponent, useAsDefault: true },
    { path: "/employee/:id",    name: "EmployeeDetail", component: EmployeeDetailComponent }
])

@Component({
    selector: "positive-feedback-app",
    template: `
        <h1>Positiivisen palautteen kehä</h1>
        <router-outlet></router-outlet>
    `,
    directives: [ROUTER_DIRECTIVES]
})

export class AppComponent
{
}