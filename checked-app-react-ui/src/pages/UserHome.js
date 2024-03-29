import React, { useState, useEffect } from 'react'
import axios from 'axios'
import { useAuth } from '../Contexts/AuthContext.js'
import '../styles/userProfile.css'
import { jwtDecode } from 'jwt-decode'
import { userEndpoints } from '../endpoints'
import { Link } from 'react-router-dom'
import Loader from '../spinners/Loader.js'

export function UserHome() {
  const { token } = useAuth();
  const [user, setUser] = useState(null);
  const [packingLists, setPackingLists] = useState([]);
  const [isEditing, setIsEditing] = useState(false);
  const [isLoading, setIsLoading] = useState(true);

  useEffect(() => {
    const storedToken = localStorage.getItem('token');
    if (storedToken) {
      axios.defaults.headers.common['Authorization'] = `Bearer ${storedToken}`;
    }
    setIsLoading(true);
    if (token) {
      const decodedToken = jwtDecode(token)
      const userId =
        decodedToken[
        'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'
        ]
      const userEmail =
        decodedToken[
        'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress'
        ]
      axios
        .get(userEndpoints.getUserData(userId))
        .then((response) => {
          setUser({
            userId: userId,
            firstName: response.data.userName,
            lastName: response.data.userSurname,
            age: response.data.userAge,
            gender: response.data.userSex,
            email: userEmail,
          })
          setPackingLists(response.data.ownItemList)
          setIsLoading(false)
        })
        .catch((error) => {
          console.error('Error while getting user', error)
          setIsLoading(false)
        })
    } else {
      setIsLoading(false)
    }
  }, [token])

  const handleInputChange = (event) => {
    const { name, value } = event.target
    setUser({ ...user, [name]: value })
  }

  const toggleEdit = () => {
    setIsEditing(!isEditing)
  }

  const saveChanges = () => {
    axios
      .put(userEndpoints.editUser(user.userId), {
        userName: user.firstName,
        userSurname: user.lastName,
        userAge: user.age,
      })
      .then(setIsEditing(false))
      .catch((error) => {
        console.error('Error while sending data', error)
      })
  }

  if (isLoading) {
    return <Loader/>;
  } else if (!user) {
    return (
      <Link to='/Login'>
        <h1>Unauthorized, click to Log In.</h1>
      </Link>
    )
  }
  return (
    <>
      <div className='user-profile'>
        <h1>My Profile</h1>
        <div className='user-info'>
          {isEditing ? (
            <>
              <label htmlFor='firstName'>Name</label>
              <input
                type='text'
                name='firstName'
                value={user.firstName}
                onChange={handleInputChange}
              />
              <label htmlFor='lastName'>Surname</label>

              <input
                type='text'
                name='lastName'
                value={user.lastName}
                onChange={handleInputChange}
              />
              <label htmlFor='age'>Age</label>

              <input
                type='number'
                name='age'
                value={user.age}
                onChange={handleInputChange}
              />
            </>
          ) : (
            <>
              <div>Name: {user.firstName}</div>
              <div>Surname: {user.lastName}</div>
              <div>Age: {user.age}</div>
            </>
          )}
          <div>Gender: {user.gender}</div>
          <div>Email: {user.email}</div>
          {isEditing ? (
            <button onClick={saveChanges}>Save</button>
          ) : (
            <button onClick={toggleEdit}>Edit</button>
          )}
        </div>
        <div className='packing-lists'>
          <h2>Your packing lists</h2>
          {packingLists.length > 0 ? (
            packingLists.map((list) => (
              <Link to='/ItemLists' key={list.itemListId} className='linkButton'>
                <button>{list.listName}</button>
              </Link>
            ))
          ) : (
            <p>You have no packing lists...</p>
          )}
        </div>
      </div>
    </>
  )
}
