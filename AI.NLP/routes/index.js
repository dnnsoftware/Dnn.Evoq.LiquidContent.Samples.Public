var express = require('express');
var router = express.Router();
var path = require('path')

const Observable = require('rxjs').Observable;
const API = require('./observables')
const natural = require('./natural')


router.get('/static/:dir/:filename', (req, res)=>{
  console.log('GET FILE')
  var dir = req.params.dir
  var filename = req.params.filename
  var file = path.join(__dirname, `../public/${dir}/${filename}`)
  console.log(file)
  res.sendFile(path.join(__dirname, `../public/${dir}/${filename}`))
})

router.get('/', function(req, res) {
  res.render("index")
});

module.exports = router;
