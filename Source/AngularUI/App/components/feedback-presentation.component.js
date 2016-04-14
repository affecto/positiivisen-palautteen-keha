/// <reference path="../../typings/jquery/jquery.d.ts" />
System.register(["angular2/core", "angular2/http", "angular2/router", "../services/employee.service"], function(exports_1, context_1) {
    "use strict";
    var __moduleName = context_1 && context_1.id;
    var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
        var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
        if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
        else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
        return c > 3 && r && Object.defineProperty(target, key, r), r;
    };
    var __metadata = (this && this.__metadata) || function (k, v) {
        if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
    };
    var core_1, http_1, router_1, employee_service_1;
    var FeedbackPresentationComponent;
    return {
        setters:[
            function (core_1_1) {
                core_1 = core_1_1;
            },
            function (http_1_1) {
                http_1 = http_1_1;
            },
            function (router_1_1) {
                router_1 = router_1_1;
            },
            function (employee_service_1_1) {
                employee_service_1 = employee_service_1_1;
            }],
        execute: function() {
            FeedbackPresentationComponent = (function () {
                function FeedbackPresentationComponent(employeeService) {
                    this.employeeService = employeeService;
                    this.shuffledEmployees = [];
                }
                FeedbackPresentationComponent.prototype.ngOnInit = function () {
                    this.getShuffledEmployeesWithFeedback();
                    this.isRolling = false;
                };
                FeedbackPresentationComponent.prototype.ngOnChanges = function () {
                };
                FeedbackPresentationComponent.prototype.ngDoCheck = function () {
                    if (jQuery(".presentation-item").length > 0 && this.isRolling === false) {
                        this.hidePresentationContainer();
                    }
                    if (this.isRolling === false) {
                        this.rollFeedback();
                    }
                };
                FeedbackPresentationComponent.prototype.getShuffledEmployeesWithFeedback = function () {
                    var _this = this;
                    this.employeeService.getShuffledEmployeesWithFeedback()
                        .subscribe(function (shuffledEmployees) { return _this.shuffledEmployees = shuffledEmployees; });
                };
                FeedbackPresentationComponent.prototype.getEmployeePictureUrl = function (employeeId) {
                    return this.employeeService.getEmployeePictureUrl(employeeId);
                };
                FeedbackPresentationComponent.prototype.getRandomGreeting = function () {
                    var greetings = [
                        "Hey there",
                        "Look at you",
                        "Howdy",
                        "Well isn't it",
                        "Good yo see you",
                        "Keep up the good work"
                    ];
                    return greetings[Math.floor(Math.random() * greetings.length)];
                };
                FeedbackPresentationComponent.prototype.getRandomInsert = function () {
                    var inserts = [
                        "Here's something your colleagues have said about you",
                        "People think you're awesome",
                        "You've got some feedback right here",
                        "Take a look at these fine words",
                        "Greetings from your co-workers",
                        "This is what your friends at work say about you"
                    ];
                    return inserts[Math.floor(Math.random() * inserts.length)];
                };
                FeedbackPresentationComponent.prototype.hidePresentationContainer = function () {
                    jQuery(".presentation-item-wrapper").css({
                        transition: "0ms all linear",
                        transform: "translateY(1200px)"
                    });
                };
                FeedbackPresentationComponent.prototype.resetPresentation = function () {
                    this.hidePresentationContainer();
                    this.isRolling = false;
                    this.shuffledEmployees = [];
                    this.getShuffledEmployeesWithFeedback();
                    this.rollFeedback();
                };
                FeedbackPresentationComponent.prototype.rollFeedback = function () {
                    var _this = this;
                    var presContainer = jQuery(".presentation-container");
                    var presItemWrapper = jQuery(".presentation-item-wrapper");
                    var presItems = jQuery(".presentation-item").length;
                    var wrapperHeight = presItemWrapper.height();
                    var transitionTime = wrapperHeight / 40 + "s";
                    var transformHeight = "translateY(-" + wrapperHeight + "px)";
                    var containerHeight = jQuery(window).height() + "px";
                    presContainer.css({
                        overflow: "hidden",
                        height: containerHeight
                    });
                    presItemWrapper.css({
                        transition: transitionTime + " all linear",
                        transform: transformHeight
                    });
                    if (presItems > 0) {
                        this.isRolling = true;
                        var resetTimer = ((wrapperHeight / 40) * 1000) + 10000;
                        window.setTimeout(function () { _this.resetPresentation(); }, resetTimer);
                    }
                };
                FeedbackPresentationComponent.prototype.bottomIsInViewport = function (el) {
                    console.log("checking!");
                    //special bonus for those using jQuery
                    if (typeof jQuery === "function" && el instanceof jQuery) {
                        el = el[0];
                    }
                    var rect = el.getBoundingClientRect();
                    return (rect.bottom <= (window.innerHeight || document.documentElement.clientHeight));
                };
                FeedbackPresentationComponent = __decorate([
                    core_1.Component({
                        selector: "feedback-presentation",
                        templateUrl: "app/components/feedback-presentation.html",
                        providers: [http_1.HTTP_PROVIDERS, employee_service_1.EmployeeService],
                        directives: [router_1.ROUTER_DIRECTIVES]
                    }), 
                    __metadata('design:paramtypes', [employee_service_1.EmployeeService])
                ], FeedbackPresentationComponent);
                return FeedbackPresentationComponent;
            }());
            exports_1("FeedbackPresentationComponent", FeedbackPresentationComponent);
        }
    }
});
//# sourceMappingURL=feedback-presentation.component.js.map