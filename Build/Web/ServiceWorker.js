const cacheName = "DefaultCompany-tswift-lang-0.1.1";
const contentToCache = [
  "Build/Web.loader.js",
  "Build/Web.framework.js.unityweb",
  "Build/Web.data.unityweb",
  "Build/Web.wasm.unityweb",
  "TemplateData/style.css"

];

self.addEventListener('install', function (e) {
  console.log('[Service Worker] Install');
  self.skipWaiting();

  e.waitUntil((async function () {
    const cache = await caches.open(cacheName);
    console.log(`[Service Worker] Using cache: ${cacheName}`);
    await cache.addAll(contentToCache);
  })());
});

self.addEventListener("activate", (event) => {
  console.log('[Service Worker] Activate')

  event.waitUntil(async function () {
    for (let name of (await caches.keys())) {
      if (!name == cacheName) {
        console.log(`[Service Worker] deleted cache: ${name}`)
        return caches.delete(cacheName);
      }
      clients.claim();
    }
  });
});

self.addEventListener('fetch', function (e) {
  e.respondWith((async function () {
    let response = await caches.match(e.request);
    console.log(`[Service Worker] Fetching resource: ${e.request.url}`);
    if (response) { return response; }

    response = await fetch(e.request);
    const cache = await caches.open(cacheName);
    console.log(`[Service Worker] Caching new resource: ${e.request.url}`);
    cache.put(e.request, response.clone());
    return response;
  })());
});
