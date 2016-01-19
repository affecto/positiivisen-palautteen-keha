import {Component} from "angular2/core";

@Component({
    selector: "employee-detail",
    template: `<span *ngIf="employee">{{ employee.name }}</span>`,
    inputs: ["employee"]
})

export class EmployeeDetailComponent
{
    public employee: Employee;
}