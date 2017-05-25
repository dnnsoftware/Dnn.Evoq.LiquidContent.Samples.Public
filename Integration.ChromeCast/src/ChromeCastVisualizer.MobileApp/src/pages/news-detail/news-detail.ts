import { Component, } from '@angular/core';
import { LoadingController } from 'ionic-angular';
import { ToastController } from 'ionic-angular';
import { NavController, NavParams } from 'ionic-angular';
import { NewsProvider } from '../../providers/news-provider'

@Component({
    selector: 'pages-news-detail',
    templateUrl: 'news-detail.html',
})
export class NewsDetailPage {

    // Chrome Cast Receiver Application identifier
    applicationId: string = '192680C9';
    // Chrome Cast Namespace
    namespace: string = 'urn:x-cast:com.google.cast.liquidcontent';

    // Chrome Object loaded by Cordova Plugin
    chrome: any = window["chrome"];
    // Chrome Cast session (null by default)
    session: any = null;

    // Set to true when a chrome cast device is found
    deviceFound: boolean = false;

    newDetail: any = null;
    previewHtml: string = "<div/>";

    constructor(
        public toastCtrl: ToastController,
        public loadingCtrl: LoadingController,
        public navCtrl: NavController,
        public navParams: NavParams,
        public newsProvider: NewsProvider) {

        // Get navigation parameter that indicate which news you want to see in the details page
        // Note: this value is set in the list page when you click on view a specific news
        this.newDetail = this.navParams.get("newDetail");
    }

    ionViewWillEnter() {
        this.loadNewDetail();

        if (!this.chrome) {
            console.log('we are not in chrome');
            return;
        }

        // Call initialization for Chrome Cast
        setTimeout(() => this.initializeCastApi(), 2000);
    }

    loadNewDetail() {
        const loader = this.loadingCtrl.create({
            content: "Loading News..."
        });
        loader.present();

        // Call Liquid Content to preview the selected News
        this.newsProvider.previewNew(this.newDetail.id)
            .subscribe(preview => {
                loader.dismiss();
                console.log(preview);
                this.previewHtml = preview.html;
            },
            error => {
                loader.dismiss();
                console.log(error);
                const toast = this.toastCtrl.create({
                    message: 'Error: ' + error,
                    duration: 3000
                });
                toast.present();
            });
    }

    // Utility function to show a toast message
    toast(message) {
        const toast = this.toastCtrl.create({
            message: message,
            duration: 3000
        });
        toast.present();
    }
    
    // Initialization of Chrome Cast API
    initializeCastApi() {
        console.log('initializeCastApi');
        var sessionRequest = new this.chrome.cast.SessionRequest(this.applicationId);
        var apiConfig = new this.chrome.cast.ApiConfig(sessionRequest,
            this.sessionListener.bind(this),
            this.receiverListener.bind(this));

        this.chrome.cast.initialize(apiConfig, this.onInitSuccess.bind(this), this.onError.bind(this));
    }
    
    onInitSuccess() {
        console.log('onInitSuccess');
    }
    
    onError(message) {
        this.toast('Error: ' + message);
        console.log('onError: ' + JSON.stringify(message));
    }
    
    onSuccess(message) {
        console.log('onSuccess: ' + message);
    }

    // session listener during initialization
    sessionListener(e) {
        console.log('New session ID:' + e.sessionId);
        this.session = e;
        this.session.addUpdateListener(this.sessionUpdateListener.bind(this));
        this.session.addMessageListener(this.namespace, this.receiverMessage.bind(this));
    }

    // listener for session updates
    sessionUpdateListener(isAlive) {
        var message = isAlive ? 'Session Updated' : 'Session Removed';
        message += ': ' + this.session.sessionId;
        console.log(message);
        if (!isAlive) {
            this.session = null;
        }
    }

    /**
     * utility function to log messages from the receiver
     * @param {string} namespace The namespace of the message
     * @param {string} message A message string
     */
    receiverMessage(namespace, message) {
        console.log('receiverMessage: ' + namespace + ', ' + message);
    }

    // receiver listener during initialization
    receiverListener(e) {
        console.log(e);
        if (e === 'available') {
            console.log('receiver found');
            this.deviceFound = true;
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
    sendMessage(message) {
        if (this.session != null) {
            this.session.sendMessage(this.namespace, message, 
            this.onMessageSentSuccess.bind(this, 'Message sent: ' + message),
                this.onError.bind(this));
        }
        else {
            this.chrome.cast.requestSession((e) => {
                console.log(e);
                this.session = e;
                this.session.sendMessage(this.namespace, message, 
                this.onMessageSentSuccess.bind(this, 'Message sent: ' +
                    message), this.onError.bind(this));
            }, this.onError.bind(this));
        }
    }

    onMessageSentSuccess(message) {
        this.toast('Casting...');
        console.log('onSuccess: ' + message);
    }

    // cast to chrome cast the preview Html
    cast() {
        this.sendMessage(this.previewHtml);
    }
}
