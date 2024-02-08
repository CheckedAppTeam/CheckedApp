import React from 'react'

function ItemList({ itemList, openModalAtIndex }) {
  const { itemListId, listName, travelDestination, travelDate, isPublic } =
    itemList

  return (
    <div className='itemList' key={itemListId}>
      <h2
        className='openModalClick'
        onClick={() => {
          openModalAtIndex(itemListId, listName)
        }}
      >
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
  )
}

function formatDate(dateString) {
  const date = new Date(dateString)
  const formattedDate = date.toISOString().split('T')[0]
  return formattedDate
}

export default ItemList
