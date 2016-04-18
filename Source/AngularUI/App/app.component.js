System.register(["angular2/core", "angular2/router", "./components/employee-list.component", "./components/employee-detail.component", "./components/feedback-report.component", "./components/feedback-presentation.component"], function(exports_1, context_1) {
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
    var core_1, router_1, employee_list_component_1, employee_detail_component_1, feedback_report_component_1, feedback_presentation_component_1;
    var AppComponent;
    return {
        setters:[
            function (core_1_1) {
                core_1 = core_1_1;
            },
            function (router_1_1) {
                router_1 = router_1_1;
            },
            function (employee_list_component_1_1) {
                employee_list_component_1 = employee_list_component_1_1;
            },
            function (employee_detail_component_1_1) {
                employee_detail_component_1 = employee_detail_component_1_1;
            },
            function (feedback_report_component_1_1) {
                feedback_report_component_1 = feedback_report_component_1_1;
            },
            function (feedback_presentation_component_1_1) {
                feedback_presentation_component_1 = feedback_presentation_component_1_1;
            }],
        execute: function() {
            AppComponent = (function () {
                function AppComponent() {
                }
                AppComponent = __decorate([
                    router_1.RouteConfig([
                        { path: "/", name: "EmployeeList", component: employee_list_component_1.EmployeeListComponent, useAsDefault: true },
                        { path: "/employee/:id", name: "EmployeeDetail", component: employee_detail_component_1.EmployeeDetailComponent },
                        { path: "/E34EB2CE-B573-48CE-BE8C-A01947D91378", name: "FeedbackReport", component: feedback_report_component_1.FeedbackReportComponent },
                        { path: "/presentation", name: "FeedbackPresentation", component: feedback_presentation_component_1.FeedbackPresentationComponent }
                    ]),
                    core_1.Component({
                        selector: "positive-feedback-app",
                        templateUrl: "app/app.html",
                        directives: [router_1.ROUTER_DIRECTIVES]
                    }), 
                    __metadata('design:paramtypes', [])
                ], AppComponent);
                return AppComponent;
            }());
            exports_1("AppComponent", AppComponent);
        }
    }
});
//# sourceMappingURL=app.component.js.map