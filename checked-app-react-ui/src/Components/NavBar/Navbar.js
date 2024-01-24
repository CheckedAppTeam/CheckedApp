import React, { useRef } from 'react'
import CheckedFullLogo from '../../assets/CheckedFullLogo.png'
import { FaBars, FaTimes } from 'react-icons/fa'
import '../../styles/main.css'

function Navbar() {
  const navRef = useRef()

  const showNavBar = () => {
    if (navRef.current) {
      navRef.current.classList.toggle('responsive_nav')
    }
  }
  return (
    <header>
      <img className='nav-Logo' src={CheckedFullLogo} alt='Logo' />
      <nav ref={navRef}>
        <a href='/'>Home</a>
        <a href='/ItemLists'>Item Lists</a>
        <a href='/Register'>Register</a>
        <a href='/Login'>Login</a>
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
