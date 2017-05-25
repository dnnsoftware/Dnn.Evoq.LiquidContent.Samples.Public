import { Component } from '@angular/core';
import { NavController } from 'ionic-angular';
import { LoadingController } from 'ionic-angular';
import { ToastController } from 'ionic-angular';
import { NewsProvider } from '../../providers/news-provider'
import { NewsDetailPage } from '../news-detail/news-detail';

@Component({
  selector: 'page-news',
  templateUrl: 'news.html'
})
export class NewsPage {
  news: Array<any> = [];

  constructor(
    public toastCtrl: ToastController,
    public loadingCtrl: LoadingController, 
    public navCtrl: NavController, 
    public newsProvider: NewsProvider) {    
  }
  
  ionViewDidLoad() {
    this.loadNews();
  }

  refresh() {
    this.loadNews();
  }
  
  loadNews() {
    const loader = this.loadingCtrl.create({
      content: "Fetching your news..."
    });
    loader.present();

    // Get from Liquid Content API the top 5 news
    this.newsProvider.getTop5News().subscribe(news => {
      loader.dismiss();
      this.news = news.documents;
    },
    error => {
      loader.dismiss();
      const toast = this.toastCtrl.create({
        message: 'Error: ' + error,
        duration: 3000
      });
      toast.present();
    });
  }

  viewNewDetail(newDetail) {
    this.navCtrl.push(NewsDetailPage, {
      newDetail
    });
  }
}
