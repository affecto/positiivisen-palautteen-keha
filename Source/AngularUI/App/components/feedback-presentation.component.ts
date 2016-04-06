/// <reference path="../../typings/jquery/jquery.d.ts" />

import {Component} from "angular2/core";
import {OnInit, OnChanges, DoCheck} from "angular2/core";
import {HTTP_PROVIDERS} from "angular2/http";
import {ROUTER_DIRECTIVES} from "angular2/router";

import {EmployeeService} from "../services/employee.service";

declare var jQuery: JQueryStatic;

@Component({
    selector: "feedback-presentation",
    templateUrl: "app/components/feedback-presentation.html",
    providers: [HTTP_PROVIDERS, EmployeeService],
    directives: [ROUTER_DIRECTIVES]
})

export class FeedbackPresentationComponent implements OnInit, OnChanges, DoCheck
{
    public shuffledEmployees: Employee[];

    constructor(private employeeService: EmployeeService)
    {
    }

    public ngOnInit()
    {
        this.getShuffledEmployeesWithFeedback();
    }

    public ngOnChanges(): void
    {
    }

    public ngDoCheck(): void
    {
        this.rollFeedback();
        
    }

    public getShuffledEmployeesWithFeedback(): void
    {
        this.employeeService.getShuffledEmployeesWithFeedback()
            .subscribe((shuffledEmployees: Employee[]) => this.shuffledEmployees = shuffledEmployees);
    }

    public getEmployeePictureUrl(employeeId: string): string
    {
        return this.employeeService.getEmployeePictureUrl(employeeId);
    }

   public rollFeedback(): void
   {
       console.log("============= ROLLING ===============");
       

       var presWrapper = jQuery(".presentation-wrapper");
       var presItems = jQuery(".presentation-item");

       var feedbackCount = presItems.length;
       var wrapperHeight = presWrapper.height();

       var transitionTime = (wrapperHeight / 40) + "s";

       var transformHeight = `translateY(-${wrapperHeight}px)`;

       presWrapper.css({
           transition: `${transitionTime} all linear`,
           transform: transformHeight
       });

       console.log("Rolling for " + wrapperHeight + " pixels");
   }
}