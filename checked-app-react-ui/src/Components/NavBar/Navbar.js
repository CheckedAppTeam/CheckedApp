import React from 'react'
import './Navbar.css'
import { Link } from 'react-router-dom'
import CheckedFullLogo from '../../assets/CheckedFullLogo.png'

const Navbar = () => {
  return (
    <div className='navbar'>
      <img src={CheckedFullLogo} alt='Logo' className='logo' />
      <ul>
        <li>
          <Link to='/'>Home</Link>
        </li>
        <li>
          <Link to='/ItemLists'>ItemLists</Link>
        </li>
      </ul>
    </div>
  )
}

export default Navbar
