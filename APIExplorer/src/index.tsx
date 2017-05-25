import * as React from "react";
import * as ReactDOM from "react-dom";

import { App } from "./components/App";
import MuiThemeProvider from "material-ui/styles/MuiThemeProvider";
import * as injectTapEventPlugin from "react-tap-event-plugin";

// Needed for onTouchTap
// http://stackoverflow.com/a/34015469/988941
injectTapEventPlugin();

ReactDOM.render(
    <App />,
    document.getElementById("main-container")
);