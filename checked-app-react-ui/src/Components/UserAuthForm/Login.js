import React, { useState } from 'react'
import axios from 'axios'
import { useNavigate } from 'react-router-dom'
import { useAuth } from '../../Contexts/AuthContext.js'
import '../../styles/loginSignup.css'
import email_icon from '../../assets/email.png'
import password_icon from '../../assets/password.png'
import InputWithIcon from '../Reusables/InputWithIcon.js'
import { userEndpoints } from '../../endpoints'
import { Link } from 'react-router-dom'

function Login() {
  const navigate = useNavigate()
  const { updateTokens } = useAuth()
  const [email, setEmail] = useState('')
  const [password, setPassword] = useState('')

  const handleSubmit = async (event) => {
    event.preventDefault()

    const loginPayload = {
      Email: email,
      Password: password,
    }

    try {
      const response = await axios.post(userEndpoints.logUser, loginPayload)
      const { token, refreshToken } = response.data

      if (token && refreshToken) {
        updateTokens(token, refreshToken)
        navigate('/user-home')
      }
    } catch (err) {
      console.error(err)
    }
  }

  return (
    <div className='auth-container'>
      <div className='container'>
        <form className='form' onSubmit={handleSubmit}>
          <div className='row'>
            <h4>Log In</h4>

            <InputWithIcon
              placeholder='Email Address'
              imagePath={email_icon}
              name='email'
              type='email'
              value={email}
              onChange={(event) => setEmail(event.target.value)}
            />
            <InputWithIcon
              placeholder='Password'
              imagePath={password_icon}
              name='password'
              type='password'
              value={password}
              onChange={(event) => setPassword(event.target.value)}
            />
          </div>
          <div className='row'>
            You don't have an account? <Link to='/Register'>Register</Link>
          </div>
          <div className='row'>
            <div className='col-button'>
              <button className='submit-button' type='submit'>
                Submit
              </button>
            </div>
          </div>
        </form>
      </div>
    </div>
  )
}

export default Login
