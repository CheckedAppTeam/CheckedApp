import React from 'react'
import { useState, useEffect } from 'react'
import '../styles/modal.css'
import axios from 'axios';
import { userItemEndpoints } from '../endpoints';
import Loader from '../spinners/Loader';


function ItemListModal({ closeModal, itemListName, itemListId }) {
    const [allItemsByItemListId, setAllItemsByItemListId] = useState();
    const [loading, setLoading] = useState(false);
    //userItemEndpoints.itemListId(itemListId, userId)


    const showAllItemsByItemListId = async () => {
        try {
            const response = await axios.get(`https://localhost:7161/UserId/ByListId/${itemListId}`);
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
                    {!loading && <Loader />}
                </div>
                <div className='body'>
                    {console.log(allItemsByItemListId)}
                    <div className='items'>
                        {allItemsByItemListId && allItemsByItemListId.map((item, index) => (
                            <div className='item' key={index}>
                                {item.userItemName}
                            </div>
                        ))}
                    </div>
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