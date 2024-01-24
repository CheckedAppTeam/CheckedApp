import React, { useRef } from 'react'
import CheckedFullLogo from '../../assets/CheckedFullLogo.png'
import { FaBars, FaTimes } from 'react-icons/fa'
import { useAuth } from '../../Contexts/AuthContext.js'
import { useNavigate } from 'react-router-dom'
import '../../styles/main.css'

function Navbar() {
  const navRef = useRef()
  const { token, removeToken } = useAuth()
  const navigate = useNavigate();

  const showNavBar = () => {
    if (navRef.current) {
      navRef.current.classList.toggle('responsive_nav')
    }
  }
  const handleLogout = () => {
    removeToken()
    navigate('/')
  };
  return (
    <header>
      <img className='nav-Logo' src={CheckedFullLogo} alt='Logo' />
      <nav ref={navRef}>
        <a href='/'>Home</a>
        <a href='/ItemLists'>Item Lists</a>
        <a href='/hehe'>Items</a>
        {!token && <a href='/Register'>Register</a>}
        {!token && <a href='/Login'>Login</a>}
        {token && <a href='/UserHome'>User</a>}
        {token && <button onClick={handleLogout}>Unlog</button>}
        <button className='nav-btn nav-close-btn' onClick={showNavBar}>
          <FaTimes />
        </button>
      </nav>
      <button className='nav-btn' onClick={showNavBar}>
        <FaBars />
      </button>
    </header>
  )
}

export default Navbar
