var express = require('express');
var router = express.Router();

const Observable = require('rxjs').Observable;
const API = require('./observables')
const natural = require('./natural')


router.post('/blog-posts', function(req, res) {
  const APIKEY = req.headers['authorization'];

  let blogData = [];
  const next = (data) => blogData=data;
  const err = () => {}
  const complete = () => res.json({ data:blogData })

  const stream = API.BlogPostStream(APIKEY).map(data => {
    blogData.push(data);
    return data;
  });

  stream.subscribe(next, err, complete)

});


router.put('/blog-posts', function(req, res) {
  const APIKEY = req.headers['authorization'];

  const updates = JSON.parse(req.body.updates)
  let resp = null;
  const stream = API.PutBlogPostStream(updates, APIKEY)

  const next = (data) => resp = data;
  const err = () => null
  const complete = () => res.json({resp:resp})
  stream.subscribe(next, err, complete)

});


router.post("/tag", (req, res) => {
  let content = req.body.content
  content =  JSON.parse(content);
  const tags = natural.classify(content)
  res.json({tags: tags});
})



module.exports = router;
