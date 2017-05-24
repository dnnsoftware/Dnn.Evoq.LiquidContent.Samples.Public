import * as types from './types'
import Api from '../lib/api'

export function getContentTypes(searchText) {
  return (dispatch, getState) => {
    if(!searchText || searchText === '') searchText = 'My';
    return Api.get('/ContentTypes?searchText=' + searchText).then(resp => {
      dispatch(setRetrievedContentTypes(resp));
    }).catch((ex) => {
      console.log(ex);
    });
  }
}

export function getContentItems(id) {
  return (dispatch, getState) => {
    return Api.get('/PublishedContentItems?contentTypeId=' + id).then(resp => {
      dispatch(setRetrievedContentItems(resp));
    }).catch((ex) => {
      console.log(ex);
    });
  }
}

export function setRetrievedContentTypes(contentTypes) {
  return {
    type: types.RETRIEVED_CONTENT_TYPES,
    contentTypes: contentTypes
  }
}

export function setRetrievedContentItems(contentItems) {
  return {
    type: types.RETRIEVED_CONTENT_ITEMS,
    contentItems: contentItems
  }
}