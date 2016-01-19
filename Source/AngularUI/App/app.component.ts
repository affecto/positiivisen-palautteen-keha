import {Component} from "angular2/core";
import {OnInit} from "angular2/core";
import {HTTP_PROVIDERS} from "angular2/http";
import {EmployeeService} from "./services/employee.service";
import {EmployeeDetailComponent} from "./employee-detail.component"

@Component({
    selector: "my-app",
    template: `
        <h1>Affecton työntekijät</h1>
        <div>
            <span *ngFor="#employee of employees">
                <employee-detail [employee]="employee"></employee-detail>
            </span>
        </div>
    `,
    providers: [HTTP_PROVIDERS, EmployeeService],
    directives: [EmployeeDetailComponent]
})

export class AppComponent implements OnInit
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