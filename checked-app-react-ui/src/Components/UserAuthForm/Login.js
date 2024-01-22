import React, { useState } from 'react'
import axios from "axios";
import { useNavigate } from "react-router-dom";
import '../../styles/loginSignup.css'
import email_icon from '../../assets/email.png'
import person_icon from '../../assets/person.png'
import password_icon from '../../assets/password.png'
import InputWithIcon from '../Reusables/InputWithIcon.js'

function Login() {
    const navigate = useNavigate();
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
  
    function handleSubmit(event) {
      event.preventDefault();
  
      const loginPayload = {
        Email: email,
        Password: password,
      };
  
      axios
        .post("https://localhost:7161/Auth/Login", loginPayload)
        .then((response) => {
          const token = response.data.Token;
  
          localStorage.setItem("token", token);
  
          if (token) {
            axios.defaults.headers.common["Authorization"] = `Bearer ${token}`;
          }
  
          navigate("/");
        })
        .catch((err) => console.log(err));
    }
  
    function handleUserEmailChange(event) {
      console.log(event)
      setEmail(event.target.value);
    }
  
    function handlePasswordChange(event) {
      console.log(event)
      setPassword(event.target.value);
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
              onChange={handleUserEmailChange}
            />
            <InputWithIcon
              placeholder='Password'
              imagePath={password_icon}
              name='password'
              type='password'
              value={password}
              onChange={handlePasswordChange}
            />
          </div>
          <div className='row'>
            You don't have an account? <a href='/Login'>Register</a>
          </div>
          <div className='row'>
            <div className='col-button'>
              <button style={{ backgroundColor: 'white' }} type="submit">Submit</button>
            </div>
          </div>
        </form>
      </div>
    </div>
  )
}

export default Login
