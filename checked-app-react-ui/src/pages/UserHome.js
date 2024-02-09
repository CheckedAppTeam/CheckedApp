import React, { useState, useEffect } from 'react'
import axios from 'axios'
import { useAuth } from '../Contexts/AuthContext.js'
import '../styles/userProfile.css'
import { jwtDecode } from 'jwt-decode'
import { userEndpoints } from '../endpoints'
import { Link } from 'react-router-dom'

export function UserHome() {
  const { token } = useAuth()
  const [user, setUser] = useState(null)
  const [packingLists, setPackingLists] = useState([])
  const [isEditing, setIsEditing] = useState(false)

  useEffect(() => {
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
          console.log(response)
          setUser({
            userId: userId,
            firstName: response.data.userName,
            lastName: response.data.userSurname,
            age: response.data.userAge,
            gender: response.data.userSex,
            email: userEmail,
          })
          setPackingLists(response.data.ownItemList)
        })
        .catch((error) => {
          console.error('Error while getting user', error)
        })
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

  if (!user) {
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
              <label for='firstName'>Name</label>
              <input
                type='text'
                name='firstName'
                value={user.firstName}
                onChange={handleInputChange}
              />
              <label for='lastName'>Surname</label>

              <input
                type='text'
                name='lastName'
                value={user.lastName}
                onChange={handleInputChange}
              />
              <label for='age'>Age</label>

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
              <Link to='/ItemLists' className='linkButton'>
                <button key={list.itemListId}>{list.listName}</button>
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
