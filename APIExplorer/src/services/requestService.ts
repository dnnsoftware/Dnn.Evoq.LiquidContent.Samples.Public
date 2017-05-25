import {Request} from "./Request";

export default class RequestService {
    static request(method: string, apiKey: string, url: string, body: string): Promise<Request> {

        const headers = {
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + apiKey
        }
        let requestInfo;
        if (method == 'POST' || method == 'PUT') {
            requestInfo = {
                method,
                body,
                headers
            }; 
        } else {
            requestInfo = {
                method,
                headers
            }; 
        }

        const request = new Request(method, apiKey, url, body);
        return fetch(url, requestInfo).then(response => 
            response
                .json().then(jsonResponse => {
                    request.setStatus(response.status);
                    const text = JSON.stringify(jsonResponse, null, 4);
                    request.setResponseText(text);
                    return request
                }).catch(error => {
                    request.setStatus(response.status);
                    request.setResponseText("");
                    return request;
                })
        );
    }
}