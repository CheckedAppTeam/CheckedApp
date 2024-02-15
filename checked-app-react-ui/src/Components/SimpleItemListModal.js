import React, { useState, useEffect } from 'react'
import axios from 'axios'
import { userItemEndpoints } from '../endpoints'
import Loader from '../spinners/Loader'

function SimpleItemListModal({ closeModal, itemListName, itemListId }) {
  const [allItemsByItemListId, setAllItemsByItemListId] = useState([])
  const [loading, setLoading] = useState(false)

  const showAllItemsByItemListId = async () => {
    try {
      const response = await axios.get(
        userItemEndpoints.getAllUsersItemsByListId(itemListId)
      )
      setAllItemsByItemListId(response.data)
      setLoading(true)
    } catch (error) {
      console.error('Error:', error)
    }
  }

  useEffect(() => {
    showAllItemsByItemListId()
  }, [itemListId])

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
            <div className='items'>
              {allItemsByItemListId
                .sort((a, b) => a.userItemId - b.userItemId)
                .map((item, index) => (
                  <div className='item-container' key={item.userItemId}>
                    {console.log(item)}
                    <h2>{item && item.userItemName}</h2>
                  </div>
                ))}
            </div>
          )}
        </div>
      </div>
    </div>
  )
}

export default SimpleItemListModal