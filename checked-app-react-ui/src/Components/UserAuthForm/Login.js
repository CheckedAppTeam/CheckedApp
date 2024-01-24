import React, { useState } from 'react'
import '../../styles/loginSignup.css'
import email_icon from '../../assets/email.png'
import password_icon from '../../assets/password.png'
import InputWithIcon from '../Reusables/InputWithIcon.js'

function Login() {
  return (
    <div className='auth-container'>
      <div className='container'>
        <form className='form'>
          <div className='row'>
            <h4>Log In</h4>

            <InputWithIcon
              placeholder='Email Address'
              imagePath={email_icon}
              name='email'
              type='email'
            />
            <InputWithIcon
              placeholder='Password'
              imagePath={password_icon}
              name='password'
              type='password'
            />
          </div>
          <div className='row'>
            You don't have an account? <a href='/Login'>Register</a>
          </div>
          <div className='row'>
            <div className='col-button'>
              <button style={{ backgroundColor: 'white' }}>Submit</button>
            </div>
          </div>
        </form>
      </div>
    </div>
  )
}

export default Login
