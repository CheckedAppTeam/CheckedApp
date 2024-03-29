import React, { useState } from 'react'

export async function getCoordinates(address) {
  const apiKey = process.env.REACT_APP_GOOGLE_MAPS_API_KEY
  const apiUrl = `https://maps.googleapis.com/maps/api/geocode/json?address=${encodeURIComponent(
    address
  )}&key=${apiKey}`

  try {
    const response = await fetch(apiUrl)
    const data = await response.json()

    if (data.status === 'OK' && data.results.length > 0) {
      const location = data.results[0].geometry.location
      const coordinates = {
        latitude: location.lat,
        longitude: location.lng,
      }
      return coordinates
    } else {
      console.error('Error fetching coordinates:', data.status)
      return null
    }
  } catch (error) {
    console.error('Error fetching coordinates:', error)
    return null
  }
}

const PlaceSeeker = ({ onCoordinatesChange }) => {
  const [address, setAddress] = useState('')
  const [coordinates, setCoordinates] = useState(null)
  const [placeTitle, setPlaceTitle] = useState('')

  const handleAddressChange = (event) => {
    setAddress(event.target.value)
  }

  const handleGetCoordinates = async () => {
    if (address.trim() !== '') {
      const coords = await getCoordinates(address)
      setCoordinates(coords)
      onCoordinatesChange(coords)
    }
  }
  const handlePlaceTitle = (placeName) => {
    const formattedName = alternateCase(placeName)
    setPlaceTitle(formattedName)
  }

  const handleKeyPress = (event) => {
    if (event.key === 'Enter') {
      handleGetCoordinates()
      window.scrollBy(560, 560)
      handlePlaceTitle(address)
    }
  }

  const alternateCase = (word) => {
    return word
      .split('')
      .map((char, index) => {
        return index === 0 ? char.toUpperCase() : char.toLowerCase()
      })
      .join('')
  }
  return (
    <div>
      <input
        placeholder='Type address...'
        type='text'
        value={address}
        onChange={handleAddressChange}
        onKeyUp={handleKeyPress}
      />

      <button onClick={handleGetCoordinates}>Add Place</button>
      {coordinates && (
        <div>
          <h3>Coordinates:</h3>
          <p>Address: {address.slice(0).toUpperCase()}</p>
          <p>Latitude: {coordinates.latitude}</p>
          <p>Longitude: {coordinates.longitude}</p>
          <h1>{placeTitle}</h1>
        </div>
      )}
    </div>
  )
}

export default PlaceSeeker
