import { useState, useEffect } from 'react'
import { useNavigate, useLocation } from 'react-router-dom'

export function NotFound() {
  const navigate = useNavigate()
  const location = useLocation()
  const [notFound, setNotFound] = useState(false)

  useEffect(() => {
    if (location.pathname !== "/") {
      setNotFound(true);
      const timeout = setTimeout(() => {
        navigate('/')
      }, 2000);
      return () => clearTimeout(timeout);
    }
  }, [navigate, location.pathname])

  return (
    <>
      {notFound && <h1>Not Found</h1>}
    </>
  )
}