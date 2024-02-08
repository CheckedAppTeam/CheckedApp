import Map from '../Components/Map/Map';
import axios from 'axios';
import React, { useState, useEffect } from 'react';
import { useAuth } from '../Contexts/AuthContext.js';
import ItemListModal from '../Components/ItemListModal';

export function Home() {
  const { token } = useAuth()
  const [openModal, setOpenModal] = useState(false);
  const [currentId, setCurrentId] = useState(null);
  const [currentListName, setCurrentListName] = useState(null);
  //mapa
  const handleMarkerClick = (itemListId, listName) => {
    setCurrentId(itemListId);
    setCurrentListName(listName);

    setOpenModal(true);
  };

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
