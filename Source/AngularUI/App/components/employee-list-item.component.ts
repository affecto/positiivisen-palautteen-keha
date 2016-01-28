import {Component} from "angular2/core";
import {ROUTER_DIRECTIVES} from "angular2/router";

@Component({
    selector: "employee-list-item",
    templateUrl: "app/components/employee-list-item.html",
    inputs: ["employee", "employeePictureUrl"],
    directives: [ROUTER_DIRECTIVES]
})

export class EmployeeListItemComponent
{
    public employee: Employee;
    public employeePictureUrl: string;
}