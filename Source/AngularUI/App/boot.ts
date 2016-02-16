import {bootstrap}    from "angular2/platform/browser"
import {ROUTER_PROVIDERS} from "angular2/router";
import {provide} from "angular2/core";
import {LocationStrategy, HashLocationStrategy} from "angular2/router";

import {Configuration} from "./configuration";
import {AppComponent} from "./app.component"

bootstrap(AppComponent, [
    Configuration,
    ROUTER_PROVIDERS,
    provide(LocationStrategy, { useClass: HashLocationStrategy })
]);