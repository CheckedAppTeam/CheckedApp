import React from 'react'
import { useState } from 'react'
import '../styles/modal.css'

function ItemListModal({closeModal, itemListName, itemListId}) {

    

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
                <p>
                    text
                </p>
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