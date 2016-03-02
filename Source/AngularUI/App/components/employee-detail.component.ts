import {Component} from "angular2/core";
import {OnInit} from "angular2/core";
import {NgForm} from "angular2/common";
import {RouteParams, Router, ROUTER_DIRECTIVES} from "angular2/router";
import {HTTP_PROVIDERS} from "angular2/http";

import {EmployeeService} from "../services/employee.service";

@Component({
    selector: "employee-detail",
    templateUrl: "app/components/employee-detail.html",
    inputs: ["employee"],
    directives: [ROUTER_DIRECTIVES],
    providers: [HTTP_PROVIDERS, EmployeeService]
})

export class EmployeeDetailComponent implements OnInit
{
    public employee: Employee;
    public feedback: string;

    constructor(private routeParams: RouteParams, private router: Router, private employeeService: EmployeeService)
    {
    }

    public ngOnInit(): void
    {
        this.getEmployee(this.routeParams.get("id"));
    }

    public sendFeedback(): void
    {
        if (this.feedback != null && this.feedback !== "")
        {
            this.employeeService.addTextFeedback(this.employee.id, this.feedback)
                .subscribe(() =>
                {
                    this.feedback = "";
                    alert("Feedback sent successfully.");
                    this.router.navigate(["EmployeeList"]);
                });
        }
    }

    public getEmployeePictureUrl(): string
    {
        if (this.employee != null)
        {
            return this.employeeService.getEmployeePictureUrl(this.employee.id);
        }

        return null;
    }

    private getEmployee(id: string): void
    {
        this.employeeService.getEmployee(id)
            .subscribe(employee => this.employee = employee);
    }
}