System.register(["angular2/core", "angular2/http", "rxjs/add/operator/map", "../configuration"], function(exports_1, context_1) {
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
    var core_1, http_1, configuration_1;
    var EmployeeService;
    return {
        setters:[
            function (core_1_1) {
                core_1 = core_1_1;
            },
            function (http_1_1) {
                http_1 = http_1_1;
            },
            function (_1) {},
            function (configuration_1_1) {
                configuration_1 = configuration_1_1;
            }],
        execute: function() {
            EmployeeService = (function () {
                function EmployeeService(http, configuration) {
                    this.http = http;
                    this.configuration = configuration;
                }
                EmployeeService.prototype.getEmployeePictureUrl = function (employeeId) {
                    return this.configuration.apiBaseUrl + "employees/" + employeeId + "/picture";
                };
                EmployeeService.prototype.getEmployees = function () {
                    var headers = this.createAcceptJsonHeaders();
                    return this.http
                        .get(this.configuration.apiBaseUrl + "employees", { headers: headers })
                        .map(function (response) { return response.json(); });
                };
                EmployeeService.prototype.getShuffledEmployeesWithFeedback = function () {
                    var headers = this.createAcceptJsonHeaders();
                    return this.http
                        .get(this.configuration.apiBaseUrl + "shuffledemployeeswithfeedback", { headers: headers })
                        .map(function (response) { return response.json(); });
                };
                EmployeeService.prototype.getEmployeeFeedback = function () {
                    var headers = this.createAcceptJsonHeaders();
                    return this.http
                        .get(this.configuration.apiBaseUrl + "employeefeedback", { headers: headers })
                        .map(function (response) { return response.json(); });
                };
                EmployeeService.prototype.getEmployee = function (id) {
                    var headers = this.createAcceptJsonHeaders();
                    return this.http
                        .get(this.configuration.apiBaseUrl + "employees/" + id, { headers: headers })
                        .map(function (response) { return response.json(); });
                };
                EmployeeService.prototype.searchEmployees = function (searchCriteria) {
                    var headers = this.createAcceptJsonHeaders();
                    return this.http
                        .get(this.configuration.apiBaseUrl + "employees/search/" + searchCriteria, { headers: headers })
                        .map(function (response) { return response.json(); });
                };
                EmployeeService.prototype.addTextFeedback = function (id, feedback) {
                    var headers = new http_1.Headers();
                    headers.append("Content-Type", "application/json");
                    return this.http
                        .post(this.configuration.apiBaseUrl + "employees/" + id + "/textfeedback", JSON.stringify(feedback), { headers: headers });
                };
                EmployeeService.prototype.createAcceptJsonHeaders = function () {
                    var headers = new http_1.Headers();
                    headers.append("Accept", "application/json");
                    return headers;
                };
                EmployeeService = __decorate([
                    core_1.Injectable(), 
                    __metadata('design:paramtypes', [http_1.Http, configuration_1.Configuration])
                ], EmployeeService);
                return EmployeeService;
            }());
            exports_1("EmployeeService", EmployeeService);
        }
    }
});
//# sourceMappingURL=employee.service.js.map