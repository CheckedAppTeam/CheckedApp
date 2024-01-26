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

function Signup() {
  const [UserName, setUserName] = useState('')
  const [UserSurname, setUserSurname] = useState('')
  const [UserEmail, setUserEmail] = useState('')
  const [password, setPassword] = useState('')
  const [UserAge, setUserAge] = useState('')
  const [gender, setGender] = useState('')
  const [terms, setTerms] = useState(false)

  const [passwordStrength, setPasswordStrength] = useState(0)
  const [error, setError] = useState('')
  const navigate = useNavigate()

  const handlePasswordChange = (e) => {
    const { value } = e.target
    setPassword(value)

    const requirements = [
      value.length >= 8,
      /\d/.test(value),
      /[!@#$%^&*(),.?":{}|<>]/.test(value),
    ]

    const strength = requirements.reduce(
      (count, requirement) => count + requirement,
      0
    )
    setPasswordStrength(strength)
  }

  const handleSubmit = async (e) => {
    e.preventDefault()

    if (!isFormValid()) {
      setError('Please fill in all required fields.')
      return error
    }

    let addUserDto = {
      userName: UserName,
      userSurname: UserSurname,
      userEmail: UserEmail,
      password: password,
      userAge: Number(UserAge),
      userSex: gender,
    }

    axios
      .post(userEndpoints.addUser, { addUserDto: addUserDto })
      .then(() => {
        navigate('/login')
      })
      .catch((error) => {
        console.error(error)
        setError('Registration failed. Please try again.')
      })
  }

  const isFormValid = () => {
    return (
      UserName &&
      UserSurname &&
      UserEmail &&
      password &&
      UserAge &&
      gender &&
      terms === true
    )
  }

  return (
    <div className='auth-container'>
      <div className='container'>
        <form onSubmit={handleSubmit}>
          <div className='row'>
            <h4>Account</h4>
            <InputWithIcon
              placeholder='First Name'
              imagePath={person_icon}
              name='UserName'
              value={UserName}
              onChange={(e) => setUserName(e.target.value)}
            />
            <InputWithIcon
              placeholder='Last Name'
              imagePath={person_icon}
              name='UserSurname'
              value={UserSurname}
              onChange={(e) => setUserSurname(e.target.value)}
            />
            <InputWithIcon
              placeholder='Email Address'
              imagePath={email_icon}
              name='UserEmail'
              type='UserEmail'
              value={UserEmail}
              onChange={(e) => setUserEmail(e.target.value)}
            />
            <InputWithIcon
              onChange={handlePasswordChange}
              placeholder='Password'
              imagePath={password_icon}
              name='password'
              type='password'
              value={password}
            />
          </div>
          <div className='row'>
            <h4>Password Strength</h4>
            <progress
              value={passwordStrength}
              max='3'
              className={
                passwordStrength === 0
                  ? 'weak'
                  : passwordStrength === 1
                    ? 'acceptable'
                    : passwordStrength === 2
                      ? 'good'
                      : 'strong'
              }
              style={{
                width: '100%',
                height: '5px',
              }}
            />

            <div
              style={{
                display: 'flex',
                justifyContent: 'space-between',
                fontSize: '12px',
              }}
            >
              <span>Weak</span>
              <span>Acceptable</span>
              <span>Good</span>
              <span>Strong</span>
            </div>
          </div>
          <div className='row'>
            <br></br>
            You already have an account? <a href='/Login'>Login</a>
          </div>
          <br></br>
          <div className='row'>
            <div className='col-half'>
              <h4>Date of Birth</h4>
              <div className='input-group'>
                <div className='col-half'>
                  <input
                    placeholder='Age'
                    type='number'
                    name='age'
                    onChange={(e) => setUserAge(e.target.value)}
                    style={{ width: '150%' }}
                  />
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
                  onChange={() => setGender('Male')}
                />
                <label htmlFor='gender-male'>Male</label>
                <input
                  id='gender-female'
                  type='radio'
                  name='gender'
                  value='female'
                  onChange={() => setGender('Female')}
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
                onChange={() => setTerms(true)}
              />
              <label htmlFor='terms'>
                <a href={terms_pdf} target='_blank' rel='noopener noreferrer'>
                  Terms
                </a>{' '}
                I accept the terms and conditions for signing up to this
                service, and also confirm I have read the privacy policy.
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
