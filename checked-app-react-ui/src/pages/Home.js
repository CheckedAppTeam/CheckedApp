import Map from '../Components/Map/Map'
import React, { useState } from 'react'
import SimpleItemListModal from '../Components/SimpleItemListModal';

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
        <SimpleItemListModal
          closeModal={() => setOpenModal(false)}
          itemListName={currentListName}
          itemListId={currentId}
        />
      )}
    </div>
  )
}
