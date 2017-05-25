import React from 'react';

import "./style.css";

export const APIKEY_INPUT  = (props) => (
  <div className="APIKEY_INPUT">
    <input type="text" placeholder="API KEY" onChange={ props.setAPIKEY}/>
    <button onClick={ ()=>props.getBlogPosts() }>submit</button>
  </div>


)
