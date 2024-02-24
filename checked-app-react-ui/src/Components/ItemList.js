import React from 'react'
import { useState, useCallback, useEffect } from 'react'
import axios from 'axios'
import { itemListEndpoints } from '../endpoints'
import Loader from '../spinners/Loader'
import '../styles/itemList.css'
import FormControlLabel from '@mui/material/FormControlLabel'
import { orange } from '@mui/material/colors'
import Checkbox from '@mui/material/Checkbox'

function ItemList({ itemList, openModalAtIndex, onDelete, onChange }) {
  const { itemListId, listName, travelDestination, travelDate, isPublic } =
    itemList
  const [isLoading, setIsLoading] = useState(true)
  const [isEditing, setIsEditing] = useState(false)
  const [editDate, setEditDate] = useState(travelDate)
  const [editListName, setEditListName] = useState(listName)
  const [editIsPublic, setEditIsPublic] = useState(isPublic)
  const [editTravelDestination, setEditTravelDestination] =
    useState(travelDestination)

  const handleClick = useCallback(() => {
    openModalAtIndex(itemListId, listName)
  }, [itemListId, listName, openModalAtIndex])

  const deleteItemList = async () => {
    try {
      const response = await axios.delete(
        itemListEndpoints.deleteList(itemListId)
      )
      onDelete(itemListId)
      alert('ItemList is deleted')
    } catch (error) {
      console.error('Error deleting item:', error)
    }
  }

  const handleEdit = () => {
    setIsEditing(true)
  }

  const handleItemListElementChange = (event) => {
    const { name, value } = event.target
    switch (name) {
      case 'listName':
        setEditListName(value);
        onChange(itemListId, name, value)
        break;
      case 'travelDestination':
        setEditTravelDestination(value);
        onChange(itemListId, name, value)
        break;
      case 'travelDate':
        setEditDate(value);
        onChange(itemListId, name, value)
        break;
      default:
        break;
    }
  }

  const handleCheckboxChange = (e) => {
    const isChecked = e.target.checked
    setEditIsPublic(isChecked)
    onChange(itemListId, 'isPublic', isChecked)
  }

  const saveChanges = async () => {
    try {
      const isoString = new Date(editDate).toISOString()

      await axios.put(itemListEndpoints.updateItemList(itemListId), {
        ItemListName: editListName,
        ItemListDestination: editTravelDestination,
        Date: isoString,
        ItemListPublic: editIsPublic,
      })
      setIsEditing(false)
    } catch (error) {
      console.error('Error while sending data', error)
    }
  }

  const handleCancel = () => {
    setIsEditing(false)
  }

  useEffect(() => {
    if (itemList !== undefined) {
      setIsLoading(false)
    }
  }, [itemList])

  return (
    <>
      {isLoading ? (
        <Loader />
      ) : (
        <>
          {isEditing && (
            <div className='itemList'>
              <div className='titleCloseBtn'>
                <button onClick={deleteItemList}></button>
              </div>
              <label htmlFor='listName'>List name</label>
              <input
                type='text'
                name='listName'
                value={editListName}
                onChange={handleItemListElementChange}
              />
              <label htmlFor='destination'>Destination</label>

              <input
                type='text'
                name='travelDestination'
                value={editTravelDestination}
                onChange={handleItemListElementChange}
              />
              <label htmlFor='age'>Date</label>

              <input
                type='date'
                name='travelDate'
                value={editDate}
                onChange={handleItemListElementChange}
              />
              <label>
                Do you want to make it public?
                <FormControlLabel
                  control={
                    <Checkbox
                      checked={editIsPublic}
                      onChange={handleCheckboxChange}
                      sx={{
                        color: orange[500],
                        '&.Mui-checked': {
                          color: orange[600],
                        },
                      }}
                    />
                  }
                  label='Public'
                />
              </label>
              <div className='editItemListBtn'>
                <button onClick={saveChanges}>Submit</button>
              </div>
              <div className='editItemListBtn'>
                <button onClick={handleCancel}>Cancel</button>
              </div>
            </div>
          )}
          {!isEditing && (
            <div className='itemList'>
              <div className='titleCloseBtn'>
                <button onClick={deleteItemList}></button>
              </div>
              <h2 className='openModalClick' onClick={handleClick}>
                {listName}
              </h2>
              <p>{travelDestination}</p>
              <p>{formatDate(travelDate)}</p>
              {isPublic ? (
                <p className='public'>public</p>
              ) : (
                <p className='private'>private</p>
              )}
              <div className='editItemListBtn'>
                <button onClick={handleEdit}>Edit</button>
              </div>
            </div>
          )}
        </>
      )}
    </>
  )
}

function formatDate(dateString) {
  try {
    const date = new Date(dateString)
    const formattedDate = date.toISOString().split('T')[0]
    return formattedDate
  } catch (e) {
    console.error(e)
  }
}

export default ItemList
