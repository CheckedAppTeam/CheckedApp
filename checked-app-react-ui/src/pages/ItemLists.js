import { useState, useEffect, useRef } from 'react'
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
import Checkbox from '@mui/material/Checkbox';
import FormGroup from '@mui/material/FormGroup';
import FormControlLabel from '@mui/material/FormControlLabel';
import FormControl from '@mui/material/FormControl';
import { orange } from '@mui/material/colors';
import { IconButton } from '@mui/material';
import DeleteIcon from '@mui/icons-material/Delete';
import { NewList } from './NewList.js'

export function ItemLists() {
  const [allItemListsResponseData, setAllitemListsResponseData] = useState(null)
  const [loading, setLoading] = useState(false)
  const [openModal, setOpenModal] = useState(false)
  const [currentId, setCurrentId] = useState()
  const [currentListName, setCurrentListName] = useState()
  const [showForm, setShowForm] = useState(false);
  const [newListData, setNewListData] = useState({
    ItemListName: '',
    Date: new Date(),
    ItemListPublic: false,
    ItemListDestination: ''
  });
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

  // const childRef = useRef(null);
  // function handleClick() {
  //   childRef.current.openModal
  // }

  const openModalAtIndex = (index, name) => {
    if (index !== null) {
      setOpenModal(true)
    }
    setCurrentId(index)
    setCurrentListName(name)
  }



  const handleAddItemList = () => {
    setShowForm(true);
  };

  const handleChange = (e) => {
    const { name, value } = e.target;
    setNewListData({ ...newListData, [name]: value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    handleAddItemList(newListData);
    setShowForm(false);

    try {
      const decodedToken = jwtDecode(token)
      const userId =
        decodedToken[
        'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'
        ]
      console.log(newListData)

      const dateObject = newListData.Date;
      console.log(dateObject)
      console.log(newListData.Date)
      console.log(new Date())
      try {
        const isoString = new Date(dateObject).toISOString();
        const requestData = { ...newListData, Date: isoString };
        console.log(requestData);

        console.log(requestData)
        const response = await axios.post(itemListEndpoints.addList(userId), requestData);
        console.log(response.data)
        const updatedItemLists = [...allItemListsResponseData.ownItemList, response.data];
        setAllitemListsResponseData({ ...allItemListsResponseData, ownItemList: updatedItemLists });
        // setAllitemListsResponseData(prevState => {
        //   const updatedItemLists = [...prevState.ownItemList, response.data];
        //   return { ...prevState, ownItemList: updatedItemLists };
        // });

      } catch (error) {
        console.error(error)
      }


    } catch (error) {
      console.error(error);
    }
  };

  const handleCheckboxChange = (e) => {
    const isChecked = e.target.checked;
    setNewListData({ ...newListData, isPublic: isChecked });
  };

  return (
    <div className='itemListBackground'>
      <div className='body'>
        <h1>All Your Lists</h1>
        {!loading && <Loader />}
        {allItemListsResponseData && allItemListsResponseData.ownItemList && (
          <div className='item-lists'>
            <Grid container spacing={{ xs: 2, md: 3 }} columns={{ xs: 4, sm: 8, md: 12 }}>
              {allItemListsResponseData.ownItemList.map((item, index) => (
                <Grid item xs={2} sm={4} md={4} key={index}>
                  <ItemList key={item.ItemListId} itemList={item} openModalAtIndex={openModalAtIndex}>xs=2</ItemList>
                </Grid>
              ))}
            </Grid>
          </div>
        )}
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
                type="text"
                name="ItemListName"
                value={newListData.ItemListName}
                onChange={handleChange}
                required
              />
            </label>
            <label>
              Destination:
              <input
                type="text"
                name="ItemListDestination"
                value={newListData.ItemListDestination}
                onChange={handleChange}
                required
              />
            </label>
            <label>
              Date:
              <input
                type="date"
                name="Date"
                value={newListData.Date}
                onChange={handleChange}
                required
              />
            </label>
            <label>
              Do you want to make it public:
              <FormControlLabel
                control={<Checkbox checked={newListData.ItemListPublic} onChange={handleCheckboxChange} />}
                label="Public"
              />
            </label>
            <button className='submitButton' type="submit">Submit</button>
          </form>
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


