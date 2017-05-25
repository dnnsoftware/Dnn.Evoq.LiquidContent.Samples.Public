import * as React from "react";
import {Request} from "../services/Request";
import {clipboard} from "electron";
import RaisedButton from "material-ui/RaisedButton";
import SelectField from "material-ui/SelectField";
import MenuItem from "material-ui/MenuItem";
import {Editor} from "./Editor";
import {SnippetGeneratorService} from "../services/SnippetGeneratorService";

const snippetGenerator = new SnippetGeneratorService();

export interface ISnippetGeneratorProps {
    request: Request;
}

export interface ISnippetGeneratorState {
    target: string;
}

export class SnippetGenerator extends React.Component<ISnippetGeneratorProps, ISnippetGeneratorState> {
    constructor() {
        super();
        this.state = {
            target: "Javascript"
        };
    }

    render () {
        return (
            <div>
                <SelectField
                    value={this.state.target}
                    floatingLabelText="Target"
                    onChange={(e, i, target) => this.setState({target})}>
                    <MenuItem value={"JQuery"} primaryText="jQuery" />
                    <MenuItem value={"Javascript"} primaryText="Javascript ES6" />
                </SelectField>
                <RaisedButton label="Copy to clipboard" primary={true} style={{float: "right", margin: 20}}
                    onTouchTap={() => {clipboard.writeText(snippetGenerator.generate(this.state.target, this.props.request)); }} />
                <Editor code={snippetGenerator.generate(this.state.target, this.props.request)} readOnly={true} />
            </div>
        );
    }
}