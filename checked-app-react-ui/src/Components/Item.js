import React from 'react'
import '../styles/item.css'
import Button from '@mui/material/Button'

export default function Item({ item }) {
  return (
    <>
      <div className='oneItem'>
        <div className='itemName'>
          {item.itemName}
        </div>
        <div className='itemBtns'>
        <div className='editItemBtn'>
          <Button variant="outlined">Edit</Button>
        </div>
        <div className='deleteItemBtn'>
          <Button variant="outlined">Delete</Button>
        </div>
        </div>
      </div>
    </>
  )
}
