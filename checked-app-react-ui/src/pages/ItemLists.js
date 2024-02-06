import { useState, useEffect } from 'react'
import axios from 'axios'
import { userEndpoints } from '../endpoints'
import '../styles/itemLists.css'
import { Link } from 'react-router-dom'
import '../styles/main.css'
import Loader from '../spinners/Loader.js'
import ItemListModal from '../Components/ItemListModal.js'
import jwt_decode from 'jwt-decode'
import { itemListEndpoints } from '../endpoints'
import { useAuth } from '../Contexts/AuthContext.js'
import { jwtDecode } from 'jwt-decode'
import ItemList from '../Components/ItemList.js';
import Grid from '@mui/material/Grid';


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
        {allItemListsResponseData && allItemListsResponseData.ownItemList && (
          <div className='item-lists'>
            <Grid container spacing={{ xs: 2, md: 3 }} columns={{ xs: 4, sm: 8, md: 12 }}>
              {allItemListsResponseData.ownItemList.map((itemList, index) => (
                <Grid xs={2} sm={4} md={4} key={itemList.ItemListId}>
                  <ItemList itemList={itemList} openModalAtIndex={openModalAtIndex}>xs=2</ItemList>
                </Grid>
              ))}
            </Grid>
          </div>
        )}
        <div className='footerButton'>
          <div className='AddItemListButton'>
            <button >Add</button>
          </div>
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


