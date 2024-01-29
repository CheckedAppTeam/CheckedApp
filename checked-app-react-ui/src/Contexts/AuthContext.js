import React, { createContext, useState, useContext, useEffect } from 'react'
import axios from 'axios'

const AuthContext = createContext(null)

export const useAuth = () => {
  return useContext(AuthContext)
}

export const AuthProvider = ({ children }) => {
  const [token, setToken] = useState(localStorage.getItem('token'))

  const updateToken = (newToken) => {
    localStorage.setItem('token', newToken)
    setToken(newToken)
    axios.defaults.headers.common['Authorization'] = `Bearer ${newToken}`
  }

  const removeToken = () => {
    localStorage.removeItem('token')
    setToken(null)
    delete axios.defaults.headers.common['Authorization']
  }
  //refresh token
  
  useEffect(() => {
    const currentToken = localStorage.getItem('token')
    if (currentToken) {
      axios.defaults.headers.common['Authorization'] = `Bearer ${currentToken}`
    } else {
      delete axios.defaults.headers.common['Authorization']
    }
  }, [])

  return (
    <AuthContext.Provider value={{ token, updateToken, removeToken }}>
      {children}
    </AuthContext.Provider>
  )
}
