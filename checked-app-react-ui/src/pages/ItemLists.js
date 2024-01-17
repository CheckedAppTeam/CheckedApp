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
  const [allItemListsResponseData, setAllitemListsResponseData] = useState(null)
  const [loading, setLoading] = useState(false);
  const [openModal, setOpenModal] = useState(false)
  const [currentId, setCurrentId] = useState()
  const [currentListName, setCurrentListName] = useState()
  // const [openModalAtIndex, setOpenModalAtIndex] = useState()

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
    showItemListsById();
  }, []);

  const openModalAtIndex = async(index, name) =>{
    if(index !== null){
      setOpenModal(true)
    }
    setCurrentId(index)
    setCurrentListName(name)
  }


  return (
    <>
      {console.log(itemListEndpoints)}
      <h1>All Your Lists</h1>
      {!loading && <Loader />}
      {allItemListsResponseData && allItemListsResponseData.ownItemList && (
        <div className='item-lists'>
          {allItemListsResponseData && allItemListsResponseData.ownItemList.map((itemList, index) => (
            <div className='itemList' key={index}>
              <h2 className='openModalClick' onClick={() => {openModalAtIndex(itemList.itemListId, itemList.listName)}}>{itemList.listName}</h2>
              {/* {openModal && <ItemListModal closeModal={setOpenModal} itemListName={itemList.listName} itemListId={itemList.itemListId}/>} */}
              <p>{itemList.travelDestination}</p>
              <p>{formatDate(itemList.travelDate)}</p>
              {itemList.isPublic ? <p className="public">public</p> : <p className="private">private</p>}
            </div>
          ))}
        </div>
      )}
      {openModal && <ItemListModal closeModal={setOpenModal} itemListName={currentListName} itemListId={currentId}/>}
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

