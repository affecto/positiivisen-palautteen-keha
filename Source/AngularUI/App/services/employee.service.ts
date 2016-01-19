import {EMPLOYEES} from "./mock-employees"
import {Injectable} from "angular2/core";

@Injectable()
export class EmployeeService
{
    public getEmployees(): Promise<Employee[]>
    {
        return new Promise<Employee[]>(resolve => setTimeout(() => resolve(EMPLOYEES), 2000));
    }
}