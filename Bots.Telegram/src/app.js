const Telegraf = require('telegraf');
const { Extra, Markup } = Telegraf;
const commandParts = require('telegraf-command-parts');
import config from './config';
import reviewService from './reviewService';
import reviewManager from './reviewManager';

const app = new Telegraf(config.telegramBotToken);

app.use(Telegraf.memorySession());
app.use(commandParts());

app.command('start',
  ctx => ctx.reply('Welcome ' + ctx.from.first_name + ' to the DNN Draft Reviews Bot!')
);

app.command('getdrafts', (ctx) => {
  const draftReviews = reviewService.getDraftReviews()
  .then(
    result => {
      reviewManager.setReviews(result);
      ctx.reply(reviewManager.printReviews());
    },
    error => console.log(error)
  );

  ctx.reply('Requesting draft reviews...');
});

app.command('publish', (ctx) => {
  const { args } = ctx.state.command;

  const reviews = reviewManager.getReviews();
  if (!reviews.length) {
    return ctx.reply('No reviews loaded, maybe you want to /getdrafts and then publish one of the results');
  }

  if (!args) {
    const reviews = reviewManager.getReviews();
    let buttons = [];
    reviews.forEach(
      (review, i) => buttons.push('/publish ' + (i + 1))
    );

    return ctx.reply('Please, include the index of the review you want to publish.\nExample: /publish 1',
      Markup.keyboard(buttons)
        .oneTime()
        .resize()
        .extra());
  }

  const reviewToPublish = reviewManager.getReview(args);
  if (!reviewToPublish) {
    return ctx.reply('The review is not loaded, maybe you want to /getdrafts and then publish one of the results');
  }

  return reviewService.publishReview(reviewToPublish)
  .then(
    response => ctx.reply('The review has been published'),
    reason => ctx.reply('An error has ocurred')
  );
});


app.startPolling();
console.log('Telegram Bot has started...');
