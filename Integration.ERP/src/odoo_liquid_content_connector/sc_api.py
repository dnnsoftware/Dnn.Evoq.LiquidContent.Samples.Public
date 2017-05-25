import requests
import json

""" Methods to call Liquid Content API"""


def get_api_version(url):

    full_url = url + '/api/Version'
    try:
        response = requests.get(full_url)

        if response.status_code == 200:
            return response.json()['version']

        return 'Error:' + str(response.status_code) + '. ' + response.text

    except Exception as ex:
        return ex.message


def test_api_connection(url, key):

    full_url = url + '/api/ContentItems/?startIndex=0&maxItems=5&fieldOrder=createdAt&orderAsc=false&searchText='
    try:
        headers = {'Authorization': 'Bearer ' + key}
        response = requests.get(full_url, headers=headers)

        if response.status_code == 200:
            return "OK"

        return 'Error:' + str(response.status_code) + '. ' + response.text

    except Exception as ex:
        return ex.message


def post(url, payload, key):

    headers = {'Authorization': 'Bearer ' + key, 'Content-Type': 'application/json'}
    return handle_http_response(requests.post(url, data=json.dumps(payload), headers=headers))


def handle_http_response(response):

    if 200 <= response.status_code < 300:
        return response.json()

    if response.status_code == 422:
        json = response.json()
        raise Exception(json['developerMessage'] + '.Code =' + str(response.status_code))

    if response.status_code == 400:
        json = response.json()
        raise Exception(json['message'] + '.Code =' + str(response.status_code))

    raise Exception(str(response.status_code) + '- An unexpected error has been received from the server.')


def put(url, payload, key):

    headers = {'Authorization': 'Bearer ' + key, 'Content-Type': 'application/json'}
    return handle_http_response(requests.put(url, data=json.dumps(payload), headers=headers))
