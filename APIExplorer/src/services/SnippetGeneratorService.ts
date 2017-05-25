import {Request} from "./Request";
import {ISnippetGenerator} from "./snippetGenerators/ISnippetGenerator";
import {JQuerySnippetGenerator} from "./snippetGenerators/JQuerySnippetGenerator";
import {JavascriptSnippetGenerator} from "./snippetGenerators/JavascriptSnippetGenerator";

export class SnippetGeneratorService {
    private generators: any = { 
        "Javascript": new JavascriptSnippetGenerator(),
        "JQuery": new JQuerySnippetGenerator(),
    }
     
    public generate(target: string, request: Request): string {
        return this.generators[target].generate(request);
    }
}
