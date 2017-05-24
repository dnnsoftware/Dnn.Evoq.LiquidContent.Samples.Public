import React, { Component, PropTypes } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux'
import { ActionCreators } from '../actions'

import {
  Animated,
  StyleSheet,
  View,
  NavigationExperimental
} from 'react-native';
import Home from './Home';

class AppContainer extends Component {

  constructor(props) {
    super(props);
  }

  render() {
    const style = [
      styles.scene
    ];
    let Scene = Home;
    return (
      <Animated.View style={style}>
        <Scene {...this.props} style={style} />
      </Animated.View>
    )
  }
}

const styles = StyleSheet.create({
  scene: {
    flex: 1,
    bottom: 0,
    left: 0,
    position: 'absolute',
    right: 0,
    top: 0,
  },
});


function mapDispatchToProps(dispatch) {
  return bindActionCreators(ActionCreators, dispatch);
}

function mapStateToProps(state) {
  return {
    navigationState: state.navigationState
  };
}


export default connect(mapStateToProps, mapDispatchToProps)(AppContainer);
