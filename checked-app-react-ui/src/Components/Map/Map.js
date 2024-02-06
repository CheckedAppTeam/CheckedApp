import React, { useState, useEffect, useRef } from 'react'
import { MapContainer, TileLayer, Marker, Popup } from 'react-leaflet'
import L from 'leaflet'
import { FaMapMarker } from 'react-icons/fa'
import Select from 'react-select'
import ReactDOMServer from 'react-dom/server'
import '../../styles/map.css'
import PlaceSeeker from './PlaceSeeker.js'
import FlyToMarker from './FlyToMarker.js'



function Map() {
  const [parentCoordinates, setParentCoordinates] = useState(null);

  const mapRef = useRef(null)

  const CustomMarkerIcon = L.divIcon({
    className: 'custom-marker-icon',
    html: ReactDOMServer.renderToString(<FaMapMarker />),
  })

  const handleCoordinatesChange = (coordinates) => {
    setParentCoordinates(coordinates);
  }
  
  return (
    <>
      <div className='country-list'>
        <h1>Where do You wanna go?</h1>
        
        
      </div>
      <PlaceSeeker onCoordinatesChange={handleCoordinatesChange}  />
      <br></br>
      <MapContainer
        className='map-container'
        center={[52.0, 18.0]}
        zoom={5}
        whenCreated={(map) => (mapRef.current = map)}
      >
        <TileLayer
          attribution='&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
          url='https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png'
        />

        {/* {countries.map((country, index) => (
          <Marker
            key={index}
            position={country.geocode}
            icon={CustomMarkerIcon}
          >
            <Popup>
              <FaMapMarker />
              <a href={country.label}>{country.label}</a>
            </Popup>
          </Marker>
        ))} */}
        
        {parentCoordinates && (
          <FlyToMarker position={[parentCoordinates.latitude,parentCoordinates.longitude]} zoomLevel={6} />
        )}
      </MapContainer>
    </>
  )
}

export default Map
