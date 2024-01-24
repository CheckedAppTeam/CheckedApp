import React, { useState, useEffect, useRef } from 'react'
import { MapContainer, TileLayer, Marker, Popup } from 'react-leaflet'
import L from 'leaflet'
import { FaMapMarker } from 'react-icons/fa'
import { useMap } from 'react-leaflet'
import Select from 'react-select'
import ReactDOMServer from 'react-dom/server'
import '../../styles/map.css'

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

  const customStyles = {
    option: (provided, state) => ({
      ...provided,
      borderBottom: '1px solid #ccc',
      color: state.isSelected ? 'white' : 'black',
      background: state.isSelected ? '#0088cc' : 'white',
      zIndex: 9900,
    }),
    menu: (provided) => ({
      ...provided,
      zIndex: 9900,
    }),
  }

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
  }, [])

  useEffect(() => {
    if (mapRef.current && selectedCountry) {
      const { geocode, zoom } = selectedCountry
      mapRef.current.flyTo(geocode, zoom)
    }
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
    <div>
      <div className='country-list'>
        <h2>Select Country</h2>
        <Select
          className='map-input'
          value={null}
          options={filterCountries(inputValue)}
          onChange={handleCountrySelect}
          onInputChange={(value) => setInputValue(value)}
          placeholder='Type to search...'
          styles={customStyles}
        />
      </div>
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
        {countries.map((country, index) => (
          <Marker
            key={index}
            position={country.geocode}
            icon={CustomMarkerIcon}
          >
            <Popup>
              <FaMapMarker />
              {country.label}
            </Popup>
          </Marker>
        ))}
        {selectedCountry && (
          <FlyToMarker position={selectedCountry.geocode} zoomLevel={5} />
        )}
      </MapContainer>
    </div>
  )
}

export default Map
