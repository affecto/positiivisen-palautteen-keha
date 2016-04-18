/// <reference path="../../typings/jquery/jquery.d.ts" />

import {Component} from "angular2/core";
import {OnInit, DoCheck} from "angular2/core";
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

export class FeedbackPresentationComponent implements OnInit, DoCheck
{
    public shuffledEmployees: Employee[];
    public timeout: any;
    public isRolling: boolean;

    constructor(private employeeService: EmployeeService)
    {
        this.shuffledEmployees = [];
    }

    public ngOnInit()
    {
        this.getShuffledEmployeesWithFeedback();
        this.isRolling = false;
    }

    public ngDoCheck(): void
    {
        if (this.shuffledEmployees.length > 0 && this.isRolling === false )
        {
            this.hidePresentationContainer();
        }

        if (this.isRolling === false)
        {
            this.rollFeedback();
        }
       
       
    }

    public getEmployeePictureUrl(employeeId: string): string
    {
        return this.employeeService.getEmployeePictureUrl(employeeId);
    }

    public onResize(): void
    {
        var presContainer = jQuery(".presentation-container");
        var containerHeight = jQuery(window).height() + "px";

        presContainer.css({
            overflow: "hidden",
            height: containerHeight
        });
    }

    private getShuffledEmployeesWithFeedback(): void
    {
        this.employeeService.getShuffledEmployeesWithFeedback()
            .subscribe((shuffledEmployees: Employee[]) => this.shuffledEmployees = shuffledEmployees);
    }

    private getRandomItemFromArray(array: string[]): string
    {
        return array[Math.floor(Math.random() * array.length)];
    }

    private getRandomInsert(): string
    {
        var inserts = [
            "Here's something your colleagues have said about you",
            "People think you're awesome",
            "You've got some feedback right here",
            "Take a look at these fine words",
            "Greetings from your co-workers",
            "This is what your friends at work say about you"
        ];

        return this.getRandomItemFromArray(inserts);
    }
        
    private getRandomGreeting(): string
    {
        var greetings = [
            "Hey there",
            "Look at you",
            "Howdy",
            "Well isn't it",
            "Good to see you",
            "Keep up the good work"
        ];

        return this.getRandomItemFromArray(greetings);
    }

    private hidePresentationContainer(): void
    {
        var containerHeight = jQuery(window).height();
        jQuery(".presentation-item-wrapper").css({
            transition: "0ms all linear",
            transform: `translateY(${containerHeight}px)`
    });
    }

    private resetPresentation(): void
    {
        this.hidePresentationContainer();
        this.isRolling = false;
        this.shuffledEmployees = [];
        this.getShuffledEmployeesWithFeedback();
        this.rollFeedback();
    }

    private rollFeedback(): void
   {
       var presContainer = jQuery(".presentation-container");
       var presItemWrapper = jQuery(".presentation-item-wrapper");
       var presItems = jQuery(".presentation-item").length;
       
       var wrapperHeight = presItemWrapper.height();
       var transitionTime =  `${wrapperHeight / 40}s`;
       var transformHeight = `translateY(-${wrapperHeight}px)`;
       var containerHeight = jQuery(window).height() + "px";

       presContainer.css({
           overflow: "hidden",
           height: containerHeight
       });

       presItemWrapper.css({
           transition: `${transitionTime} all linear`,
           transform: transformHeight
       });
       
       if (presItems > 0)
       {
           this.isRolling = true;
           var resetTimer = ( (wrapperHeight / 40) * 1000 ) + 5000;
           window.setTimeout(() => { this.resetPresentation() }, resetTimer);
       }
       
   }
    
}
