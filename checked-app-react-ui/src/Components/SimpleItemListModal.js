import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { itemListEndpoints, userItemEndpoints } from '../endpoints';
import Loader from '../spinners/Loader';
import '../styles/modalWithMap.css'
import Button from '@mui/material/Button';
import { useAuth } from '../Contexts/AuthContext.js'
import { jwtDecode } from 'jwt-decode'


function SimpleItemListModal({ closeModal, itemListName, itemListId }) {
  const [allItemsByItemListId, setAllItemsByItemListId] = useState([]);
  const [loading, setLoading] = useState(false);
  const { token } = useAuth()

  const showAllItemsByItemListId = async () => {
    try {
      const response = await axios.get(
        userItemEndpoints.getAllUsersItemsByListId(itemListId)
      );
      setAllItemsByItemListId(response.data);
      setLoading(true);
    } catch (error) {
      console.error('Error:', error);
    }
  };

  const handleCopyClick = async () => {
    try {
      const decodedToken = jwtDecode(token)
      const userId =
        decodedToken[
          'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'
        ]
      await axios.post(itemListEndpoints.copyItemList(itemListId, userId))
      .then(alert('ItemList is added to Yours'));
    } catch (error) {
      console.error('Error:', error);
    }
  }

  useEffect(() => {
    showAllItemsByItemListId();
  }, [itemListId]);

  return (
    <div className='modalBackground'>
      <div className='modalContainer'>
        <div className='titleCloseBtn'>
          <button onClick={() => closeModal(false)}></button>
        </div>
        <div className='title'>
          <h1>{itemListName}</h1>
        </div>
        <div className='body'>
          {!loading ? (
            <Loader />
          ) : (
            <div className='items-map'>
              {allItemsByItemListId
                .sort((a, b) => a.userItemId - b.userItemId)
                .map(item => (
                  <div className='item-container' key={item.userItemId}>
                    <h3>{item && item.userItemName}</h3>
                  </div>
                ))}
            </div>
          )}
        </div>
        <div className='footer'>
          <Button id='AddItemBtn' onClick={handleCopyClick}>
            Copy
          </Button>
        </div>
      </div>
    </div>
  );
}

export default SimpleItemListModal;

