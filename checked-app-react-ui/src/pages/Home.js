import Map from '../Components/Map/Map'
import axios from 'axios'
import React, { useState, useEffect } from 'react'
import { useAuth } from '../Contexts/AuthContext.js'

export function Home() {
  const { token } = useAuth()

  return (
    <div className='map-container'>
      <Map />
    </div>
  )
}
