import React, { useState } from 'react';

async function getCoordinates(address) {
  const apiKey = process.env.REACT_APP_GOOGLE_MAPS_API_KEY
  const apiUrl = `https://maps.googleapis.com/maps/api/geocode/json?address=${encodeURIComponent(
    address
  )}&key=${apiKey}`;

  try {
    const response = await fetch(apiUrl);
    const data = await response.json();

    if (data.status === 'OK' && data.results.length > 0) {
      const location = data.results[0].geometry.location;
      const coordinates = {
        latitude: location.lat,
        longitude: location.lng,
      };
      return coordinates;
    } else {
      console.error('Error fetching coordinates:', data.status);
      return null;
    }
  } catch (error) {
    console.error('Error fetching coordinates:', error);
    return null;
  }
}

const GeocodingExample = () => {
  const [address, setAddress] = useState('');
  const [coordinates, setCoordinates] = useState(null);

  const handleAddressChange = (event) => {
    setAddress(event.target.value);
  };

  const handleGetCoordinates = async () => {
    if (address.trim() !== '') {
      const coords = await getCoordinates(address);
      setCoordinates(coords);
    }
  };

  return (
    <div>
      <h2>Cant find what you want? Just type it in!</h2>
      
        <input placeholder="Type address..." type="text" value={address} onChange={handleAddressChange} />
      
      <button onClick={handleGetCoordinates}>Add Place</button>

      {coordinates && (
        <div>
          <h3>Coordinates:</h3>
          <p>Latitude: {coordinates.latitude}</p>
          <p>Longitude: {coordinates.longitude}</p>
        </div>
      )}
    </div>
  );
};

export default GeocodingExample;