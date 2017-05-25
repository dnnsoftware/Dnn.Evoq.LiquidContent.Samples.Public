(function () {
    var applicationID = '192680C9';
    var namespace = 'urn:x-cast:com.google.cast.liquidcontent';
    var session = null;
    var initializedVisualizer = false;

    if (!chrome) {
        console.log('we are not in chrome');
        return;
    }

    /**
     * Call initialization for Cast
     */
    if (!chrome.cast || !chrome.cast.isAvailable) {
        setTimeout(initializeCastApi, 1000);
    }

    /**
     * initialization
     */
    function initializeCastApi() {
        var sessionRequest = new chrome.cast.SessionRequest(applicationID);
        var apiConfig = new chrome.cast.ApiConfig(sessionRequest,
            sessionListener,
            receiverListener);

        chrome.cast.initialize(apiConfig, onInitSuccess, onError);
    }

    function initVisualizers() {
        var attributeName = "data-dnn-content-items";

        // get all visualizers elements
        var elements = document.querySelectorAll("[" + attributeName + "]");
        
        for (var i = 0; i < elements.length; i++) {
            // get visualizer element
            var element = elements[i];
            // get module container
            var parent = element.parentElement;
            // create a wrapper div
            var div = document.createElement('div');
            div.className = 'chromeCastVisualizer';
            // append it to the parent
            parent.appendChild(div);
            // create a click event listener on the div
            div.addEventListener("click", function onClick(e) {
                // on click take the visualizer element and send its inner HTML to the cast receiver
                var parent = e.target.parentElement;
                sendMessage(parent.firstElementChild.innerHTML);
            });
        }
    }

    /**
     * initialization success callback
     */
    function onInitSuccess() {
        console.log('onInitSuccess');
    }

    /**
     * initialization error callback
     */
    function onError(message) {
        console.log('onError: ' + JSON.stringify(message));
    }

    /**
     * generic success callback
     */
    function onSuccess(message) {
        console.log('onSuccess: ' + message);
    }

    /**
     * session listener during initialization
     */
    function sessionListener(e) {
        console.log('New session ID:' + e.sessionId);
        session = e;
        session.addUpdateListener(sessionUpdateListener);
        session.addMessageListener(namespace, receiverMessage);

        if (!initializedVisualizer) {
            initVisualizers();
            initializedVisualizer = true;
        }
    }

    /**
     * listener for session updates
     */
    function sessionUpdateListener(isAlive) {
        var message = isAlive ? 'Session Updated' : 'Session Removed';
        message += ': ' + session.sessionId;
        console.log(message);
        if (!isAlive) {
            session = null;
        }
    }

    /**
     * utility function to log messages from the receiver
     * @param {string} namespace The namespace of the message
     * @param {string} message A message string
     */
    function receiverMessage(namespace, message) {
        console.log('receiverMessage: ' + namespace + ', ' + message);
    }

    /**
     * receiver listener during initialization
     */
    function receiverListener(e) {
        if (e === 'available') {
            console.log('receiver found');
        }
        else {
            console.log('receiver list empty');
        }
    }

    /**
     * send a message to the receiver using the custom namespace
     * receiver CastMessageBus message handler will be invoked
     * @param {string} message A message string
     */
    function sendMessage(message) {
        console.log(message);
        if (session != null) {
            session.sendMessage(namespace, message, onSuccess.bind(this, 'Message sent: ' + message),
            onError);
        }
        else {
            chrome.cast.requestSession(function (e) {
                session = e;
                session.sendMessage(namespace, message, onSuccess.bind(this, 'Message sent: ' +
                message), onError);
            }, onError);
        }
    }
})();