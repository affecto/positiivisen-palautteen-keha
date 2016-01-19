import {Injectable} from "angular2/core";
import {Http} from "angular2/http";
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
            .get("http://localhost:8050/v1/positivefeedback/employees")
            .map((response: any) => response.json());
    }
}