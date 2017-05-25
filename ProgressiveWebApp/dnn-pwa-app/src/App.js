import React, { Component } from 'react';
import logo from './logo.svg';
import './App.css';
import EventCard from "./EventCard";
import HeadingToolbar from "./HeadingToolbar";
import subscribe from "./services/subscribe";
import eventService from "./services/eventService";
import injectTapEventPlugin from 'react-tap-event-plugin';
import getMuiTheme from 'material-ui/styles/getMuiTheme';
import MuiThemeProvider from 'material-ui/styles/MuiThemeProvider';
import FlatButton from 'material-ui/FlatButton';

// Needed for onTouchTap
// Check this repo:
// https://github.com/zilverline/react-tap-event-plugin
injectTapEventPlugin();

class App extends Component {
  constructor(props) {
    super(props);
    this.state = {
      events: []
    };
  }

  componentWillMount() {
    this.getAllEvents();
  }

  getAllEvents() {
    eventService.getEvents().then((events) => {
      this.setState({
        events: events.reverse().map((event, index) =>
          <EventCard 
            key={index} 
            publicationUser={event.sender}
            publicationTime={event.updatedAt}
            publicationAvatar={event.avatar}
            title={event.name}
            subtitle={event.contentTypeName}
            />
        )
      });
    });
  }

  onSubscribe(){
    subscribe.subscribe();
  }

  subscribeTitle() {
    return "To be completed";
  }

  handleClose = () => {
    this.setState({dialogOpen: false});
  };

  render() {
    const actions = [
      <FlatButton
        label="Continue"
        primary={true}
        onTouchTap={this.handleClose}
      />
    ];

    return (
      <MuiThemeProvider muiTheme={getMuiTheme()}>
        <div>
          <HeadingToolbar onSubscribe={this.onSubscribe.bind(this)}/>
          <main>
            <div>
              {this.state.events}
            </div>
          </main>
        </div>
      </MuiThemeProvider>
    );
  }
}

export default App;
