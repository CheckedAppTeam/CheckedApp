import { useState, useEffect } from 'react'
import axios from 'axios'
import { itemListEndpoints } from '../endpoints'
import { userEndpoints } from '../endpoints'
import '../styles/itemLists.css'
import { Link } from 'react-router-dom'
import '../axios/ItemListAxios.js'
import { ItemListAxios } from '../axios/ItemListAxios.js'
import "../App.css";
import Loader from '../spinners/Loader.js';
import ItemListModal from '../Components/ItemListModal.js'

export function ItemLists() {
  const [allItemListsData, setAllItemListsData] = useState(null)
  const [allItemListsResponseData, setAllitemListsResponseData] = useState(null)
  const [loading, setLoading] = useState(false);
  const [openModal, setOpenModal] = useState(false)

  const showAllItemLists = async () => {
    try {
      const data = await axios
        .get(itemListEndpoints.getAllList)
        .then((res) => {
          console.log(res);
          setAllItemListsData(res.data);
        });
      setLoading(true);
    } catch (e) {
      console.log(e);
    }
  };

  const userId = 1

  const showItemListsById = async () => {
    try {
      const data = await axios
      .get(userEndpoints.getUserData(userId))
      .then((res) => {
        console.log(res);
        setAllitemListsResponseData(res.data)
      });
      setLoading(true);
    } catch (e) {
      console.log(e);
    }
  }

  useEffect(() => {
    showAllItemLists();
    showItemListsById();
  }, []);



  return (
    <>
      <h1>All Your Lists</h1>
      {! loading && <Loader/>}
      {allItemListsResponseData && allItemListsResponseData.ownItemList && (
  <div className='item-lists'>
    {allItemListsResponseData.ownItemList.map((itemList, index) => (
  <div className='item' key={index}>
    <h2 className='openModalClick' onClick={() => {setOpenModal(true)}}>{itemList.listName}</h2>
    {openModal && <ItemListModal closeModal={setOpenModal} itemListName={itemList.listName} itemListId={itemList.itemListId}/>}
    <p>{itemList.travelDestination}</p>
    <p>{formatDate(itemList.travelDate)}</p>
    {itemList.isPublic ? <p className="public">public</p> : <p className="private">private</p>}
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

function formatDate(dateString) {
  const date = new Date(dateString);
  const formattedDate = date.toISOString().split('T')[0];
  return formattedDate;
}