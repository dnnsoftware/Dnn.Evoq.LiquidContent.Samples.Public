import React, { Component } from 'react';
import logo from './logo.svg';
import './App.css';

import {APIKEY_INPUT} from './components/APIKEY_INPUT';
import {BLOG_LISTING} from './components/BLOG_LISTING';
import {BLOG_VIEWER} from './components/BLOG_VIEWER';


import {GetBlogPosts, GetTagSuggestions, PutBlogPost} from "./observables/"

import {Observable} from 'rxjs';

class App extends Component {
  constructor(){
    super()
    this.state = {
      API_KEY: null,
      BLOG_LIST: [],
      ACTIVE_BLOG:{details:{}},
      SUGGESTED_TAGS:[]
    }

  }

  getBlogPosts = () => {
    const getBlogPosts = GetBlogPosts(this.state.API_KEY);
    getBlogPosts.subscribe( (res) => this.setState({BLOG_LIST:res.data}) );
  }

  setACTIVE_BLOG = (ACTIVE_BLOG) => {
    this.setState({ACTIVE_BLOG})
    console.log(ACTIVE_BLOG)
    const stream = GetTagSuggestions(ACTIVE_BLOG)

    stream.subscribe( data => {
      this.setState({SUGGESTED_TAGS: data.tags})
    })

  }

  setAPI_KEY = (e) => {
    const target = e.target;
    const API_KEY= target.value
    this.setState({API_KEY})
  }

  setBLOG_LIST = (BLOG_LIST) => {
    this.setState({BLOG_LIST})
  }

  applySUGGESTED_TAGS = () => {
    const active_blog = Object.assign({},this.state.ACTIVE_BLOG);
    active_blog.tags = this.state.SUGGESTED_TAGS.split(',');
    const stream = PutBlogPost(active_blog, this.state.API_KEY)
    stream.subscribe((data)=>console.log(data))
  }


  render() {
    return (
      <div className="App">
        <APIKEY_INPUT getBlogPosts={this.getBlogPosts} setAPIKEY={this.setAPI_KEY}/>
        <BLOG_LISTING list={this.state.BLOG_LIST}   setActiveBlog={this.setACTIVE_BLOG} />
        <BLOG_VIEWER  post={this.state.ACTIVE_BLOG} suggestedTags={this.state.SUGGESTED_TAGS} applyTags={this.applySUGGESTED_TAGS}/>
      </div>
    );
  }
}

export default App;
