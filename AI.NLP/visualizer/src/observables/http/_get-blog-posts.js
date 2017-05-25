import {Observable} from 'rxjs';
import $ from 'jquery'

export const GetBlogPosts = (APIKEY) => {
  const source = Observable.create((observer)=>{
    console.log('in request')

    $.ajax({
        type: 'POST',
        url: "http://localhost:3001/api/blog-posts",
        headers: {"authorization": `Bearer ${APIKEY}`},
        success: (data) => {
          console.log(data);
          observer.next(data)
          observer.complete()
        }
    });


  })
  return source;
}
