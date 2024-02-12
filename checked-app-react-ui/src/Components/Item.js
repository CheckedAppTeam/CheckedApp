import React from 'react'
import '../styles/item.css'
import Button from '@mui/material/Button'
import { useState, useEffect } from 'react'
import { itemEndpoints } from '../endpoints'
import axios from 'axios'

export default function Item({ item, onUpdate }) {
  const [editedItemName, setEditedItemName] = useState(item.itemName);
  const [isEditing, setIsEditing] = useState(false)

  useEffect(() => {
    // setItemName(item.itemName);
    setEditedItemName(item.itemName);
  }, [item]);


  const handleInputChange = (event) => {
    const { value } = event.target;
    setEditedItemName(value);
  };

  const toggleEdit = () => {
    setIsEditing(!isEditing)
  }

  const saveChanges = () => {
    axios
      .put(itemEndpoints.editItem(item.itemId), { ItemName: editedItemName })
      .then(() => {
        setIsEditing(false);
        onUpdate(item.itemId)
      })
      .catch((error) => {
        console.error('Error while sending data', error)
      })
  }

  return (
    <>
      {!isEditing &&
        <div className='oneItem'>
          <div className='itemName'>
            {item.itemName}
          </div>
          <div className='itemBtns'>
            <div className='editItemBtn'>
              <Button variant="outlined" onClick={toggleEdit}>Edit</Button>
            </div>
            <div className='deleteItemBtn'>
              <Button variant="outlined">Delete</Button>
            </div>
          </div>
        </div>
      }
      {isEditing &&
        <div className='oneItem'>
          <input
            type='text'
            name='ItemName'
            value={editedItemName}
            onChange={handleInputChange}
          />
          <div className='itemBtns'>
            <div className='submitBtn'>
              <Button variant="outlined" onClick={saveChanges}>Submit</Button>
            </div>
          </div>
        </div>
      }
    </>
  )
}
