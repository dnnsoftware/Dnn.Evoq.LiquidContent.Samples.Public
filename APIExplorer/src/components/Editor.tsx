import * as React from "react";
import * as CodeMirror from "react-codemirror";
import "codemirror/mode/javascript/javascript";

export interface IEditorProps {
    code: string;
    readOnly: boolean;
    onChange?: (code: string) => void;
}

export const Editor = (props: IEditorProps) => {
    const options = {
        readOnly: props.readOnly,
    };
    return (
        <div style={{border: "1px solid lightgray"}}>
            <CodeMirror
                value={props.code} options={options} onChange={props.onChange} />
        </div>
    );
};
