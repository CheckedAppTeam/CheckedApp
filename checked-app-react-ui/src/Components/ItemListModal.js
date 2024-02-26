import React from 'react'
import { useState, useEffect } from 'react'
import axios from 'axios'
import { itemEndpoints, userItemEndpoints } from '../endpoints'
import Loader from '../spinners/Loader'
import UserItem from './UserItem'
import Select from 'react-select'
import Button from '@mui/material/Button'

function ItemListModal({ closeModal, itemListName, itemListId }) {
  const [allItemsByItemListId, setAllItemsByItemListId] = useState([])
  const [loading, setLoading] = useState(false)
  const [showSelect, setShowSelect] = useState(false)
  const [allItems, setAllItems] = useState([])
  const [selectedItem, setSelectItem] = useState()
  const [inputValue, setInputValue] = useState('')
  const [showAdd, setShowAdd] = useState(false)
  const [showBack, setShowBack] = useState(false)
  const [showItemAdd, setShowItemAdd] = useState(true)
  const [showNewItemForm, setShowNewItemForm] = useState(false)
  const [newItemName, setNewItemName] = useState('')
  const [showAddNew, setShowAddNew] = useState(false)
  const [userItemDTO, setUserItemDTO] = useState({
    itemListId: 0,
    itemId: 0,
    itemState: 0,
  })

  const customStyles = {
    option: (provided, state) => ({
      ...provided,
      borderBottom: '1px solid #ccc',
      color: state.isSelected ? 'white' : 'white',
      background: state.isSelected ? '#FF9B36' : '#9B9BFF',
      zIndex: 9900,
    }),
    menu: (provided) => ({
      ...provided,
      maxHeight: '150px',
      zIndex: 99999,
      position: 'absolute',
    }),
    control: (provided) => ({
      ...provided,
      backgroundColor: '#9B9BFF',
      color: 'white',
      borderRadius: '8px',
    }),
    placeholder: (provided) => ({
      ...provided,
      color: 'white',
    }),
    noOptionsMessage: (provided) => ({
      ...provided,
      backgroundColor: '#9B9BFF',
      color: 'white',
    }),
  }

  const handleAddNewItem = async () => {
    setShowNewItemForm(true)
    setShowBack(true)
    setShowSelect(true)
    setShowItemAdd(false)
    setShowAdd(false)
    setShowAddNew(false)
  }

  const alternateCase = (word) => {
    return word
      .split('')
      .map((char, index) => {
        return index === 0 ? char.toUpperCase() : char.toLowerCase()
      })
      .join('')
  }

  const handleNewItemSubmit = async () => {
    try {
      const AddedItem = { itemName: alternateCase(newItemName) }
      await axios.post(itemEndpoints.addItem, AddedItem).then((response) => {
      })
      const newItem = await axios.get(
        itemEndpoints.getItemByName(AddedItem.itemName)
      )
      const newUserItemDTO = {
        itemListId: itemListId,
        itemId: newItem.data.itemId,
        itemState: 0,
      }

      const newItemAdded = await axios.post(
        userItemEndpoints.addUserItem,
        newUserItemDTO
      )
      setUserItemDTO(userItemDTO)
      setAllItemsByItemListId((prevItems) => [...prevItems, newItemAdded])
      showAllItemsByItemListId()

      setNewItemName('')
      setShowNewItemForm(false)
      setShowBack(true)
      setShowSelect(true)
      setShowItemAdd(false)
      setShowAdd(false)
      setShowAddNew(true)
    } catch (error) {
      console.error('Error adding new item:', error)
    }
  }

  const showAllItemsByItemListId = async () => {
    try {
      const response = await axios.get(
        userItemEndpoints.getAllUsersItemsByListId(itemListId)
      )
      setAllItemsByItemListId(response.data)
      setLoading(true)
    } catch (error) {
      console.error('Error:', error)
    }
  }

  const showAllItems = async () => {
    try {
      const response = await axios.get(itemEndpoints.getAllItems)
      setAllItems(response.data)
    } catch (error) {
      console.error('Error:', error)
    }
  }

  const handleAddClick = (e) => {
    e.preventDefault()
    setShowSelect(true)
    setShowItemAdd(false)
    setShowBack(true)
    setShowAdd(false)
    setShowNewItemForm(false)
    setShowAddNew(true)
  }

  useEffect(() => {
    const currentToken = localStorage.getItem('token')
    if (currentToken) {
      axios.defaults.headers.common['Authorization'] = `Bearer ${currentToken}`
    } else {
      delete axios.defaults.headers.common['Authorization']
    }
    showAllItemsByItemListId()
    showAllItems()
  }, [itemListId])

  const handleBack = () => {
    setShowItemAdd(true)
    setShowSelect(false)
    setShowBack(false)
    setShowAdd(false)
    setShowNewItemForm(false)
    setShowAddNew(false)
  }

  const filterItems = (input) => {
    if (input.length > 2) {
      return allItems
        .filter((item) =>
          item.itemName.toLowerCase().includes(input.toLowerCase())
        )
        .map((item) => ({
          value: item.itemName,
          label: item.itemName,
        }))
    }
  }

  const handleItemsSelect = (selectedOption) => {
    setSelectItem({
      itemName: selectedOption,
    })
    setShowAdd(true)
  }

  const handleAdd = async () => {
    setShowSelect(false)
    setShowItemAdd(true)
    setShowBack(false)
    setShowAdd(true)
    setShowNewItemForm(false)
    setShowAddNew(false)

    const matchingItem = allItems.find(
      (item) =>
        selectedItem.itemName.value.toLowerCase() ===
        item.itemName.toLowerCase()
    )
    if (matchingItem) {
      const userItemDTO = {
        itemListId: itemListId,
        itemId: matchingItem.itemId,
        itemState: 0,
      }
      try {
        await axios.post(userItemEndpoints.addUserItem, userItemDTO)
        setUserItemDTO(userItemDTO)
        setAllItemsByItemListId((prevItems) => [...prevItems, matchingItem])
        showAllItemsByItemListId()
      } catch (error) {
        console.error('Error:', error)
      }
    }
  }

  const handleItemChange = () => {
    showAllItemsByItemListId()
  }

  const renderButtons = () => {
    return (
      <>
        {showAdd && (
          <div className='onlyOneButton'>
            <div id='addBtn'>
              <Button onClick={handleAdd} variant='contained' color='success'>
                Add
              </Button>
            </div>
          </div>
        )}
        <div className='twoButtons'>
          {showBack && (
            <div id='backBtn'>
              <Button onClick={handleBack} variant='contained' color='success'>
                Back
              </Button>
            </div>
          )}
          <div id='addNewItemBtn'>
            {showAddNew && (
              <>
                <p className='modalText'>Can't find any item?</p>
                <Button
                  onClick={handleAddNewItem}
                  variant='contained'
                  color='success'
                >
                  Add New
                </Button>
              </>
            )}
            {showNewItemForm && (
              <div className='inputAndSubmit'>
                <input
                  className='inputNewItem'
                  type='text'
                  value={newItemName}
                  onChange={(e) => setNewItemName(e.target.value)}
                  placeholder='Enter new item name'
                />
                <Button
                  className='submitNewItem'
                  onClick={handleNewItemSubmit}
                  variant='contained'
                  color='success'
                >
                  Submit
                </Button>
              </div>
            )}
          </div>
        </div>
      </>
    )
  }

  return (
    <div className='modalBackground'>
      <div className='modalContainer'>
        <div className='titleCloseBtn'>
          <button onClick={() => closeModal(false)}></button>
        </div>
        <div className='title'>
          <h1>{itemListName}</h1>
        </div>
        <div className='body'>
          {!loading && <Loader />}
          <div className='items-inList'>
            {allItemsByItemListId
              .sort((a, b) => a.userItemId - b.userItemId)
              .map(item => {
                return (
                  <div className='item-container' key={item.userItemId}>
                    <UserItem item={item} onItemChange={handleItemChange} />
                  </div>
                );
              })}
          </div>
        </div>
        <div className='footer'>
          <div className='selectBtn'>
            {showSelect && (
              <Select
                className='map-input'
                defaultValue={inputValue}
                options={filterItems(inputValue)}
                onChange={handleItemsSelect}
                onInputChange={(value) => setInputValue(value)}
                placeholder='Type to search...'
                styles={customStyles}
              />
            )}
          </div>
          <div className='selectButtons'>{!showItemAdd && renderButtons()}</div>
          {showItemAdd && (
            <Button id='AddItemBtn' onClick={handleAddClick}>
              Add
            </Button>
          )}
        </div>
      </div>
    </div>
  )
}

export default ItemListModal