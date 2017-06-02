
chrome.browserAction.onClicked.addListener(function(tab) {
  chrome.debugger.attach({tabId:tab.id}, version,
      onAttach.bind(null, tab.id));
});

var version = "1.0";

function onAttach(tabId) {
  if (chrome.runtime.lastError) {
    alert(chrome.runtime.lastError.message);
    return;
  }

  chrome.windows.create(
      {url: "LCfileslog.html?" + tabId, type: "popup", width: 800, height: 600});
}
