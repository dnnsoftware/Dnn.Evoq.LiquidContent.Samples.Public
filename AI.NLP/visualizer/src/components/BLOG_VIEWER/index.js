import React from 'react'
import {ADD_TAGS} from '../ADD_TAGS'
import {SUGGESTED_TAGS} from "../SUGGESTED_TAGS";

import "./style.css";

export const BLOG_VIEWER = (props) => {
  const post = props.post
  const tags = post.tags ? post.tags.map(tag => (<p className="tags">{tag}</p> )) : <span></span>
  const render = () => post.details.blogTitle ? template : null

  const template = (
    <div style={{padding:'10px'}}>
      <h1>{props.post.details.blogTitle}</h1>
      <p>{props.post.details.content}</p>

      <div>
        <p className="tag-header">Current Tags</p>
        { tags }
      </div>

      <div>
         <SUGGESTED_TAGS suggestedTags={props.suggestedTags} applyTags={props.applyTags} />
      </div>
    </div>
  )

  return (
    <div className="BLOG_VIEWER">
      {render()}
    </div>

  )
}
