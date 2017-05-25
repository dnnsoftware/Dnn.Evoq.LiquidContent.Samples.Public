import React from 'react'

import "./style.css";

export const BLOG_LISTING = (props) => (
  <div className="BLOG_LISTING">
    <ul>
    {
      props.list.map(blogpost => {
        return (
          <li key={blogpost.id} onClick={ () => props.setActiveBlog(blogpost) }>
            {blogpost.name}
          </li>
        )
      })
    }
    </ul>
  </div>
)
