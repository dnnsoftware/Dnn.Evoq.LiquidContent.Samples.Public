(function(){
  'use strict';

  const CACHE_VERSION = 'v1';

  // Config Service Worker Cache
  self.addEventListener('install', (event) => {
    console.log('Service worker installing...');

    event.waitUntil(
      caches.open(CACHE_VERSION)
      .then(cache => cache.addAll([
        'index.html'
      ]))
    );
  });

  // Accept push notifications
  self.addEventListener('push', (event) => {
      console.log("Event recieved", event.data.text());
      const json = JSON.parse(event.data.text());

      event.waitUntil(
        self.registration.showNotification(json.title, {
          body: json.body,
        })
      );
  });
})();