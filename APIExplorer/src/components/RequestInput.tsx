import * as React from "react";
import TextField from "material-ui/TextField";
import SelectField from "material-ui/SelectField";
import MenuItem from "material-ui/MenuItem";
import FloatingActionButton from "material-ui/FloatingActionButton";
import PlayIcon from "material-ui/svg-icons/av/play-arrow";
import {Editor} from "./Editor";
import {Util} from "../Util";

const style = {
    button: {
        float: "right"
    },
    label: {
        verticalAlign: "text-bottom"
    }
};

export interface RequestInputProps {
    baseUrl: string;
    apiKey: string;
    onChangeApiKey: (apiKey: string) => void;
    method: string;
    onChangeMethod: (medthod: string) => void;
    controller: string;
    onChangeController: (controller: string) => void;
    parameters: string;
    onChangeParameters: (parameters: string) => void;
    body: string;
    onChangeBody: (body: string) => void;
    loading: boolean;
    onRequest: () => void;
}

function getParametersHintText(method: string): string {
    switch (method) {
        case "GET":
            return "id of the element or URL parameters";
        case "DELETE":
            return "id of the element to delete";
        case "PUT":
            return "id of the element to update";
        default:
            return "";
    }
}

export const RequestInput = (props: RequestInputProps) => (
    <div>            
        <TextField
            hintText="Enter the API Key obtained from Content Library"
            floatingLabelText="API Key"
            fullWidth={true}
            floatingLabelFixed={true}
            value={props.apiKey}
            onChange={(e, apiKey) => props.onChangeApiKey(apiKey)}/>
        <SelectField style={{width: 120}}
            floatingLabelText="Method"
            value={props.method}
            onChange={(e, i, method) => props.onChangeMethod(method)}>
            <MenuItem value={"GET"} primaryText="GET" />
            <MenuItem value={"POST"} primaryText="POST" />
            <MenuItem value={"PUT"} primaryText="PUT" />
            <MenuItem value={"DELETE"} primaryText="DELETE" />
        </SelectField>
        <TextField disabled={true} style={style.label}
            value={props.baseUrl} id="baseUrl" />
        <SelectField
            style={{width: 180}}
            floatingLabelText="Controller"
            value={props.controller}
            onChange={(e, i, controller) => props.onChangeController(controller)}>
            <MenuItem value={"ContentTypes"} primaryText="ContentTypes" />
            <MenuItem value={"ContentItems"} primaryText="ContentItems" />
        </SelectField>
        {props.method !== "POST" &&
            <TextField disabled={true} style={{...style.label, width: 20}}
                value={!props.parameters || Util.isGuid(props.parameters) ? "/" : "?"} id="paramSeparator"/>}
        {props.method !== "POST" &&
            <TextField style={{verticalAlign: "text-bottom", width: 400}}
                floatingLabelText="Parameters"
                floatingLabelFixed={true}
                value={props.parameters}
                onChange={(e, parameters) => props.onChangeParameters(parameters)}
                hintText={getParametersHintText(props.method)} />}
        <FloatingActionButton style={style.button}
            onTouchTap={props.onRequest} disabled={props.loading}>
            <PlayIcon />
        </FloatingActionButton>

        {(props.method === "POST" || props.method === "PUT") &&
            <Editor code={props.body} readOnly={false}
                onChange={(body) => props.onChangeBody(body)} />}
    </div>
);