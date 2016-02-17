import {Component} from "angular2/core";
import {OnInit} from "angular2/core";
import {ROUTER_DIRECTIVES} from "angular2/router";
import {HTTP_PROVIDERS} from "angular2/http";

import {EmployeeService} from "../services/employee.service";

@Component({
    selector: "feedback-report",
    templateUrl: "app/components/feedback-report.html",
    directives: [ROUTER_DIRECTIVES],
    providers: [HTTP_PROVIDERS, EmployeeService]
})

export class FeedbackReportComponent implements OnInit
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
        this.employeeService.getEmployeeFeedback()
            .subscribe((employees: Employee[]) => this.employees = employees);
    }
}