import createReducer from '../lib/createReducer'
import * as types from '../actions/types'

export const contentTypes = createReducer({}, {
  [types.RETRIEVED_CONTENT_TYPES](state, action) {
    return action.contentTypes;
  }
});

export const contentItems = createReducer({}, {
  [types.RETRIEVED_CONTENT_ITEMS](state, action) {
    return action.contentItems;
  }
});


