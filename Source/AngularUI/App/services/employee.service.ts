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
        var headers = this.createAcceptJsonHeaders();
        return this.http
            .get(`${this.configuration.apiBaseUrl}employees`, { headers: headers })
            .map((response: any) => response.json());
    }

    public getEmployeeFeedback()
    {
        var headers = this.createAcceptJsonHeaders();
        return this.http
            .get(`${this.configuration.apiBaseUrl}employeefeedback`, { headers: headers })
            .map((response: any) => response.json());
    }

    public getEmployee(id: string)
    {
        var headers = this.createAcceptJsonHeaders();
        return this.http
            .get(`${this.configuration.apiBaseUrl}employees/${id}`, { headers: headers })
            .map((response: any) => response.json());
    }

    public searchEmployees(searchCriteria: string)
    {
        var headers = this.createAcceptJsonHeaders();
        return this.http
            .get(`${this.configuration.apiBaseUrl}employees/search/${searchCriteria}`, { headers: headers })
            .map((response: any) => response.json());
    }

    public addTextFeedback(id: string, feedback: string)
    {
        var headers = new Headers();
        headers.append("Content-Type", "application/json");

        return this.http
            .post(`${this.configuration.apiBaseUrl}employees/${id}/textfeedback`, JSON.stringify(feedback), { headers: headers });
    }

    private createAcceptJsonHeaders(): Headers
    {
        var headers = new Headers();
        headers.append("Accept", "application/json");
        return headers;
    }
}