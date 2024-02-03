import React, { useRef } from 'react'
import CheckedFullLogo from '../../assets/CheckedFullLogo.png'
import { FaBars, FaTimes } from 'react-icons/fa'
import { useAuth } from '../../Contexts/AuthContext.js'
import { Link, useNavigate } from 'react-router-dom'
import '../../styles/main.css'

function Navbar() {
  const navRef = useRef()
  const { token, removeToken } = useAuth()
  const navigate = useNavigate()

  const showNavBar = () => {
    if (navRef.current) {
      navRef.current.classList.toggle('responsive_nav')
    }
  }
  const handleLogout = () => {
    removeToken()
    navigate('/', Link)
  }
  return (
    <header>
      <img className='nav-Logo' src={CheckedFullLogo} alt='Logo' />
      <nav ref={navRef}>
        <Link to='/' onClick={showNavBar}>Home</Link>
        <Link to='/ItemLists' onClick={showNavBar}>Item Lists</Link>
        <Link to='/Items' onClick={showNavBar}>Items</Link>
        {!token && <Link to='/Register' onClick={showNavBar}>Register</Link>}
        {!token && <Link to='/Login' onClick={showNavBar}>Login</Link>}
        {token && <Link to='/user-home' onClick={showNavBar}>User</Link>}
        {token && <span onClick={handleLogout}>Log out</span>}
        <button className='nav-btn nav-close-btn' onClick={showNavBar}>
          <FaTimes />
        </button>
      </nav>
      <button className='nav-btn' onClick={showNavBar}>
        <FaBars />
      </button>
    </header>
  );
}

export default Navbar
