import React, { useState } from 'react'
import '../../styles/loginSignup.css'
import email_icon from '../../assets/email.png'
import person_icon from '../../assets/person.png'
import password_icon from '../../assets/password.png'
import InputWithIcon from '../Reusables/InputWithIcon.js'

function Signup() {
  const [formData, setFormData] = useState({
    firstName: '',
    lastName: '',
    email: '',
    password: '',
    day: '',
    month: '',
    year: '',
    gender: '',
    terms: false,
  })

  const handleChange = (e) => {
    const { name, value, type, checked } = e.target
    setFormData((prevData) => ({}))
  }

  const calculateAge = () => {
    const { day, month, year } = formData
    const birthDate = new Date(`${year}-${month}-${day}`)
    const currentDate = new Date()
    const age = Math.floor(
      (currentDate - birthDate) / (365.25 * 24 * 60 * 60 * 1000)
    )
    return age
  }

  const handleSubmit = (e) => {
    e.preventDefault()
    const age = calculateAge()
    const userData = {
      ...formData,
      age,
    }
    const jsonString = JSON.stringify(userData, null, 2)
    console.log(jsonString)
  }

  const isFormValid = () => {
    // Check if all required fields are filled
    return Object.values(formData).every((value) => Boolean(value))
  }

  return (
    <div className='auth-container'>
      <div className='container'>
        <form onSubmit={handleSubmit} onChange={handleChange}>
          <div className='row'>
            <h4>Account</h4>
            <InputWithIcon
              placeholder='First Name'
              imagePath={person_icon}
              name='firstName'
            />
            <InputWithIcon
              placeholder='Last Name'
              imagePath={person_icon}
              name='lastName'
            />
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
            You already have an account? <a href='/Login'>Login</a>
          </div>
          <br></br>
          <div className='row'>
            <div className='col-half'>
              <h4>Date of Birth</h4>
              <div className='input-group'>
                <div className='col-third'>
                  <input placeholder='DD' name='day' />
                </div>
                <div className='col-third'>
                  <input placeholder='MM' name='month' />
                </div>
                <div className='col-third'>
                  <input placeholder='YYYY' name='year' />
                </div>
              </div>
            </div>
            <div className='col-half'>
              <h4>Gender</h4>
              <div className='input-group'>
                <input
                  id='gender-male'
                  type='radio'
                  name='gender'
                  value='male'
                />
                <label htmlFor='gender-male'>Male</label>
                <input
                  id='gender-female'
                  type='radio'
                  name='gender'
                  value='female'
                />
                <label htmlFor='gender-female'>Female</label>
              </div>
            </div>
          </div>
          <div className='row'></div>
          <div className='row'>
            <h4>Terms and Conditions</h4>
            <div className='input-group'>
              <input id='terms' type='checkbox' name='terms' />
              <label htmlFor='terms'>
                I accept the terms and conditions for signing up to this
                service, and hereby confirm I have read the privacy policy.
              </label>
            </div>
          </div>
          <div className='row'>
            <div className='col-button'>
              <button
                type='submit'
                style={{ backgroundColor: isFormValid() ? 'green' : 'white' }}
              >
                Submit
              </button>
            </div>
          </div>
        </form>
      </div>
    </div>
  )
}

export default Signup