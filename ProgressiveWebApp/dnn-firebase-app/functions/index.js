const functions = require('firebase-functions');
const admin = require('firebase-admin');
const request = require('request-promise');
const webPush = require('web-push');
const cors = require('cors')({origin: true});

admin.initializeApp(functions.config().firebase);
webPush.setGCMAPIKey(functions.config().dnn.server_key);

function getKeys() {
    return getData("keys").then(storedKeys => {
        var keys;
        if (!storedKeys) {
            keys = webPush.generateVAPIDKeys();
            admin.database().ref("keys").set(keys);
            console.log("Setting keys", keys);
        }
        else {
            keys = storedKeys;
            console.log("Reading keys", keys);
        }
        return keys;
    });
}

function getData(key) {
    return admin.database().ref(key).once("value")
    .then(snapshot => {
        return snapshot.val();
    });
}

function saveSubscription(subscription) {
  admin.database().ref('subscriptions').push({
    subscription
  });
}

function getSubscriptions() {
  return admin.database().ref('subscriptions').once("value")
    .then(snapshot => {
        const subscriptions = [];
        console.log("Getting subscriptions", snapshot.val());
        snapshot.forEach((childSnapshot) => {
            const subscription = childSnapshot.child("subscription").val();
            console.log("Push a susbscription to array", subscription);   
            subscriptions.push(subscription);
        });

        return subscriptions;
    });
}

function saveEvent(event) {
    admin.database().ref('events').push({
        event
    });
}

function getEvents() {
  return admin.database().ref('events').once("value")
    .then(snapshot => {
        const events = [];
        snapshot.forEach((childSnapshot) => {
            const event = childSnapshot.child("event").val();
            events.push({
                messageId: event.messageId,
                sender: event.sentBy.name,
                avatar: event.sentBy.thumbnail,
                updatedAt: event.currentValue.updatedAt,
                contentTypeName: event.currentValue.contentTypeName,
                name:  event.currentValue.name
            });
        });

        return events;
    });
}

/**
 * @function dnnWebhook
 * This is the webhook subscriber. This function
 * handle webhook notification events from Dnn, store it 
 * as events and notify the subscribers
 */
exports.dnnWebhook = functions.https.onRequest((req, res) => {
    console.log("New hook event recieved: ", req.body);
    if (req.body.operationType !== "ContentItemPublished"){
        res.sendStatus(200);
        return;
    }
    if (!!req.body.testMode) {
        res.sendStatus(200);
        return;
    }

    saveEvent(req.body);
    const message = req.body.currentValue.name;
    getKeys().then(keys => {
        webPush.setVapidDetails(`mailto:${functions.config().dnn.support_mail}`, keys.publicKey, keys.privateKey);
        getSubscriptions().then(subscriptions => {
            console.log("New published content: ", message);
            console.log("Calling subscribers: ", subscriptions);
            subscriptions.forEach((subscription) => {
                console.log("Calling subscriber: ", subscription);
                webPush.sendNotification(subscription, JSON.stringify({title: "New Content item published", body: message}))
                .then(function() {
                    console.log("Notification send");
                })
                .catch(function(error) {
                    console.log("Notification error", error);
                });
            });
        });
    });

    res.sendStatus(200);
});

/**
 * @function dnnPushKey
 * Small function to retrieve public key, needed for push notification subscribing
 */
exports.dnnPushKey = functions.https.onRequest((req, res) => {
    cors(req, res, () => {
        getKeys().then(keys => {
            res.json({
                key: keys.publicKey
            });
        });
    });
});

/**
 * @function dnnGetEvents
 * Return all the events since webhook subscription
 */
exports.dnnGetEvents = functions.https.onRequest((req, res) => {
    cors(req, res, () => {
        getEvents().then(event =>
            res.json(
                event
            )
        );
    });
});

/**
 * @function dnnSubscribe
 * Each PWA app client should subscribe to the notification
 * system calling this function
 */
exports.dnnSubscribe = functions.https.onRequest((req, res) => {
    cors(req, res, () => {
        console.log("New subscription event recieved: ", req.body);
        if (!req.body) {
            res.sendStatus(400);
        } else {
            const { subscription } = req.body;
            console.log("New subscription recieved ", subscription);
            saveSubscription(subscription);
            res.sendStatus(200);
        }
    });
});

/**
 * @function dnnWebhookSubscribe
 * Utility fucntion to subscribe to Dnn webhook
 */
exports.dnnWebhookSubscribe = functions.https.onRequest((req, res) => {
    const subscriberUrl = functions.config().dnn.subscriber_url;
    console.log('Trying to subscribe to Dnn webhook ', subscriberUrl);
    const requestConfig = {
        method: 'POST',
        uri: functions.config().dnn.webhook_url,
        headers: {
            'Authorization': 'Bearer ' + functions.config().dnn.token_key
        },
        body: {
            description: "Firebase subscription test",
            url: functions.config().dnn.subscriber_url,
            secret: "dnn-secret-key",
            events: [   
                "ContentItemPublished"
            ]
        },
        json: true
    };
    console.log(requestConfig);
    request(requestConfig).then(response => {
        if (response.statusCode >= 400) {
            console.log('Subscription Error', response);
            res.json({
                status: "error",
                message: response
            });
        }
        else {
            console.log('SUCCESS! You are subscribed to Dnn webhook');
            res.json({
                status: "success",
                message: `${subscriberUrl} has been subscribed to Dnn Liquid Content Webhook`
            });
        }
    }).catch(function(error) {
        res.sendStatus(500);
        console.log(error);
    });;
});