///<reference path="../node_modules/angular2/typings/browser.d.ts"/>
import {bootstrap}    from "angular2/platform/browser"
import {ROUTER_PROVIDERS} from "angular2/router";
import {provide, enableProdMode} from "angular2/core";
import {LocationStrategy, HashLocationStrategy} from "angular2/router";

import {Configuration} from "./configuration";
import {AppComponent} from "./app.component"

enableProdMode();

bootstrap(AppComponent, [
    Configuration,
    ROUTER_PROVIDERS,
    provide(LocationStrategy, { useClass: HashLocationStrategy })
]);