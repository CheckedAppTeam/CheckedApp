import Map from '../Components/Map/Map'
import React, { useState } from 'react'
import ItemListModal from '../Components/ItemListModal'

export function Home() {
  const [openModal, setOpenModal] = useState(false)
  const [currentId, setCurrentId] = useState(null)
  const [currentListName, setCurrentListName] = useState(null)
  //mapa
  const handleMarkerClick = (itemListId, listName) => {
    setCurrentId(itemListId)
    setCurrentListName(listName)
    setOpenModal(true)
  }

  return (
    <div className='map-container'>
      <Map handleMarkerClick={handleMarkerClick} />
      {openModal && (
        <ItemListModal
          closeModal={() => setOpenModal(false)}
          itemListName={currentListName}
          itemListId={currentId}
        />
      )}
    </div>
  )
}
