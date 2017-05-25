import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions } from '@angular/http';
import 'rxjs/add/operator/map';

@Injectable()
export class NewsProvider {
 
  // Content Item api url
  private apiUrl: string = 'https://dnnapi.com/content/api/contentItems/';

  // Hardcoded News Content Type Identifier
  private newsContentTypeId: string = 'ffda3e66-12f9-43de-a886-a8be21102915';

  // Query string to get top 5 content items, ordered by created At desc filtering by Content Type News
  private queryString: string = '?startIndex=0&maxItems=5&fieldOrder=createdAt&orderAsc=false&searchText=&contentTypeId=' + this.newsContentTypeId;

  // Standard Liquid Content API KEY (used to get news)
  private apiKey: string = '3b20949657bda770b916475d6563ddad';
  
  // Api Preview Url Endpoint (this api is used today to show a preview of a visualizer in the embed code wizard 
  // (it is not available as part of the API KEY)
  private apiPreviewUrl: string = 'https://dnnapi.com/content/api/visualizerEngine/preview/';

  // Hardcoded Card New Visualizer Identifier
  private visualizerId: string = '45cd16ac-53f9-4390-ac94-b1a53588e2b6';

   // 1 hour live token (this token is not the API KEY)
  private specialApiKey: string = '858751c42905e3715ff9e74447120daf';

  constructor(public http: Http) {
    
  }

  // Get top 5 News
  getTop5News(){
    const headers = new Headers({
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + this.apiKey
    });
    const options = new RequestOptions({
        headers: headers
    });
    return this.http.get(this.apiUrl + this.queryString, options)
      .map(response => response.json());
  }

  // Given a news id, preview it using a pre-existing and hardcoded visualizer
  previewNew(newsId){    
    const headers = new Headers({
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + this.specialApiKey
    });
    const options = new RequestOptions({
        headers: headers
    });
    return this.http.get(this.apiPreviewUrl + '?visualizerId=' + this.visualizerId + '&contentItemIds=' + newsId, options)
      .map(response => response.json());
  }
}