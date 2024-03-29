import { MapContainer, TileLayer, Marker, Popup } from 'react-leaflet'
import L from 'leaflet'
import FlyToMarker from './FlyToMarker.js'
import PlaceSeeker, { getCoordinates } from './PlaceSeeker.js'
import React, { useState, useEffect, useRef } from 'react'
import ReactDOMServer from 'react-dom/server'
import { mapEndpoints } from '../../endpoints.js'
import { FaMapMarker } from 'react-icons/fa'
import MarkerClusterGroup from 'react-leaflet-cluster'
import '../../styles/map.css'

function Map({ handleMarkerClick }) {
  const [parentCoordinates, setParentCoordinates] = useState(null)
  const [formattedCoords, setFormattedCoords] = useState([])

  useEffect(() => {
    const fetchDataAndFormatCoords = async () => {
      try {
        const response = await fetch(mapEndpoints.getAllDestinations, {
          headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json',
          },
        })

        if (!response.ok) {
          console.error(
            'Failed to fetch destinations. Status:',
            response.status
          )
          return <div>Fail</div>
        }

        const data = await response.json()

        const formattedCoordsPromises = data.map(async (destination) => {
          try {
            const coordinates = await getCoordinates(
              destination.itemListDestination
            )
            return {
              key: destination.itemListId,
              position: [coordinates.latitude, coordinates.longitude],
              icon: CustomMarkerIcon,
              name: destination.itemListName,
            }
          } catch (error) {
            console.error(
              'Error fetching coordinates for destination:',
              destination,
              error
            )
            return null
          }
        })
        const resolvedFormattedCoords = await Promise.all(
          formattedCoordsPromises
        )
        setFormattedCoords(resolvedFormattedCoords)
      } catch (error) {
        console.error('Error fetching destinations:', error)
      }
    }
    fetchDataAndFormatCoords()
  }, [])

  const mapRef = useRef(null)

  const CustomMarkerIcon = L.divIcon({
    className: 'custom-marker-icon',
    html: ReactDOMServer.renderToString(<FaMapMarker />),
  })

  const handleCoordinatesChange = (coordinates) => {
    setParentCoordinates(coordinates)
  }

  return (
    <>
      <div className='country-list'>
        <h1>Where do You wanna go?</h1>
      </div>
      <PlaceSeeker onCoordinatesChange={handleCoordinatesChange} />
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

        <MarkerClusterGroup
          chunkedLoading
          // iconCreateFunction={createClusterIcon}
        >
          {formattedCoords.map((formattedCoord) => {
            if (formattedCoord) {
              return (
                <Marker
                  key={formattedCoord.key}
                  position={formattedCoord.position}
                  icon={formattedCoord.icon}
                  eventHandlers={{
                    click: () =>
                      handleMarkerClick(
                        formattedCoord.key,
                        formattedCoord.name
                      ),
                  }}
                >
                  <Popup>
                    <FaMapMarker />
                    <span
                      onClick={() =>
                        handleMarkerClick(
                          formattedCoord.key,
                          formattedCoord.name
                        )
                      }
                      style={{ cursor: 'pointer', textDecoration: 'underline' }}
                    >
                      {formattedCoord.name}
                    </span>
                  </Popup>
                </Marker>
              )
            } else {
              return null
            }
          })}
        </MarkerClusterGroup>

        {parentCoordinates && (
          <FlyToMarker
            position={[parentCoordinates.latitude, parentCoordinates.longitude]}
            zoomLevel={10}
          />
        )}
      </MapContainer>
    </>
  )
}

export default Map
