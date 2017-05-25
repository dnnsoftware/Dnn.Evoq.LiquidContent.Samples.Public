var natural = require('natural');
var classifier = new natural.BayesClassifier();
var trainingData = require('./_trainingData')

trainingData.forEach(blogpost => classifier.addDocument(blogpost.content, blogpost.tags))
classifier.train()

module.exports = classifier;
