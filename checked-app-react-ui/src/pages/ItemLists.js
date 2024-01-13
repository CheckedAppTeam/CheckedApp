import { useState, useEffect } from 'react'
import axios from 'axios'
import { itemListEndpoints } from '../endpoints'
import { userEndpoints } from '../endpoints'
import '../styles/itemLists.css'

export function ItemLists() {
  const [itemListResponseData, setItemListResponseData] = useState(null)
  const [allItemListsResponseData, setAllitemListsResponseData] = useState(null)

  useEffect(() => {
    axios
      .get(itemListEndpoints.getAllList)
      .then((response) => {
        setItemListResponseData(response.data)
      })
      .catch((error) => {
        console.log(error)
      })
    const userId = 1 // Replace with the actual user ID
    axios
      .get(userEndpoints.getUserData(userId))
      .then((response) => {
        setAllitemListsResponseData(response.data)
      })
      .catch((error) => {
        console.log(error)
      })
  }, [])

  return (
    <>
      <h1>Item Lists</h1>
      {allItemListsResponseData && allItemListsResponseData.ownItemList && (
        <div className='item-lists'>
          {allItemListsResponseData.ownItemList.map((item, index) => (
            <div className='item' key={index}>
              <a href={`/itemLists/${item.listName}`}>{item.listName}</a>
              <p>{item.travelDestination}</p>
              <p>{item.travelDate}</p>
              {item.isPublic ? (
                <p className='public'>public</p>
              ) : (
                <p className='private'>private</p>
              )}
            </div>
          ))}
        </div>
      )}
      <div>
        {/* <pre> */}
        {/* <h2>{JSON.stringify(itemListResponseData,null,2)}</h2> */}
        {/* <p>{JSON.stringify(allItemListsResponseData, null, 2)}</p> */}
        {/* </pre> */}
      </div>
    </>
  )
}
