import {Component} from "angular2/core";
import {OnInit} from "angular2/core";
import {HTTP_PROVIDERS} from "angular2/http";
import {ROUTER_DIRECTIVES} from "angular2/router";

import {EmployeeService} from "../services/employee.service";
import {EmployeeListItemComponent} from "./employee-list-item.component"

@Component({
    selector: "employee-list",
    templateUrl: "app/components/employee-list.html",
    providers: [HTTP_PROVIDERS, EmployeeService],
    directives: [ROUTER_DIRECTIVES, EmployeeListItemComponent]
})

export class EmployeeListComponent implements OnInit
{
    public employees: Employee[];

    constructor(private employeeService: EmployeeService)
    {
    }

    public ngOnInit()
    {
        this.getEmployees();
    }

    public getEmployeePictureUrl(employeeId: string): string
    {
        return this.employeeService.getEmployeePictureUrl(employeeId);
    }

    private getEmployees(): void
    {
        this.employeeService.getEmployees()
            .subscribe(employees => this.employees = employees);
    }
}