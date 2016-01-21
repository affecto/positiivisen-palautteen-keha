import {bootstrap}    from "angular2/platform/browser"
import {ROUTER_PROVIDERS} from "angular2/router";
import {Configuration} from "./configuration";
import {AppComponent} from "./app.component"

bootstrap(AppComponent, [Configuration, ROUTER_PROVIDERS]);