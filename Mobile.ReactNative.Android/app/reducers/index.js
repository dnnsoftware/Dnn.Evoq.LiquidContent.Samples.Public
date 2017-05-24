import { combineReducers } from 'redux';
import * as contentTypesReducer from './contentTypes'

export default combineReducers(Object.assign(
  contentTypesReducer
));
