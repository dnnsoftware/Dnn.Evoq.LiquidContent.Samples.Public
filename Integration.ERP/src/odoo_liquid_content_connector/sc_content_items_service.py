from . import sc_api

""" Methods to call create/update content items API methods in Liquid Content """


def add_content_item(content, backend):
    url = backend.apiUrl + '/api/ContentItems/?publish=true'
    response = sc_api.post(url, content, backend.apiKey)
    return response['id']


def update_content_item(sc_id, content, backend):
    url = backend.apiUrl + '/api/ContentItems/' + sc_id + '?publish=true'
    response = sc_api.put(url, content, backend.apiKey)
    return response['id']

