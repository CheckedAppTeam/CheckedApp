import React, { useRef } from 'react'
import CheckedFullLogo from '../../assets/CheckedFullLogo.png'
import { FaBars, FaTimes } from 'react-icons/fa'
import { useAuth } from '../../Contexts/AuthContext.js'
import { useNavigate } from 'react-router-dom'
import '../../styles/navbar.css'
import { Link } from 'react-router-dom'
import { jwtDecode } from 'jwt-decode';

function Navbar() {
  const navRef = useRef()
  const { token, removeTokens } = useAuth()
  const navigate = useNavigate()

  let isAdmin = false;
  if (token) {
    try {
      const decodedToken = jwtDecode(token);
      const roles = decodedToken["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
      isAdmin = roles === 'Admin';
    } catch (error) {
      console.error('Problem with token decoding:', error);
    }
  }

  const showNavBar = () => {
    if (navRef.current) {
      navRef.current.classList.toggle('responsive_nav')
    }
  }
  const handleLogout = () => {
    removeTokens()
    showNavBar()
    navigate('/')
  }
  const handleLogoClick = ()=>{
    navigate('/')
  }
  return (
    <header>
      <img className='nav-Logo' onClick={handleLogoClick} src={CheckedFullLogo} alt='Logo' />
      <nav ref={navRef}>
        <Link onClick={showNavBar} to='/'>
          Home
        </Link>
        {token && (
        <Link onClick={showNavBar} to='/ItemLists'>
          Item Lists
        </Link>
        )}
        {isAdmin && (
          <Link onClick={showNavBar} to='/Items'>
            Items
          </Link>
        )}
        {!token && (
          <Link onClick={showNavBar} to='/Register'>
            Register
          </Link>
        )}
        {!token && (
          <Link onClick={showNavBar} to='/Login'>
            Login
          </Link>
        )}
        {token && (
          <Link onClick={showNavBar} to='/user-home'>
            User
          </Link>
        )}
        {token && <a onClick={handleLogout}>Log out </a>}
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
