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
    var EmployeeDetailComponent;
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
            EmployeeDetailComponent = (function () {
                function EmployeeDetailComponent(routeParams, router, employeeService) {
                    this.routeParams = routeParams;
                    this.router = router;
                    this.employeeService = employeeService;
                }
                EmployeeDetailComponent.prototype.ngOnInit = function () {
                    this.getEmployee(this.routeParams.get("id"));
                };
                EmployeeDetailComponent.prototype.sendFeedback = function () {
                    var _this = this;
                    if (this.feedback != null && this.feedback !== "") {
                        this.employeeService.addTextFeedback(this.employee.id, this.feedback)
                            .subscribe(function () {
                            _this.feedback = "";
                            alert("Feedback sent successfully.");
                            _this.router.navigate(["EmployeeList"]);
                        });
                    }
                };
                EmployeeDetailComponent.prototype.getEmployeePictureUrl = function () {
                    if (this.employee != null) {
                        return this.employeeService.getEmployeePictureUrl(this.employee.id);
                    }
                    return null;
                };
                EmployeeDetailComponent.prototype.getEmployee = function (id) {
                    var _this = this;
                    this.employeeService.getEmployee(id)
                        .subscribe(function (employee) { return _this.employee = employee; });
                };
                EmployeeDetailComponent = __decorate([
                    core_1.Component({
                        selector: "employee-detail",
                        templateUrl: "app/components/employee-detail.html",
                        inputs: ["employee"],
                        directives: [router_1.ROUTER_DIRECTIVES],
                        providers: [http_1.HTTP_PROVIDERS, employee_service_1.EmployeeService]
                    }), 
                    __metadata('design:paramtypes', [router_1.RouteParams, router_1.Router, employee_service_1.EmployeeService])
                ], EmployeeDetailComponent);
                return EmployeeDetailComponent;
            }());
            exports_1("EmployeeDetailComponent", EmployeeDetailComponent);
        }
    }
});
//# sourceMappingURL=employee-detail.component.js.map