import { useState } from 'react';
import axios from 'axios';

export const useAuth = () => {
  const [token, setToken] = useState(localStorage.getItem('token'));

  const updateToken = (newToken) => {
    localStorage.setItem('token', newToken);
    setToken(newToken);
    axios.defaults.headers.common['Authorization'] = `Bearer ${newToken}`;
  };

  const removeToken = () => {
    localStorage.removeItem('token');
    setToken(null);
    delete axios.defaults.headers.common['Authorization'];
  };

  return {
    token,
    updateToken,
    removeToken
  };
};