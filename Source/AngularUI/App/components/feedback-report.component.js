System.register(["angular2/core", "angular2/router", "angular2/http", "../services/employee.service"], function(exports_1, context_1) {
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
    var core_1, router_1, http_1, employee_service_1;
    var FeedbackReportComponent;
    return {
        setters:[
            function (core_1_1) {
                core_1 = core_1_1;
            },
            function (router_1_1) {
                router_1 = router_1_1;
            },
            function (http_1_1) {
                http_1 = http_1_1;
            },
            function (employee_service_1_1) {
                employee_service_1 = employee_service_1_1;
            }],
        execute: function() {
            FeedbackReportComponent = (function () {
                function FeedbackReportComponent(employeeService) {
                    this.employeeService = employeeService;
                }
                FeedbackReportComponent.prototype.ngOnInit = function () {
                    this.getEmployees();
                };
                FeedbackReportComponent.prototype.getEmployees = function () {
                    var _this = this;
                    this.employeeService.getEmployeeFeedback()
                        .subscribe(function (employees) { return _this.employees = employees; });
                };
                FeedbackReportComponent = __decorate([
                    core_1.Component({
                        selector: "feedback-report",
                        templateUrl: "app/components/feedback-report.html",
                        directives: [router_1.ROUTER_DIRECTIVES],
                        providers: [http_1.HTTP_PROVIDERS, employee_service_1.EmployeeService]
                    }), 
                    __metadata('design:paramtypes', [employee_service_1.EmployeeService])
                ], FeedbackReportComponent);
                return FeedbackReportComponent;
            }());
            exports_1("FeedbackReportComponent", FeedbackReportComponent);
        }
    }
});
//# sourceMappingURL=feedback-report.component.js.map