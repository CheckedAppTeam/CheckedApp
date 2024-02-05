import React, { useState, useEffect, useRef } from 'react'
import { MapContainer, TileLayer, Marker, Popup } from 'react-leaflet'
import L from 'leaflet'
import { FaMapMarker } from 'react-icons/fa'
import { useMap } from 'react-leaflet'
import Select from 'react-select'
import ReactDOMServer from 'react-dom/server'
import '../../styles/map.css'
import PlaceSeeker from './PlaceSeeker.js'
const FlyToMarker = ({ position, zoomLevel }) => {
  const map = useMap()

  useEffect(() => {
    if (position) {
      const zoom = zoomLevel || map.getZoom()
      map.flyTo(position, zoom, {
        duration: 4,
      })
    }
  }, [map, position, zoomLevel])

  return null
}

function Map() {
  const [countries, setCountries] = useState([])
  const [selectedCountry, setSelectedCountry] = useState(null)
  const mapRef = useRef(null)
  const [inputValue, setInputValue] = useState('')

  const CustomMarkerIcon = L.divIcon({
    className: 'custom-marker-icon',
    html: ReactDOMServer.renderToString(<FaMapMarker />),
  })

  useEffect(() => {
    fetch('https://restcountries.com/v2/all')
      .then((response) => response.json())
      .then((data) => {
        const countryOptions = data
          .filter((country) => country.latlng && country.latlng.length === 2)
          .map((country) => ({
            value: country.name,
            label: country.name,
            geocode: country.latlng,
          }))
        setCountries(countryOptions)
      })
      .catch((error) => console.error('Error fetching country data:', error))
  }, [selectedCountry])

  const handleCountrySelect = (selectedOption) => {
    setSelectedCountry({
      geocode: selectedOption.geocode,
      zoom: 6,
    })
  }

  const filterCountries = (input) => {
    return countries.filter((country) =>
      country.label.toLowerCase().includes(input.toLowerCase())
    )
  }

  return (
    <>
      <div className='country-list'>
        <h1>Where do You wanna go?</h1>
        <Select
          className='map-input'
          value={null}
          options={filterCountries(inputValue)}
          onChange={handleCountrySelect}
          onInputChange={(value) => setInputValue(value)}
          placeholder='Type to search...'
          />
          <PlaceSeeker/>
      </div>
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
        {selectedCountry && (
          <FlyToMarker position={selectedCountry.geocode} zoomLevel={5} />
        )}
      </MapContainer>
    </>
  )
}

export default Map
