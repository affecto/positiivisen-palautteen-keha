/// <reference path="../../typings/jquery/jquery.d.ts" />

import {Component} from "angular2/core";
import {OnInit, OnChanges, AfterContentInit, AfterContentChecked, AfterViewInit, AfterViewChecked} from "angular2/core";
import {HTTP_PROVIDERS} from "angular2/http";
import {ROUTER_DIRECTIVES} from "angular2/router";

import {EmployeeService} from "../services/employee.service";
import {EmployeeListItemComponent} from "./employee-list-item.component";

declare var jQuery: JQueryStatic;
declare var Isotope: any;


@Component({
    selector: "employee-list",
    templateUrl: "app/components/employee-list.html",
    providers: [HTTP_PROVIDERS, EmployeeService],
    directives: [ROUTER_DIRECTIVES, EmployeeListItemComponent]
})

export class EmployeeListComponent implements OnInit
{
    public employees: Employee[];
    public searchCriteria: string;

    constructor(private employeeService: EmployeeService)
    {
    }

    public ngOnInit()
    {
        this.searchCriteria = "";
        this.getEmployees();
        window.addEventListener("resize", this.calculateGridWidth, false);  
        this.calculateGridWidth();
    }

    public onSearch(event: KeyboardEvent)
    {
        if (this.searchCriteria === "")
        {
            this.getEmployees();
        }
        else
        {
            this.searchEmployees(this.searchCriteria);
        }
    }

    public getEmployeePictureUrl(employeeId: string): string
    {
        return this.employeeService.getEmployeePictureUrl(employeeId);
    }

    private getEmployees(): void
    {
        this.employeeService.getEmployees()
            .subscribe((employees: Employee[]) => this.employees = employees);
    }

    private searchEmployees(searchCriteria: string): void
    {
        this.employeeService.searchEmployees(searchCriteria)
            .subscribe((searchResult: SearchResult) =>
            {
                if (searchResult.searchCriteria === searchCriteria)
                {
                    this.employees = searchResult.employees;
                }
            });
    }

    private calculateGridWidth(): void
    {
        console.log("initializing isotope grid");

        var $gridWidth = jQuery("body").width();
        var colWidth = 160;
        var gridCols = Math.floor($gridWidth / colWidth);

        jQuery(".employee-grid").width( (gridCols - 1) * colWidth );
    }
}