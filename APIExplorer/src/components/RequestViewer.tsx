import * as React from "react";
import {Tabs, Tab} from "material-ui/Tabs";
import {Editor} from "./Editor";
import {SnippetGenerator} from "./SnippetGenerator";
import {Request} from "../services/Request";

const styles = {
    main: {
        marginTop: 50
    },
    headline: {
        fontSize: 24,
        paddingTop: 16,
        marginBottom: 12
    }
};

export interface IRequestViewerProps {
    request: Request;
    responseJson: string;
}

export const RequestViewer = (props: IRequestViewerProps) => (
 <Tabs style={styles.main}>
    <Tab label="Response JSON" >
      <div>
        <Editor code={props.responseJson} readOnly={true} />
      </div>
    </Tab>
    <Tab label="Request" >
      <div style={{width: "100%"}}>
          <textarea rows={30} readOnly
            style={{width: "100%", height: 300, boxSizing: "border-box"}}
            value={props.request.getRawRequestText()} />                    
      </div>
    </Tab>
    <Tab label="Code Snippets" >
      <div>
        <SnippetGenerator request={props.request} />
      </div>
    </Tab>
  </Tabs>
);