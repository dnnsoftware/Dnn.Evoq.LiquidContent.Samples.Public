const classifier = require('./_classifier');

module.exports = (blogpost) => {
  
  const tags = classifier.classify(blogpost.details.content);

  return tags;
}
