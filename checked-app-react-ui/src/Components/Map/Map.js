import React from 'react'
import { MapContainer, TileLayer, Marker } from 'react-leaflet'
import 'leaflet/dist/leaflet.css'
import '../../styles/map.css'

let markers = [
  {
    geocode: [48.86, 2.3522],
    popUp: 'Hello, I am pop up 1',
  },
  {
    geocode: [48.85, 2.3522],
    popUp: 'Hello, I am pop up 2',
  },
  {
    geocode: [48.855, 2.34],
    popUp: 'Hello, I am pop up 3',
  },
]

function Map() {
  return (
    <MapContainer
      className='map-container'
      center={[52.2297, 21.0122]}
      zoom={12}
    >
      <TileLayer
        attribution='&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
        url='https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png'
      />
      {markers.map((marker) => {
        ;<Marker position={marker.geocode}></Marker>
      })}
    </MapContainer>
  )
}

export default Map
