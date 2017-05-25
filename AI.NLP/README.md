
# NODE EXPRESSJS REACT AUTO TAGGING APPLICATION
This auto tagging application is a demonstration of how a user can automatically
generate tags for liquid content using a combination of machine learning and a natural langauge library for for JavaScript in a NodeJS application that serves a React front-end. By using already existing liquid content items, tags can be autoamtically generated from a local model hosted on the NodeJS server.

## On Initial Launch
``` You will notice that there are no tags associated to the blog post pulled from liquid content```
![img](https://github.com/dnnsoftware/Dnn.Evoq.LiquidContent.Samples/blob/master/Daniel/images/before.png?raw=true)

## After pushing "Apply"
``` You will notice after pushing apply and inspecting the network tab, the liquid content is being updated```
![img](https://github.com/dnnsoftware/Dnn.Evoq.LiquidContent.Samples/blob/master/Daniel/images/apply.png?raw=true)

## Refreshing the page will result in the selected tags being present
``` You will notice that the "Current Tags" have been updated for the liquid content item ```
![img](https://github.com/dnnsoftware/Dnn.Evoq.LiquidContent.Samples/blob/master/Daniel/images/after.png?raw=true)

## The Inspiration
in the 1980's we saw the revolution of the computer entering homes and becoming a norm or the business environment. Soon the computer replaced many ‘old-fashioned’ and manual processes. Today, we are seeing another revolution, not only do we need hardware and electronics that are capable of processing AI, deep learning and automation but also with software design. The ability to solve common problems in web development with the new paradigm of how can it be done with machine learning is going to be in demand in the future.  Being able to automate and bring the power of AI to the web and other connected devices will be a common place for many web and connected devices going forward.
To keep with this trend, this I decided to showcase how simple it can be to start in the seemingly complicated realm of machine learning. While this is a very rudimentary attempt and demonstration of machine learning that does not cover any of the topics of gradient descent or complicated learning algorithms, for the front-end/back-end web developer, it would be wonderful if these processes could be more plug and play anyway, similar to how web applications are built today.

## How does this relate to developers
Developers utilizing liquid content can leverage the data already in liquid content to generate tags, assign categories, generate additional content or even automatically create new content items through analyzing the newly created content. In this demonstration, we show how a local copy of hardcoded liquid content can be used to suggest proper tags of a new piece of content for a blog post. This elevates some of the work a blog poster will need to think about by having suggestions available one the new content is loaded. 


## How does this relate to business value
Businesses and companies will need to leverage the power of machine learning, deep learning and AI to stay competitive in their industries. It will become more than necessary for the user experience that their web applications can learn from their users and already existing content to provide a more catered and customized experience for that user. Imagine posting new products to a catalog that will already price the item competitively with an already existing inventory or through a localized data file of prices from competitors.


# Requirements / Dependancies
  - NodeJS & Express JS (web server and REST API )
  - Natural JS (natural langauge processing with machine learning)
  - RxJs (Used for async calls and creating async streams)

# 1. How to run
#### Download and install Node and NPM
```
https://nodejs.org/en/download/
```
#### Download source
```
git clone https://github.com/dnnsoftware/Dnn.Evoq.LiquidContent.Samples
```
#### cd source/folder
``` 
cd source/Daniel 
```
#### Run installation and start commands
```
npm install
npm start
navigate to http://localhost:3001
```

# 1.2 How to rebuild front-end application project
```
cd source/Daniel/visualizer
npm install
npm run build
cd ..
npm start
```


# 2. Architecture diagram
This application works by serving a static React Application to the browser, which provides an interface for the user to indirectly interact with Liquid Content through a REST API provided by the NodeJS Server. The NodeJS server also holds the models for the NLP machine learning auto tagging features and provides a rest API for the front-end to obtain tags based on the content of a blog post. 
![img](https://github.com/dnnsoftware/Dnn.Evoq.LiquidContent.Samples/blob/master/Daniel/images/architecture.png?raw=true)

# 3. Important Files
#### Location of front-end asynchronous observable http requests:
```source/visualizer/src/....```
![img](https://github.com/dnnsoftware/Dnn.Evoq.LiquidContent.Samples/blob/master/Daniel/images/autotagging-front-end-async-requests.png?raw=true)

#### Location of front-end React components:
```source/visualizer/src/components/...```
![img](https://github.com/dnnsoftware/Dnn.Evoq.LiquidContent.Samples/blob/master/Daniel/images/autotagging-front-end-react-components.png?raw=true)

####  Location of NodeJS ansyncrhonus observable http requests to liquid content:
```source/routes/observables/http/...```
![img](https://github.com/dnnsoftware/Dnn.Evoq.LiquidContent.Samples/blob/master/Daniel/images/autotagging-nodejs-liquidcontent-httprequests.png?raw=true)

#### Location of Node JS - Natural Language Processing Files: 
```source/routes/natural/...```
![img](https://github.com/dnnsoftware/Dnn.Evoq.LiquidContent.Samples/blob/master/Daniel/images/autotagging-nodejs-nlp-files.png?raw=true)


