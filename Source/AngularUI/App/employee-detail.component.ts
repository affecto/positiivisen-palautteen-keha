import {Component} from "angular2/core";
import {OnInit} from "angular2/core";
import {RouteParams, ROUTER_DIRECTIVES} from "angular2/router";
import {HTTP_PROVIDERS} from "angular2/http";

import {EmployeeService} from "./services/employee.service";

@Component({
    selector: "employee-detail",
    template: `
        <p *ngIf="employee">{{ employee.name }}</p>
        <p><a [routerLink]="['EmployeeList']">Palaa etusivulle</a></p>
    `,
    inputs: ["employee"],
    directives: [ROUTER_DIRECTIVES],
    providers: [HTTP_PROVIDERS, EmployeeService]
})
export class EmployeeDetailComponent implements OnInit
{
    public employee: Employee;

    constructor(private routeParams: RouteParams, private employeeService: EmployeeService)
    {
    }

    public ngOnInit()
    {
        this.getEmployee(this.routeParams.get("id"));
    }

    private getEmployee(id: string): void
    {
        this.employeeService.getEmployee(id)
            .subscribe(employee => this.employee = employee);
    }
}