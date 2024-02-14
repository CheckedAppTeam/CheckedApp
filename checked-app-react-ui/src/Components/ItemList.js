import React from 'react';
import { useState, useCallback, useEffect } from 'react'
import axios from 'axios';
import { itemListEndpoints } from '../endpoints';
import Loader from '../spinners/Loader';
import '../styles/itemList.css'

function ItemList({ itemList, openModalAtIndex, onDelete }) {
  const [isLoading, setIsLoading] = useState(true);

  const { itemListId, listName, travelDestination, travelDate, isPublic } = itemList;
  
  const handleClick = useCallback(() => {
    openModalAtIndex(itemListId, listName);
  }, [itemListId, listName, openModalAtIndex]);

 const deleteItemList = async() => {
      try {
          const response = await axios.delete(itemListEndpoints.deleteList(itemListId));
          onDelete(itemListId);
          alert('ItemList is deleted');
      } catch (error) {
          console.error('Error deleting item:', error);
      }
  }

  useEffect(() => {
    if (itemList !== undefined) {
      setIsLoading(false);
    }
  }, [itemList]);

  return (
    <>
      {isLoading ? (
        <Loader/>
      ) : (
        <>
            <div className='itemList' key={itemListId}>
              <div className='titleCloseBtn'>
                <button onClick={deleteItemList}></button>
              </div>
              <h2 className='openModalClick' onClick={handleClick}>
                {listName}
              </h2>
              <p>{travelDestination}</p>
              <p>{formatDate(travelDate)}</p>
              {isPublic ? (
                <p className='public'>public</p>
              ) : (
                <p className='private'>private</p>
              )}
            </div>
        </>
      )}
    </>
  );
  
}

function formatDate(dateString) {
    try {
      const date = new Date(dateString)
      const formattedDate = date.toISOString().split('T')[0]
      return formattedDate
    } catch (e) {
      console.error(e)
    }
}

export default ItemList

