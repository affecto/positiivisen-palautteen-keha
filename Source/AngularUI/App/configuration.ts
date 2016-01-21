import {Injectable} from "angular2/core";

@Injectable()
export class Configuration
{
    public apiBaseUrl: string;

    constructor()
    {
        this.apiBaseUrl = (<any>window).positiveFeedbackApp.config.apiBaseUrl;
    }
}