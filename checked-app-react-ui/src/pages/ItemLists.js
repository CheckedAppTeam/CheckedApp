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
      <h1>Item Lists</h1>
      {! loading && <Loader/>}
      {allItemListsResponseData && allItemListsResponseData.ownItemList && (
  <div className='item-lists'>
    {/* {loading ? showAllItemLists : <ReactBootStrap.Spinner animation="border" />} */}
    {allItemListsResponseData.ownItemList.map((itemList, index) => (
  <div className='item' key={index}>
    {/* Use Link component for internal navigation */}
    <button className='openModalBtn' onClick={() => {setOpenModal(true)}}>{itemList.listName}</button>
    {openModal && <ItemListModal closeModal={setOpenModal} itemListName={itemList.listName} itemListId={itemList.itemListId}/>}
{/*     
    <Link to={`/itemlists/${item.itemListId}`}>
      {item.listName}
    </Link> */}
    <p>{itemList.travelDestination}</p>
    <p>{formatDate(itemList.travelDate)}</p>
    {itemList.isPublic ? <p className="public">public</p> : <p className="private">private</p>}
  </div>
))}
{/* {openModal && <ItemListModal closeModal={setOpenModal}/>} */}
{/* {allItemListsResponseData.ownItemList.map((item, index) => (
      <div className = 'item' key={index}>
        <a href={`/itemLists/${item.itemListId}`}>
          {item.listName}
        </a>
        <p>{item.travelDestination}</p>
        <p>{formatDate(item.travelDate)}</p>
        {item.isPublic ? <p className="public">public</p> : <p className="private">private</p>}
      </div>
    ))} */}
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