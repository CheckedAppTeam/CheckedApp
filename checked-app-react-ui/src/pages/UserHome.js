import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { useAuth } from '../Components//UserAuthForm/useAuth.js';
import '../styles/main.css'; 
import { jwtDecode } from 'jwt-decode';
import { userEndpoints } from '../endpoints';

export function UserHome() {
  const { token } = useAuth();
  const [user, setUser] = useState(null);
  const [packingLists, setPackingLists] = useState([]);

  useEffect(() => {
    if (token) {
        const decodedToken = jwtDecode(token);
        console.log(decodedToken)
        const userId = decodedToken["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"]
        const userEmail = decodedToken["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"]
        console.log(userEmail)

        axios.get(userEndpoints.getUserData(userId))
        .then(response => {
            console.log(response.data);
          setUser({
            firstName: response.data.userName,
            lastName: response.data.userSurname,
            age: response.data.userAge,
            gender: response.data.userSex,
            email: userEmail
          });
          setPackingLists(response.data.ownItemList);
        })
        .catch(error => {
          console.error("Błąd podczas pobierania danych użytkownika:", error);
        });
    }
  }, [token]);

  const handleInputChange = (event) => {
    const { name, value } = event.target;
    setUser({ ...user, [name]: value });
  };

  const saveChanges = () => {
    // zrobić wysyłanie zmian na serwer
    console.log('Zapisane zmiany:', user);
  };

  if (!user) {
    return <div>Loading...</div>;
  }

  return (
    <>
      <div className='user-profile'>
        <h1>User Profile</h1>
        <div className='user-info'>
          <input type="text" name="firstName" value={user.firstName} onChange={handleInputChange} />
          <input type="text" name="lastName" value={user.lastName} onChange={handleInputChange} />
          <input type="number" name="age" value={user.age} onChange={handleInputChange} />
          <input type="text" name="gender" value={user.gender} onChange={handleInputChange} />
          <input type="email" name="email" value={user.email} readOnly />
          <button onClick={saveChanges}>Save changes</button>
        </div>
        <div className='packing-lists'>
          <h2>Your packing lists</h2>
          {packingLists.length > 0 ? (
            packingLists.map(list => (
              <button key={list.itemListId}>
                {list.listName}
              </button>
            ))
          ) : (
            <p>You have no packing lists...</p>
          )}
        </div>
      </div>
    </>
  );
}