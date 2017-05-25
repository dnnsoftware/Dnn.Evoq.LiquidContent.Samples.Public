import {ISnippetGenerator} from "./ISnippetGenerator";
import {Request} from "../Request";

export class JavascriptSnippetGenerator implements ISnippetGenerator {
    generate(request: Request) {
        return `fetch("${request.getUrl()}", {
    method: "${request.getMethod()}",
    headers: {
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ${request.getApiKey()}'
    }
}).then(response => 
    response.json().then(data => {
        if (response.ok) {
            console.log("success", data);
        } else {
            console.log("fail", response);
        }        
    }));`;
    }

    test() {
        fetch("https://dnnapi.com/content/api/ContentTypes", {
            method: "GET",
            headers: {
                'Content-Type': 'application/json',
                'Authorization': 'Bearer '
            }
        });
    }
}