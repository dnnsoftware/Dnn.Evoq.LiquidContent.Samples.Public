
//,

import {Observable} from 'rxjs';
import $ from 'jquery'

export const PutBlogPost = (content, API_KEY) => {
  const source = Observable.create((observer)=>{

    $.ajax({
        type: 'PUT',
        dataType: 'json',
        url: "http://localhost:3001/api/blog-posts",
        data:{ updates: JSON.stringify(content) },
        headers: {"authorization": `Bearer ${API_KEY}`},
        success: (data) => {
          observer.next(data)
          observer.complete()
        }
    });

  });
  return source;
}
