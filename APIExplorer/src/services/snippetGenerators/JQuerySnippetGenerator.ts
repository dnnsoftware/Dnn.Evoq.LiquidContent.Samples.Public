import {ISnippetGenerator} from "./ISnippetGenerator";
import {Request} from "../Request";

export class JQuerySnippetGenerator implements ISnippetGenerator {
    generate(request: Request) {
        return `$.ajax({
  method: "${request.getMethod()}",
  url: "${request.getUrl()}",
  dataType: "json",
  headers: {
  	Authorization: "Bearer ${request.getApiKey()}"
  }
}).done(function (data) {
	console.log("success", data);
}).fail(function (jqXHR, textStatus) {
	console.log("fail", textStatus);
});`;

    }
}