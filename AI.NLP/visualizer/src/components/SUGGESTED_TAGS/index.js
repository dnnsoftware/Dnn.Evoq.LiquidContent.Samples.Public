import React from "react"

export const SUGGESTED_TAGS = (props) => {

  return (

      <div>
        <p className="tag-header">Recommended Tags</p>
        <p className="tags">{props.suggestedTags}</p>
        <button onClick={ ()=> props.applyTags() }>Apply</button>
      </div>
  )
}
