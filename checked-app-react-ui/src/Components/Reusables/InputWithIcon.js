import React from 'react'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'

const InputWithIcon = ({ placeholder, imagePath, type, onChange }) => (
  <div className='input-group input-group-icon'>
    <input type={type} placeholder={placeholder} onChange={onChange} />
    <div className='input-icon'>
      {imagePath ? <img src={imagePath} alt={placeholder} /> : <></>}
    </div>
  </div>
)

export default InputWithIcon
//import React from 'react';
//
//const InputWithIcon = ({ placeholder, imagePath, type, onFocus, onBlur,onChange }) => (
//  <div className='input-group input-group-icon'>
//    <input type={type} placeholder={placeholder} onFocus={onFocus} onBlur={onBlur} onChange={onChange}/>
//
//      {imagePath ? <img  src={imagePath} alt={placeholder} /> : <></>}
//
//    </div>
//);
//
//export default InputWithIcon;
