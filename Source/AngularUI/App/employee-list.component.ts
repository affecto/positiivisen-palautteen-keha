import {Component} from "angular2/core";
import {OnInit} from "angular2/core";
import {HTTP_PROVIDERS} from "angular2/http";

import {EmployeeService} from "./services/employee.service";
import {EmployeeListItemComponent} from "./employee-list-item.component"

@Component({
    selector: "employee-list",
    template: `
        <div>
            <span *ngFor="#employee of employees">
                <employee-list-item [employee]="employee"></employee-list-item>
            </span>
        </div>
    `,
    providers: [HTTP_PROVIDERS, EmployeeService],
    directives: [EmployeeListItemComponent]
})

export class EmployeeListComponent implements OnInit
{
    public employees: Employee[];

    constructor(private employeeService: EmployeeService)
    {
    }

    ngOnInit()
    {
        this.getEmployees();
    }

    private getEmployees(): void
    {
        this.employeeService.getEmployees()
            .subscribe(employees => this.employees = employees);
    }
}