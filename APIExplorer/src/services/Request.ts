export class Request {

    method: string;
    apiKey: string;
    url: string;
    body: string;

    status: number;
    text: string;

    constructor(method: string, apiKey: string, url: string, body: string) {
        this.method = method;
        this.apiKey = apiKey;
        this.url = url;
        this.body = body;
    }

    public getMethod() {
        return this.method;
    }

    public getApiKey() {
        return this.apiKey;
    }

    public getUrl() {
        return this.url;
    }

    public getStatus(): number {
        return this.status;
    }

    public setStatus(status: number) {
        this.status = status;
    }

    public getResponseText(): string {
        return this.text;
    }

    public setResponseText(text: string) {
        this.text = text;
    }

    public getRawRequestText() {
    return `${this.method} ${this.url}
Content-Type: application/json
Authorization: Bearer ${this.apiKey}`;
    }
}