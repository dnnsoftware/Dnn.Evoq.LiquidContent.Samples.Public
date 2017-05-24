import React, { Component } from 'react';
import { connect } from 'react-redux';
import ReactNative from 'react-native';
import { appStyle } from '../styles';
import MapView from 'react-native-maps';
const {
  ScrollView,
  View,
  TextInput,
  Image,
  Text,
  TouchableHighlight,
  StyleSheet,
  Button
} = ReactNative;

class Home extends Component {
  constructor(props) {
    super(props)
    this.state = {
      searching: false,
      ingredientsInput: ''
    }
  }

  searchPressed() {
    this.setState({ searching: true })
    this.props.getContentTypes(this.state.ingredientsInput).then((res) => {
      this.setState({ searching: false })
    });
  }

  categoryPressed(id) {
    this.props.getContentItems(id).then((res) => {
    });
  }

  contentTypes() {
    return Object.keys(this.props.contentTypes).map(key => this.props.contentTypes[key]);
  }

  render() {
    return (
      <View style={appStyle.scene}>
        <View style={appStyle.searchSection}>
          <TextInput style={appStyle.searchInput}
            returnKeyType="search"
            placeholder="my location"
            onChangeText={(ingredientsInput) => this.setState({ ingredientsInput })}
            value={this.state.ingredientsInput}
          />
          <Button
            onPress={() => this.searchPressed()}
            title="Load Geolocation Groups"
          />
        </View>
        <ScrollView style={appStyle.scrollSection} >
          {!this.state.searching && this.contentTypes().map((type, index) => {
            return <TouchableHighlight key={"group-" + index}
              style={appStyle.searchButton}
              onPress={() => this.categoryPressed(type.id)}>
              <View style={appStyle.contentTypes}>
                <Text style={appStyle.resultText} >{type.name}</Text>
              </View>
            </TouchableHighlight>
          })}
          {this.state.searching ? <Text style={appStyle.loading}>Loading...</Text> : null}         
        </ScrollView>
         {this.props.contentItems.length > 0 && <MapView
            style={appStyle.map}
            region={{
              latitude: parseFloat(this.props.contentItems[0].details.latitude),
              longitude: parseFloat(this.props.contentItems[0].details.longitude),
              latitudeDelta: 0.02,
              longitudeDelta: 0.02,
            }}
          >{this.props.contentItems.length > 0 && this.props.contentItems.map((item, index) => {
            return <MapView.Marker key={"point-" + index}
              coordinate={{ latitude: parseFloat(item.details.latitude), longitude: parseFloat(item.details.longitude) }}
              title={item.details.name}
              description={item.details.description}
            />
          })}
          </MapView>
          }
      </View>
    )
  }
}

function mapStateToProps(state) {
  return {
    contentTypes: state.contentTypes,
    contentItems: state.contentItems
  };
}

export default connect(mapStateToProps)(Home);
