import React from 'react';
import { Navigate } from 'react-router-dom';
import {jwtDecode} from 'jwt-decode';
import { useAuth } from '../Contexts/AuthContext';

const PrivateRoute = ({ children }) => {
  const { token } = useAuth();
  let isAdmin = false;

  if (token) {
    try {
      const decodedToken = jwtDecode(token);
      isAdmin = decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] === 'Admin';
    } catch (error) {
      console.error('Error decoding token:', error);
    }
  }
  return isAdmin ? children : <Navigate to="/Login" />;
};

export default PrivateRoute;