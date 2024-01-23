import Map from '../Components/Map/Map'
import axios from "axios";
import React, { useState, useEffect } from "react";
import { useAuth } from '../Components/UserAuthForm/useAuth.js';

export function Home() {
  const { token } = useAuth();
  
  // useEffect(() => {
  //  const token = localStorage.getItem("token");

  //   if (token) {
  //     axios.defaults.headers.common["Authorization"] = `Bearer ${token}`;
  //   }
  // });

  return (
    <div className='map-container'>
      <h1>Home</h1>
      <Map />
    </div>
  )
}
