import React from 'react'
import Checkbox from '@mui/material/Checkbox'
import FormGroup from '@mui/material/FormGroup'
import FormControlLabel from '@mui/material/FormControlLabel'
import FormControl from '@mui/material/FormControl'
import { orange } from '@mui/material/colors'
import { IconButton } from '@mui/material'
import DeleteIcon from '@mui/icons-material/Delete'
import { useEffect, useState } from 'react'
import { userItemEndpoints } from '../endpoints'
import { axios } from '../endpoints'
import '../styles/modal.css'

const label = { inputProps: { 'aria-label': 'Checkbox demo' } }

function UserItem({ item, onItemChange }) {
  const [id, setId] = useState()

  const updateItemState = async (updatedState) => {
    if (id !== null) {
      try {
        const response = await axios.put(
          userItemEndpoints.editUserItem(id),
          updatedState,
          { headers: { 'Content-Type': 'application/json' } }
        )
      } catch (error) {
        console.error('Error updating item state:', error)
      }
    }
    onItemChange()
  }

  const handleCheckboxChange = (e, checkboxType) => {
    const isChecked = e.target.checked
    const newState =
      checkboxType === 'packed' && isChecked
        ? 2
        : checkboxType === 'toBuy' && isChecked
          ? 1
          : 0
    updateItemState(newState)
    return newState
  }

  const deleteUserItem = async () => {
    try {
      await axios.delete(userItemEndpoints.deleteUserItem(id))
      alert('Item is deleted')
    } catch (error) {
      console.error('Error deleting item:', error)
    }
  }

  useEffect(() => {
    setId(item.userItemId)
  }, [item])

  const handleDelete = async (e) => {
    e.preventDefault()
    try {
      await deleteUserItem()
    } catch (error) {
      console.error('Error deleting item:', error)
    }
    onItemChange()
  }

  return (
    <>
    {/* <div className='item-in-modal'> */}
      <IconButton aria-label='delete' size='small' color='white'>
        <DeleteIcon className='deleteIcon' onClick={(e) => handleDelete(e)} />
      </IconButton>
      <div className='item'>{item.userItemName}</div>
      <FormControl component='fieldset'>
        <FormGroup aria-label='position' row>
          <FormControlLabel
            value='bottom'
            control={
              <Checkbox
                {...label}
                checked={item.itemState === 2}
                onChange={(e) => handleCheckboxChange(e, 'packed')}
                sx={{
                  color: orange[500],
                  '&.Mui-checked': {
                    color: orange[600],
                  },
                  '@media (max-width: 600px)': {
                    transform: "scale(0.8)",
                      width: 20,
                      height: 20
                  },
                }}
              />
            }
            label={<span style={{ color: 'orange' }}>Packed</span>}
            labelPlacement='bottom'
          />
          <FormControlLabel
            value='bottom'
            control={
              <Checkbox
                {...label}
                checked={item.itemState === 1}
                onChange={(e) => handleCheckboxChange(e, 'toBuy')}
                sx={{
                  color: orange[500],
                  '&.Mui-checked': {
                    color: orange[600],
                  },
                  '@media (max-width: 600px)': {
                    transform: "scale(0.8)",
                      width: 20,
                      height: 20
                  },
                }}
              />
            }
            label={<span style={{ color: 'orange' }}>To buy</span>}
            labelPlacement='bottom'
          />
        </FormGroup>
      </FormControl>
      {/* </div> */}
    </>
  )
}

export default UserItem
