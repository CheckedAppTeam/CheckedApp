import { useMap } from 'react-leaflet'
import { useEffect } from 'react'

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
  export default FlyToMarker