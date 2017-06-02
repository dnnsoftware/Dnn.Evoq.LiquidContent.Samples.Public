var apiKey = 'd13c6455d78656bcd66dda0e7d9c633f';
var tabId = parseInt(window.location.search.substring(1));
var pattern = new RegExp(/(http|https)\:\/\/.*\/content\/assets\/.*\/visualizers\/.+/);

window.addEventListener("load", function() {
  chrome.debugger.sendCommand({tabId:tabId}, "Network.enable");
  chrome.debugger.onEvent.addListener(onEvent);
});

window.addEventListener("unload", function() {
  chrome.debugger.detach({tabId:tabId});
});

var requests = {};

function onEvent(debuggeeId, message, params) {
  if (tabId != debuggeeId.tabId)
    return;

  if (message == "Network.requestWillBeSent") {
	if (pattern.test(params.request.url)) {
		var requestDiv = requests[params.requestId];
		if (!requestDiv) {
			
			  var requestDiv = document.createElement("div");
			  requestDiv.className = "request";
			  requests[params.requestId] = requestDiv;
			  
			  // Url Line
			  //var urlLine = document.createElement("div");
			  //urlLine.textContent = params.request.url;
			  //requestDiv.appendChild(urlLine);
			  
			  // Get visualizer data
			  var result = getVisualizerData(params.request.url);
			  
			  // File Info
			  var fileLine = document.createElement("div");
				  
				  // Visualizer data
				  var visualizerName = document.createElement("h2");
				  visualizerName.textContent = 'Visualizer: ' + result[1] + ' [' + result[0] + ']';
			      fileLine.appendChild(visualizerName);
				  
				  // File type
				  var fileType = document.createElement("p");
				  fileType.textContent = 'File type: ' + result[2];
			      fileLine.appendChild(fileType);
				  
				  // File link
				  var fileLink = document.createElement("a");
				  fileLink.textContent = params.request.url;
				  fileLink.href = params.request.url;
				  fileLink.target = '_blank';
				  fileLine.appendChild(fileLink);
			  requestDiv.appendChild(fileLine);
			  
		}

		if (params.redirectResponse)
		  appendResponse(params.requestId, params.redirectResponse);

		var requestLine = document.createElement("div");
		requestLine.textContent = "\n" + params.request.method + " " +
			parseURL(params.request.url).path + " HTTP/1.1";
		requestDiv.appendChild(requestLine);
		document.getElementById("container").appendChild(requestDiv);
	}
  } else if (message == "Network.responseReceived") {
		appendResponse(params.requestId, params.response);
  }
}

function appendResponse(requestId, response) {
  var requestDiv = requests[requestId];
  requestDiv.appendChild(formatHeaders(response.requestHeaders));

  var statusLine = document.createElement("div");
  statusLine.textContent = "\nHTTP/1.1 " + response.status + " " +
      response.statusText;
  requestDiv.appendChild(statusLine);
  requestDiv.appendChild(formatHeaders(response.headers));
}

function formatHeaders(headers) {
  var text = "";
  for (name in headers)
    text += name + ": " + headers[name] + "\n";
  var div = document.createElement("div");
  div.textContent = text;
  return div;
}

function parseURL(url) {
  var result = {};
  var match = url.match(
      /^([^:]+):\/\/([^\/:]*)(?::([\d]+))?(?:(\/[^#]*)(?:#(.*))?)?$/i);
  if (!match)
    return result;
  result.scheme = match[1].toLowerCase();
  result.host = match[2];
  result.port = match[3];
  result.path = match[4] || "/";
  result.fragment = match[5];
  return result;
}

function getVisualizerData(fileUri) {
	// Get visualizer list
    var xhttp = new XMLHttpRequest();
    xhttp.open("GET", "https://qa.dnnapi.com/content/api/Visualizers?maxitems=50", false);
	xhttp.setRequestHeader("Authorization", "Bearer " + apiKey);
    xhttp.setRequestHeader("Content-type", "application/json");
    xhttp.send();
    var visualizers = JSON.parse(xhttp.responseText);
	
	// Get the visualizer that matches the downloaded files  
	for(var i = 0; i < visualizers.documents.length; i++) {
		if(visualizers.documents[i].isSystem == false)	{

			// Get visualizer
			xhttp = new XMLHttpRequest();
			xhttp.open("GET", "https://qa.dnnapi.com/content/api/Visualizers/" + visualizers.documents[i].id, false);
			xhttp.setRequestHeader("Authorization", "Bearer " + apiKey);
			xhttp.setRequestHeader("Content-type", "application/json");
			xhttp.send();
			var visualizer = JSON.parse(xhttp.responseText);
			
			// Check that the path of the file matches with the .css file for the visualizer
			if (visualizer.cssFiles.length > 0) {
				if (fileUri.includes(visualizer.cssFiles[0].uri)) {
					return [visualizers.documents[i].id, visualizers.documents[i].name, 'css'];
				}
			}
			
			// Check that the path of the file matches with the .js file for the visualizer
			if (visualizer.scripts.length > 0) {
				if (fileUri.includes(visualizer.scripts[0].uri)) {
					return [visualizers.documents[i].id, visualizers.documents[i].name, 'script'];
				}
			}
		}
	}
}