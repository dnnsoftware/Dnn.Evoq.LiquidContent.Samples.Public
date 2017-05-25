import {Observable} from 'rxjs';
import $ from 'jquery'

export const GetTagSuggestions= (content) => {
  const source = Observable.create((observer)=>{

    $.post("http://localhost:3001/api/tag", {content: JSON.stringify(content) }, (data) => {
        observer.next(data)
        observer.complete()
    });

  })
  return source;
}
