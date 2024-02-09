import React from 'react'
import { useState, useCallback, useEffect } from 'react'

function ItemList({ itemList, openModalAtIndex }) {
  const [isLoading, setIsLoading] = useState(true)

  const { itemListId, listName, travelDestination, travelDate, isPublic } =
    itemList

  const handleClick = useCallback(() => {
    openModalAtIndex(itemListId, listName)
  }, [itemListId, listName, openModalAtIndex])

  useEffect(() => {
    if (itemList !== undefined) {
      setIsLoading(false)
    }
  }, [itemList])

  return (
    <>
      {isLoading ? (
        <div>Loading...</div>
      ) : (
        <>
          <div className='itemList' key={itemListId}>
            <div className='titleCloseBtn'>
              <button></button>
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
  )
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
