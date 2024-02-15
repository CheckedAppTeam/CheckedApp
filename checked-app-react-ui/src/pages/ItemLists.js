import { useState, useEffect } from 'react'
import axios from 'axios'
import { userEndpoints } from '../endpoints'
import '../styles/itemLists.css'
import '../styles/main.css'
import Loader from '../spinners/Loader.js'
import ItemListModal from '../Components/ItemListModal.js'
import { itemListEndpoints } from '../endpoints'
import { useAuth } from '../Contexts/AuthContext.js'
import { jwtDecode } from 'jwt-decode'
import ItemList from '../Components/ItemList.js'
import Grid from '@mui/material/Grid'
import Checkbox from '@mui/material/Checkbox'
import FormControlLabel from '@mui/material/FormControlLabel'
import { orange } from '@mui/material/colors'

export function ItemLists() {
  const [allItemListsResponseData, setAllitemListsResponseData] = useState(null)
  const [loading, setLoading] = useState(false)
  const [openModal, setOpenModal] = useState(false)
  const [currentId, setCurrentId] = useState()
  const [currentListName, setCurrentListName] = useState()
  const [showForm, setShowForm] = useState(false)
  const [newListData, setNewListData] = useState({
    ItemListName: '',
    Date: new Date(),
    ItemListPublic: false,
    ItemListDestination: '',
  })
  const { token } = useAuth()

  const fetchData = async () => {
    const storedToken = localStorage.getItem('token');
  if (storedToken) {
    axios.defaults.headers.common['Authorization'] =`Bearer ${storedToken}`;
  }
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
  useEffect(() => {
    fetchData()
  }, [])

  const openModalAtIndex = (index, name) => {
    if (index !== null) {
      setOpenModal(true)
    }
    setCurrentId(index)
    setCurrentListName(name)
  }

  const handleAddItemList = (e) => {
    e.preventDefault()
    setShowForm(true)
  }

  const handleChange = (e) => {
    const { name, value } = e.target
    setNewListData({ ...newListData, [name]: value })
  }


  const handleSubmit = async (e) => {
    e.preventDefault()

    try {
      const decodedToken = jwtDecode(token)
      const userId =
        decodedToken[
          'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'
        ]
      const dateObject = newListData.Date
      try {
        const isoString = new Date(dateObject).toISOString()
        const requestData = { ...newListData, Date: isoString }
        const response = await axios.post(
          itemListEndpoints.addList(userId),
          requestData
        )
        const updatedItemLists = [
          ...allItemListsResponseData.ownItemList,
          response.data,
        ]
        setAllitemListsResponseData({
          ...allItemListsResponseData,
          ownItemList: updatedItemLists,
        })
        await fetchData()
      } catch (error) {
        console.error(error)
      }
    } catch (error) {
      console.error(error)
    }
    setShowForm(false)
  }

  const handleDelete = (itemId) => {
    const updatedItemLists = allItemListsResponseData.ownItemList.filter(
      (item) => item.itemListId !== itemId
    )
    setAllitemListsResponseData({
      ...allItemListsResponseData,
      ownItemList: updatedItemLists,
    })
  }

  const handleCheckboxChange = () => {
    setNewListData({
      ...newListData,
      ItemListPublic: !newListData.ItemListPublic,
    })
  }

  return (
    <div className='itemListBackground'>
      <div className='body'>
        <h1>All Your Lists</h1>
        <div className='footerButton'>
          <div className='AddItemListButton'>
            <button onClick={handleAddItemList}>Add</button>
          </div>
        </div>
        {showForm && (
          <form className='addItemListForm' onSubmit={handleSubmit}>
            <div className='closeBtnForm'>
              <button onClick={() => setShowForm(false)}></button>
            </div>
            <label>
              Name:
              <input
                type='text'
                name='ItemListName'
                value={newListData.ItemListName}
                onChange={handleChange}
                required
              />
            </label>
            <label>
              Destination:
              <input
                type='text'
                name='ItemListDestination'
                value={newListData.ItemListDestination}
                onChange={handleChange}
                required
              />
            </label>
            <label>
              Date:
              <input
                type='date'
                name='Date'
                value={newListData.Date}
                onChange={handleChange}
                required
              />
            </label>
            <label>
              Do you want to make it public:
              <FormControlLabel
                control={
                  <Checkbox
                    checked={newListData.ItemListPublic}
                    onChange={handleCheckboxChange}
                    sx={{
                      color: orange[500],
                      '&.Mui-checked': {
                        color: orange[600],
                      }
                    }}
                  />
                }
                label='Public'
              />
            </label>
            <button className='submitButton' type='submit'>
              Submit
            </button>
          </form>
        )}
        {!loading && <Loader />}
        {allItemListsResponseData && allItemListsResponseData.ownItemList && (
          <div className='item-lists'>
            <Grid
              container
              spacing={{ xs: 2, md: 2 }}
              columns={{ xs: 4, sm: 8, md: 12 }}
            >
              {allItemListsResponseData.ownItemList.map((item, index) => (
                <Grid item xs={2} sm={4} md={4} key={index}>
                  <ItemList
                    key={item.ItemListId}
                    itemList={item}
                    openModalAtIndex={openModalAtIndex}
                    onDelete={handleDelete}
                    addedList={newListData}
                    onChange={fetchData}
                  >
                    xs=2
                  </ItemList>
                </Grid>
              ))}
            </Grid>
          </div>
        )}
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
