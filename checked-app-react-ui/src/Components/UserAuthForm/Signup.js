import React, { useState } from 'react'
import axios from 'axios'
import { useNavigate } from 'react-router-dom'
import '../../styles/loginSignup.css'
import email_icon from '../../assets/email.png'
import person_icon from '../../assets/person.png'
import password_icon from '../../assets/password.png'
import InputWithIcon from '../Reusables/InputWithIcon.js'
import terms_pdf from '../../assets/TermsOfService.pdf'
import { userEndpoints } from '../../endpoints'
import { Link } from 'react-router-dom'

function Signup() {
  const [form, setForm] = useState({
    UserName: '',
    UserSurname: '',
    UserEmail: '',
    password: '',
    UserAge: '',
    gender: '',
    terms: false,
    passwordStrength: 0,
    error: '',
  })

  const navigate = useNavigate()

  const handlePasswordChange = (e) => {
    const value = e.target.value
    setForm((prevForm) => ({
      ...prevForm,
      password: value,
      passwordStrength: calculatePasswordStrength(value),
    }))
  }

  const calculatePasswordStrength = (value) => {
    const requirements = [
      value.length >= 8,
      /\d/.test(value),
      /[!@#$%^&*(),.?":{}|<>]/.test(value),
    ]

    return requirements.reduce((count, requirement) => count + requirement, 0)
  }

  const handleForm = (e) => {
    const name = e.target.name
    const value =
      e.target.type === 'checkbox' ? e.target.checked : e.target.value

    setForm((prevForm) => ({
      ...prevForm,
      [name]: value,
    }))
  }

  const handleSubmit = async (e) => {
    e.preventDefault()

    if (!isFormValid()) {
      setForm((prevForm) => ({
        ...prevForm,
        error: 'Please fill in all required fields.',
      }))
      return
    }

    const addUserDto = {
      userName: form.UserName,
      userSurname: form.UserSurname,
      userEmail: form.UserEmail,
      password: form.password,
      userAge: Number(form.UserAge),
      userSex: form.gender,
    }

    try {
      await axios.post(userEndpoints.addUser, { addUserDto })
      navigate('/login')
    } catch (error) {
      console.error(error)
      setForm((prevForm) => ({
        ...prevForm,
        error: 'Registration failed. Please try again.',
      }))
    }
  }

  const isFormValid = () => {
    return (
      form.UserName &&
      form.UserSurname &&
      form.UserEmail &&
      form.password &&
      form.UserAge &&
      form.gender &&
      form.terms === true
    )
  }

  return (
    <div className='auth-container'>
      <div className='container'>
        <form onSubmit={handleSubmit}>
          <div className='row'>
            <InputWithIcon
              placeholder='First Name'
              imagePath={person_icon}
              name='UserName'
              value={form.UserName}
              onChange={handleForm}
            />
            <InputWithIcon
              placeholder='Last Name'
              imagePath={person_icon}
              name='UserSurname'
              value={form.UserSurname}
              onChange={handleForm}
            />
            <InputWithIcon
              placeholder='Email Address'
              imagePath={email_icon}
              name='UserEmail'
              type='UserEmail'
              value={form.UserEmail}
              onChange={handleForm}
            />
            <InputWithIcon
              onChange={handlePasswordChange}
              placeholder='Password'
              imagePath={password_icon}
              name='password'
              type='password'
              value={form.password}
            />
          </div>
          <div className='row'>
            <h4>Password Strength</h4>
            <progress
              className='password-strength-bar'
              value={form.passwordStrength}
              max='3'
            />
            <div className='password-strength-label'>
              <span>Weak</span>
              <span>Acceptable</span>
              <span>Good</span>
              <span>Strong</span>
            </div>
          </div>
          <div className='row'>
            <br></br>
            You already have an account? <Link to='/Login'>Login</Link>
          </div>
          <br></br>
          <div className='row'>
            <div className='col-half'>
              <h4>Age</h4>

              <div className='col-half'>
                <input
                  className='age-input'
                  type='number'
                  name='UserAge'
                  onChange={handleForm}
                  value={form.UserAge}
                />
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
                  onChange={handleForm}
                  checked={form.gender === 'male'}
                />
                <label htmlFor='gender-male'>Male</label>
                <input
                  id='gender-female'
                  type='radio'
                  name='gender'
                  value='female'
                  onChange={handleForm}
                  checked={form.gender === 'female'}
                />
                <label htmlFor='gender-female'>Female</label>
              </div>
            </div>
          </div>
          <div className='row'></div>
          <div className='row'>
            <h4>Terms and Conditions</h4>
            <div className='input-group'>
              <input
                id='terms'
                type='checkbox'
                name='terms'
                onChange={handleForm}
                checked={form.terms}
              />
              <label htmlFor='terms'>
                <Link to={terms_pdf} target='_blank' rel='noopener noreferrer'>
                  Terms
                </Link>{' '}
                I accept the terms and conditions for signing up to this
                service, and also confirm I have read the privacy policy.
              </label>
            </div>
          </div>
          <div className='row'>
            <div className='col-button'>
              <button type='submit'>Submit</button>
            </div>
          </div>
        </form>
      </div>
    </div>
  )
}

export default Signup
