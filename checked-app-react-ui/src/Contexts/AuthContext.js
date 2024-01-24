import React, { createContext, useState, useContext } from 'react';
import axios from 'axios';

const AuthContext = createContext(null);

export const useAuth = () => {
    return useContext(AuthContext);
};

export const AuthProvider = ({ children }) => {
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

    return (
        <AuthContext.Provider value={{ token, updateToken, removeToken }}>
            {children}
        </AuthContext.Provider>
    );
};