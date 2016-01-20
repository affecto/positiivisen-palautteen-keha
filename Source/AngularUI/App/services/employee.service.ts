import {Injectable} from "angular2/core";
import {Http, Headers} from "angular2/http";
import "rxjs/add/operator/map";

@Injectable()
export class EmployeeService
{
    constructor(private http: Http)
    {
    }

    public getEmployees()
    {
        return this.http
            .get("http://localhost:8050/v1/employees")
            .map((response: any) => response.json());
    }

    public getEmployee(id: string)
    {
        return this.http
            .get(`http://localhost:8050/v1/employee/${id}`)
            .map((response: any) => response.json());
    }

    public addTextFeedback(id: string, feedback: string)
    {
        var headers = new Headers();
        headers.append("Content-Type", "application/json");

        return this.http
            .post(`http://localhost:8050/v1/employee/${id}/textfeedback`, JSON.stringify(feedback), { headers: headers });
    }
}