/// <reference path="../../typings/jquery/jquery.d.ts" />
System.register(["angular2/core", "angular2/http", "angular2/router", "../services/employee.service", "./employee-list-item.component"], function(exports_1, context_1) {
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
    var core_1, http_1, router_1, employee_service_1, employee_list_item_component_1;
    var EmployeeListComponent;
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
            },
            function (employee_list_item_component_1_1) {
                employee_list_item_component_1 = employee_list_item_component_1_1;
            }],
        execute: function() {
            EmployeeListComponent = (function () {
                function EmployeeListComponent(employeeService) {
                    this.employeeService = employeeService;
                }
                EmployeeListComponent.prototype.ngOnInit = function () {
                    this.emptySearchCriteria();
                    this.getEmployees();
                    window.addEventListener("resize", this.calculateGridWidth, false);
                    this.calculateGridWidth();
                };
                EmployeeListComponent.prototype.onSearch = function () {
                    if (this.searchCriteria === "") {
                        this.getEmployees();
                    }
                    else {
                        this.searchEmployees(this.searchCriteria);
                    }
                };
                EmployeeListComponent.prototype.getEmployeePictureUrl = function (employeeId) {
                    return this.employeeService.getEmployeePictureUrl(employeeId);
                };
                EmployeeListComponent.prototype.emptySearchCriteria = function () {
                    this.searchCriteria = "";
                    this.getEmployees();
                };
                EmployeeListComponent.prototype.hasSearchCriteria = function () {
                    return this.searchCriteria !== null && this.searchCriteria !== "";
                };
                EmployeeListComponent.prototype.getEmployees = function () {
                    var _this = this;
                    this.employeeService.getEmployees()
                        .subscribe(function (employees) { return _this.employees = employees; });
                };
                EmployeeListComponent.prototype.searchEmployees = function (searchCriteria) {
                    var _this = this;
                    this.employeeService.searchEmployees(searchCriteria)
                        .subscribe(function (searchResult) {
                        if (searchResult.searchCriteria === searchCriteria) {
                            _this.employees = searchResult.employees;
                        }
                    });
                };
                EmployeeListComponent.prototype.calculateGridWidth = function () {
                    var $gridWidth = jQuery("body").width();
                    var colWidth = 160;
                    var gridCols = Math.floor($gridWidth / colWidth);
                    jQuery(".employee-grid").width((gridCols - 1) * colWidth);
                };
                EmployeeListComponent = __decorate([
                    core_1.Component({
                        selector: "employee-list",
                        templateUrl: "app/components/employee-list.html",
                        providers: [http_1.HTTP_PROVIDERS, employee_service_1.EmployeeService],
                        directives: [router_1.ROUTER_DIRECTIVES, employee_list_item_component_1.EmployeeListItemComponent]
                    }), 
                    __metadata('design:paramtypes', [employee_service_1.EmployeeService])
                ], EmployeeListComponent);
                return EmployeeListComponent;
            }());
            exports_1("EmployeeListComponent", EmployeeListComponent);
        }
    }
});
//# sourceMappingURL=employee-list.component.js.map