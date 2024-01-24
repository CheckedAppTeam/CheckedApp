import React from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';


const InputWithIcon = ({ placeholder, imagePath, type, onChange}) => (
  <div className='input-group input-group-icon'>
    <input type={type} placeholder={placeholder} onChange={onChange} />
    {type === 'password' && (
        <FontAwesomeIcon style={{position:'absolute',top:"35%",right:"10px",translateY:"50%",cursor:"pointer"}}className='password-field'  />
      )}
    <div className='input-icon' >
      {imagePath ? <img  src={imagePath} alt={placeholder} /> : <></>}
      
    </div>
  </div>
);

export default InputWithIcon;
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
