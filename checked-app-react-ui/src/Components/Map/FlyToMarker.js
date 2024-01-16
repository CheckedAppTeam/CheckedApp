import React, { useEffect } from 'react'
import { useMap } from 'react-leaflet'

const FlyToMarker = ({ position, zoomLevel }) => {
  const map = useMap()

  useEffect(() => {
    if (position) {
      const zoom = zoomLevel ?? map.getZoom()
      map.flyTo(position, zoom, {
        duration: 10,
      })
    }
  }, [map, position, zoomLevel])

  return null
}

export default FlyToMarker
