import * as React from "react";
import Drawer from "material-ui/Drawer";
import AppBar from "material-ui/AppBar";
import MenuItem from "material-ui/MenuItem";
import * as sampleContentType from "../samples/contentType.json";

export interface IExamplePaletteProps {
    open: boolean;
    onClose: () => void;
    onSelected: (method: string, controller: string, parameters: string, body: string) => void;
}

export const ExamplePalette = (props: IExamplePaletteProps) => (
    <Drawer
        docked={false}
        open={props.open}
        onRequestChange={(open) => { !open && props.onClose(); }}>
        <AppBar title="Example requests" showMenuIconButton={false} />
        <MenuItem onTouchTap={() => {props.onSelected(
                "GET",
                "ContentTypes",
                "searchText=&fieldOrder=createdAt&orderAsc=false&startIndex=0&maxItems=2",
                ""); }}>
            Paginated Content Types</MenuItem>
        <MenuItem onTouchTap={() => {props.onSelected(
                "POST",
                "ContentTypes",
                "",
                JSON.stringify(sampleContentType, null, 4)); }}>
            Create Content Type</MenuItem>
        <MenuItem onTouchTap={() => {props.onSelected(
                "PUT",
                "ContentTypes",
                "",
                JSON.stringify({...sampleContentType, name: "Sample Type -edited-"}, null, 4)); }}>
            Update Content Type</MenuItem>
    </Drawer>
);