import {Request} from "../Request";

export interface ISnippetGenerator {
    generate(request: Request): string;
}