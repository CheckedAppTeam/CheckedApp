import { useState, useEffect } from 'react'
import axios from 'axios'
import { userEndpoints } from '../endpoints'
import '../styles/itemLists.css'
import { Link } from 'react-router-dom' // do wywalenia??
import '../styles/main.css'
import Loader from '../spinners/Loader.js'
import ItemListModal from '../Components/ItemListModal.js'
import jwt_decode from 'jwt-decode' // to używane jest? będzie? kasujcie od razu?
import { itemListEndpoints } from '../endpoints'
import { useAuth } from '../Contexts/AuthContext.js'
import { jwtDecode } from 'jwt-decode'

export function ItemLists() {
  const [allItemListsResponseData, setAllitemListsResponseData] = useState(null)
  const [loading, setLoading] = useState(false)
  const [openModal, setOpenModal] = useState(false)
  const [currentId, setCurrentId] = useState()
  const [currentListName, setCurrentListName] = useState()
  const { token } = useAuth()

  // const token = localStorage.getItem("token");

  useEffect(() => {
    const fetchData = async () => {
      if (token) {
        try {
          const decodedToken = jwtDecode(token)
          // console.log('Decoded Token:', decodedToken);
          const userId =
            decodedToken[
              'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'
            ]
          const data = await axios.get(userEndpoints.getUserData(userId))
          // console.log(data);
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
    <>
      <div className='itemListBackground'>
        {console.log(itemListEndpoints)}
        <div className='body'>
          <h1>All Your Lists</h1>
          {!loading && <Loader />}
          {allItemListsResponseData && allItemListsResponseData.ownItemList && (
            <div className='item-lists'>
              {allItemListsResponseData &&
                allItemListsResponseData.ownItemList.map((itemList, index) => (
                  <div className='itemList' key={index}>
                    <h2
                      className='openModalClick'
                      onClick={() => {
                        openModalAtIndex(itemList.itemListId, itemList.listName)
                      }}
                    >
                      {itemList.listName}
                    </h2>
                    <p>{itemList.travelDestination}</p>
                    <p>{formatDate(itemList.travelDate)}</p>
                    {itemList.isPublic ? (
                      <p className='public'>public</p>
                    ) : (
                      <p className='private'>private</p>
                    )}
                  </div>
                ))}
            </div>
          )}
          <div className='footer'>
            <div className='AddButton'>
              <button>Add</button>
            </div>
          </div>
          {openModal && (
            <ItemListModal
              closeModal={setOpenModal}
              itemListName={currentListName}
              itemListId={currentId}
            />
          )}
          <div>
            {/* <pre> */}
            {/* <h2>{JSON.stringify(itemListResponseData,null,2)}</h2> */}
            {/* <p>{JSON.stringify(allItemListsResponseData, null, 2)}</p> */}
            {/* </pre> */}
          </div>
        </div>
      </div>
    </>
  )
}

function formatDate(dateString) {
  const date = new Date(dateString)
  const formattedDate = date.toISOString().split('T')[0]
  return formattedDate
}
