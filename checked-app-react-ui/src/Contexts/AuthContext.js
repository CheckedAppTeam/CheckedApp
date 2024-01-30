import React, { createContext, useState, useContext, useEffect } from 'react'
import axios from 'axios'
import { userEndpoints } from '../endpoints'
import { useNavigate } from 'react-router-dom';

const AuthContext = createContext(null)

export const useAuth = () => {
  return useContext(AuthContext)
}

export const AuthProvider = ({ children }) => {
  const [token, setToken] = useState(localStorage.getItem('token'))
  const [refreshToken, setRefreshToken] = useState(localStorage.getItem('refreshToken'));

  const updateTokens = (newToken, newRefreshToken) => {
    localStorage.setItem('token', newToken);
    localStorage.setItem('refreshToken', newRefreshToken);
    setToken(newToken);
    setRefreshToken(newRefreshToken);
    axios.defaults.headers.common['Authorization'] = `Bearer ${newToken}`;
  };

  const removeTokens = () => {
    localStorage.removeItem('token');
    localStorage.removeItem('refreshToken');
    setToken(null);
    setRefreshToken(null);
    delete axios.defaults.headers.common['Authorization'];
  };
  const navigate = useNavigate();

  const handleLogout = () => {
    removeTokens();
    navigate('/login');
  };
  
  
  useEffect(() => {
    const interceptor = axios.interceptors.response.use(
      response => response, 
      async error => {
        const originalRequest = error.config;
        if (error.response.status === 401 && !originalRequest._retry) { 
          originalRequest._retry = true;
          try {
            const response = await axios.post(userEndpoints.refreshToken, { refreshToken });
            const { token: newToken, refreshToken: newRefreshToken } = response.data;
            
            updateTokens(newToken, newRefreshToken);

            axios.defaults.headers.common['Authorization'] = `Bearer ${newToken}`;
            originalRequest.headers['Authorization'] = `Bearer ${newToken}`;
            return axios(originalRequest);
          } catch (refreshError) {
            removeTokens();
            handleLogout();
            return Promise.reject(refreshError);
          }
        }
        return Promise.reject(error);
      }
    );

    return () => {
      axios.interceptors.response.eject(interceptor);
    };
  }, [refreshToken, updateTokens, removeTokens]);

  return (
    <AuthContext.Provider value={{ token, updateTokens, removeTokens }}>
      {children}
    </AuthContext.Provider>
  )
}
