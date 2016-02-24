import {Component} from "angular2/core";
import {OnInit, OnChanges, AfterContentInit, AfterContentChecked, AfterViewInit, AfterViewChecked} from "angular2/core";
import {RouteParams, Router, ROUTER_DIRECTIVES} from "angular2/router";

@Component({
    selector: "employee-list-item",
    styles: [],
    templateUrl: "app/components/employee-list-item.html",
    inputs: ["employee", "employeePictureUrl", "employeeSearchInput"],
    directives: [ROUTER_DIRECTIVES]
})

export class EmployeeListItemComponent
{
    public employee: Employee;
    public employeePictureUrl: string;
    public employeeSearchInput: HTMLInputElement;

    constructor(private router: Router)
    {
    }

    public ngOnInit(): void 
    {
    }

    public goToEmployee()
    {
        this.employeeSearchInput.value = "";
        this.router.navigate(["EmployeeDetail", { id: this.employee.id }]);
    }
}