import {Component} from "angular2/core";
import {ROUTER_DIRECTIVES} from "angular2/router";

@Component({
    selector: "employee-list-item",
    template: `<a *ngIf="employee" [routerLink]="['EmployeeDetail', { id: employee.id }]">{{ employee.name }}</a>`,
    inputs: ["employee"],
    directives: [ROUTER_DIRECTIVES]
})

export class EmployeeListItemComponent
{
    public employee: Employee;
}