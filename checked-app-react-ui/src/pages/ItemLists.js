import { useState, useEffect } from 'react'
import axios from 'axios'
import { userEndpoints } from '../endpoints'
import '../styles/itemLists.css'
import '../styles/main.css'
import Loader from '../spinners/Loader.js'
import ItemListModal from '../Components/ItemListModal.js'
import jwt_decode from 'jwt-decode' // to używane jest? będzie? kasujcie od razu?
import { itemListEndpoints } from '../endpoints'
import { useAuth } from '../Contexts/AuthContext.js'
import { jwtDecode } from 'jwt-decode'
import ItemList from '../Components/ItemList.js'

export function ItemLists() {
  const [allItemListsResponseData, setAllitemListsResponseData] = useState(null)
  const [loading, setLoading] = useState(false)
  const [openModal, setOpenModal] = useState(false)
  const [currentId, setCurrentId] = useState()
  const [currentListName, setCurrentListName] = useState()
  const { token } = useAuth()

  useEffect(() => {
    const fetchData = async () => {
      if (token) {
        try {
          const decodedToken = jwtDecode(token)
          const userId =
            decodedToken[
              'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'
            ]
          const data = await axios.get(userEndpoints.getUserData(userId))
          setAllitemListsResponseData(data.data)
          setLoading(true)
        } catch (error) {
          console.error(error)
        }
      }
    }

    fetchData()
  }, [token])

  const openModalAtIndex = (index, name) => {
    if (index !== null) {
      setOpenModal(true)
    }
    setCurrentId(index)
    setCurrentListName(name)
  }

  return (
    <div className='itemListBackground'>
      {console.log(itemListEndpoints)}
      <div className='body'>
        <h1>All Your Lists</h1>
        {!loading && <Loader />}
        {!loading && <Loader />}
        {allItemListsResponseData && allItemListsResponseData.ownItemList && (
          <div className='item-lists'>
            {allItemListsResponseData.ownItemList.map((itemList, index) => (
              <ItemList
                key={index}
                itemList={itemList}
                openModalAtIndex={openModalAtIndex}
              />
            ))}
          </div>
        )}
        <div className='footer'>
          <button className='AddButton'>Add</button>
        </div>
        {openModal && (
          <ItemListModal
            closeModal={setOpenModal}
            itemListName={currentListName}
            itemListId={currentId}
          />
        )}
      </div>
    </div>
  )
}
