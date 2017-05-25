const Observable = require('rxjs').Observable;
const request = require('request')
const striptags = require('striptags')

const BlogPostStream = (API_KEY) => {
  const source = Observable.create((observer)=>{

    const KEY = `074689b46c8704aa6988f88e7005a806`;
    const url = "https://qa.dnnapi.com/content/api/ContentItems/?startIndex=0&maxItems=48&fieldOrder=createdAt&orderAsc=false&searchText=&contentTypeId=028a4f48-a203-463e-9991-4c962c6b3a31";

    const options = {
      url: url,
      headers: {
        'authorization': `${API_KEY}`
      }
    }

    request(options, (err,resp, respbody)=>{
      let documents = JSON.parse(respbody).documents;
      observer.next(documents);
      observer.complete();
    });

  });

return source;

}

module.exports = BlogPostStream
