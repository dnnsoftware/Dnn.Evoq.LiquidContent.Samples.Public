const Observable = require('rxjs').Observable;
const request = require('request')
const striptags = require('striptags')

const KEY = `074689b46c8704aa6988f88e7005a806`;

module.exports = (update, API_KEY) => {

  const source = Observable.create((observer) => {
  const url = `https://qa-sc.dnnapi.com:443/api/ContentItems/${update.id}`;

    const options = {
      method:"PUT",
      url: url,
      headers: { 'authorization': `${API_KEY}`},
      json: update
    };

    request(options, (err, resp) => {
      observer.next(resp)
      observer.complete()
    })
  });

  return source;
}
