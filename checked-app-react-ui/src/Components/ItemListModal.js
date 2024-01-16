import React from 'react'
import { useState, useEffect } from 'react'
import '../styles/modal.css'
import axios from 'axios';
import { userItemEndpoints } from '../endpoints';


function ItemListModal({closeModal, itemListName, itemListId}) {
const[allItemsByItemListId, setAllItemsByItemListId] = useState();
const [loading, setLoading] = useState(false);


    // const showAllItemsByItemListId = async () => {
    //     try {
    //       const data = await axios
    //         .get(userItemEndpoints.itemListId(itemListId))
    //         .then((res) => {
    //           console.log(res);
    //           setAllItemsByItemListId(res.data);
    //         });
    //       setLoading(true);
    //     } catch (e) {
    //       console.log(e);
    //     }
    //   };
//userItemEndpoints.itemListId(itemListId, userId)
    const userId = 1;

    const showAllItemsByItemListId = async () => {
        try {
          const response = await axios.get(`https://localhost:7161/${userId}/ByListId/${itemListId}`);
          console.log('Response:', response);
          setAllItemsByItemListId(response.data);
          setLoading(true);
        } catch (error) {
          console.error('Error:', error);
        }
      };

      useEffect(() => {
        showAllItemsByItemListId();
      }, []);

  return (
    <div className='modalBackground'>
        <div className='modalContainer'>
            {/* <div className='titleCloseBtn'>
            <button onClick={() => closeModal(false)}> X </button>
            </div> */}
            <div className='title'>
                <h1>{itemListName}</h1>
            </div>
            <div className='body'>
                {console.log(allItemsByItemListId)}
            </div>
            <div className='footer'>
                <button onClick={() => closeModal(false)} id='cancelBtn'>
                    Cancel
                </button>
            </div>
        </div>
    </div>
  )
}

export default ItemListModal