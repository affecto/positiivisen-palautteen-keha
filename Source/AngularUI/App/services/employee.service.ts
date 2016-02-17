import {Injectable} from "angular2/core";
import {Http, Headers} from "angular2/http";
import "rxjs/add/operator/map";
import {Configuration} from "../configuration";

@Injectable()
export class EmployeeService
{
    constructor(private http: Http, private configuration: Configuration)
    {
    }

    public getEmployeePictureUrl(employeeId: string): string
    {
        return `${this.configuration.apiBaseUrl}employees/${employeeId}/picture`;
    }

    public getEmployees()
    {
        return this.http
            .get(`${this.configuration.apiBaseUrl}employees`)
            .map((response: any) => response.json());
    }

    public getEmployeeFeedback()
    {
        return this.http
            .get(`${this.configuration.apiBaseUrl}employeefeedback`)
            .map((response: any) => response.json());
    }

    public getEmployee(id: string)
    {
        return this.http
            .get(`${this.configuration.apiBaseUrl}employees/${id}`)
            .map((response: any) => response.json());
    }

    public searchEmployees(searchCriteria: string)
    {
        return this.http
            .get(`${this.configuration.apiBaseUrl}employees/search/${searchCriteria}`)
            .map((response: any) => response.json());
    }

    public addTextFeedback(id: string, feedback: string)
    {
        var headers = new Headers();
        headers.append("Content-Type", "application/json");

        return this.http
            .post(`${this.configuration.apiBaseUrl}employees/${id}/textfeedback`, JSON.stringify(feedback), { headers: headers });
    }
}