import * as React from "react";
import MuiThemeProvider from "material-ui/styles/MuiThemeProvider";
import getMuiTheme from "material-ui/styles/getMuiTheme";
import AppBar from "material-ui/AppBar";
import LinearProgress from "material-ui/LinearProgress";
import {RequestViewer} from "./RequestViewer";
import {Toolbar, ToolbarGroup} from "material-ui/Toolbar";
import {RequestInput} from "./RequestInput";
import {ExamplePalette} from "./ExamplePalette";

import RequestService from "../services/RequestService";
import {Request} from "../services/Request";
import {Util} from "../Util";

import * as sampleContentType from "../samples/contentType.json";

const baseUrl = "https://dnnapi.com/content/api/";

export interface IAppState {
    examplePaletteOpen: boolean;
    loading: boolean;
    apiKey: string;
    method: string;
    controller: string;
    parameters: string;
    body: string;
    status?: number;
    result: string;
}

export class App extends React.Component<undefined, IAppState> {
    constructor() {
        super();
        this.state = {
            examplePaletteOpen: false,
            loading: false,
            apiKey: "",
            method: "GET",
            controller: "ContentTypes",
            parameters: "",
            body: "",
            result: ""
        };
    }

    private getUrl(): string {
        const {controller, parameters} = this.state;
        if (!parameters) {
            return `${baseUrl}${controller}`;
        }

        const separator = Util.isGuid(parameters) ? "/" : "?";
        return `${baseUrl}${controller}${separator}${parameters}`;
    }

    private makeRequest() {
        const {method, apiKey, body} = this.state;
        this.setState({loading: true, result: ""});
        const url = this.getUrl();
        RequestService.request(method, apiKey, url, body)
            .then(result => this.setState({
                result: result.getResponseText(),
                status: result.getStatus(),
                loading: false
            }));
    }

    render() {
        const {loading, status, method, parameters, body, apiKey} = this.state;
        return (
            <MuiThemeProvider>
                <div>
                    <AppBar
                        style={{position: "absolute", top: 0, right: 0, left: 0}}
                        title="Liquid Content API Explorer"
                        iconClassNameRight="muidocs-icon-navigation-expand-more"
                        onLeftIconButtonTouchTap={() => this.setState({examplePaletteOpen: !this.state.examplePaletteOpen})} />

                    <div style={{padding: 20, position: "absolute", top: 64, bottom: 64, right: 0, left: 0, overflow: "auto"}}>    

                        <RequestInput
                            baseUrl={baseUrl}
                            apiKey={this.state.apiKey}
                            onChangeApiKey={apiKey => this.setState({apiKey})} 
                            method={this.state.method}
                            onChangeMethod={method => this.setState({method})}
                            controller={this.state.controller}
                            onChangeController={controller => this.setState({controller})}
                            parameters={this.state.parameters}
                            onChangeParameters={parameters => this.setState({parameters})}
                            body={this.state.body}
                            onChangeBody={body => this.setState({body})}
                            loading={this.state.loading}
                            onRequest={this.makeRequest.bind(this)} />

                        {loading &&
                            <LinearProgress mode="indeterminate" /> }

                        <RequestViewer
                            request={new Request(method, apiKey, this.getUrl(), body)}
                            responseJson={this.state.result} />

                    </div>

                    <Toolbar style={{position: "absolute", bottom: 0, right: 0, left: 0}}>
                        {status && !loading &&
                            <ToolbarGroup firstChild={true}>
                                <div style={{fontFamily: getMuiTheme().fontFamily, marginLeft: 20,
                                        color: (status >= 200 && status <= 299) ? "green" : "red"}}>
                                    Response Status: <strong>{status}</strong>
                                </div>
                            </ToolbarGroup>}
                    </Toolbar>
            
                    <ExamplePalette
                        open={this.state.examplePaletteOpen}
                        onClose={() => this.setState({examplePaletteOpen: false})}
                        onSelected={(method, controller, parameters, body) =>
                            this.setState({examplePaletteOpen: false, method, controller, parameters, body})}/>
                </div>
            </MuiThemeProvider>
        );
    }
};