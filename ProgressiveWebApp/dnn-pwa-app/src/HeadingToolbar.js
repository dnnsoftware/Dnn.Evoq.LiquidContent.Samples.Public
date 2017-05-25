import React from 'react';
import {Toolbar, ToolbarGroup, ToolbarTitle} from 'material-ui/Toolbar';
import RaisedButton from 'material-ui/RaisedButton';
import ActionInput from 'material-ui/svg-icons/action/input';

export default class HeadingToolbar extends React.Component {

  constructor(props) {
    super(props);
    this.state = {
      value: 3,
    };
  }

  handleChange = (event, index, value) => this.setState({value});

  render() {
    const {onSubscribe} = this.props;
    return (
      <Toolbar>
        <ToolbarGroup firstChild={true}>
          <RaisedButton label="Subscribe" primary={true} onTouchTap={onSubscribe} icon={<ActionInput />}/>
        </ToolbarGroup>
        <ToolbarGroup>
          <ToolbarTitle text="Dnn PWA" />
        </ToolbarGroup>
      </Toolbar>
    );
  }
}