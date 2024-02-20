import React, { useEffect, useState, useRef, useCallback } from 'react'
import { itemEndpoints } from '../endpoints'
import axios from 'axios'
import Item from '../Components/Item'
import Grid from '@mui/material/Grid'
import '../styles/items.css'
import useItemSearch from '../Components/useItemSearch'
import Loader from '../spinners/Loader'


export function ItemsByPages() {
  const [addBtn, setAddBtn] = useState(true)
  const [formAdd, setFormAdd] = useState(false)
  const [searchBtn, setSearchBtn] = useState(true)
  const [fornSearch, setFormSearch] = useState(false)
  const [newItemName, setNewItemName] = useState('')


  const [query, setQuery] = useState('')
  const [pageNumb, setPageNumb] = useState(1)

  function handleSearch(e) {
    setQuery(e.target.value)
    setPageNumb(1)
  }

  const {
    allItems,
    hasMore,
    loading,
    error,
    setAllItems
  } = useItemSearch(query, pageNumb)


  const observer = useRef()
  const lastItemElement = useCallback(node => {
    if (loading) return
    if (observer.current) observer.current.disconnect()
    observer.current = new IntersectionObserver(entries => {
      if (entries[0].isIntersecting && hasMore) {
        setPageNumb(prevPageNumb => prevPageNumb + 1)
      }
    })
    if (node) observer.current.observe(node)
    console.log(node)
  }, [loading, hasMore])

  const handleDeleteItem = async (itemId) => {
    setAllItems(prevItems => prevItems.filter(item => item.itemId !== itemId));
  };

  const alternateCase = (word) => {
    return word
      .split('')
      .map((char, index) => {
        return index === 0 ? char.toUpperCase() : char.toLowerCase()
      })
      .join('')
  }

  const handleOkAdd = async () => {
    try {
      const addedItem = { itemName: alternateCase(newItemName) };
      const response = await axios.post(itemEndpoints.addItem, addedItem);
      console.log(response)
      setAllItems(prevItems => [response.data, ...prevItems]);
      setNewItemName('');
      handleCancel();
    } catch (error) {
      console.error(error);
    }
  }

  const handleAddItem = () => {
    setAddBtn(false)
    setFormAdd(true)
    setSearchBtn(false)
  }

  const handleSearchItem = () => {
    setSearchBtn(false)
    setAddBtn(false)
    setFormSearch(true)
  }

  const handleCancel = () => {
    setAddBtn(true)
    setFormAdd(false)
    setSearchBtn(true)
  }

  const handleCancelSearch = () => {
    setAddBtn(true)
    setFormSearch(false)
    setSearchBtn(true)
    setQuery('');
  }

  const handleChange = (itemId, newValue) => {
    setAllItems((prevItems) =>
      prevItems.map((item) =>
        item.itemId === itemId ? { ...item, itemName: newValue } : item
      )
    );
  };

  return (

    <div className='allItems'>
      <div className='body'>
        <h1>All Items</h1>
        <div className='itemsButtons'>
          {addBtn &&
            <div className='addItemButton'>
              <button onClick={handleAddItem}>Add</button>
            </div>
          }
          {searchBtn &&
            <div className='searchItemButton'>
              <button onClick={handleSearchItem}>Search</button>
            </div>
          }
        </div>
        {formAdd &&
          <>
            <form className='addForm'>
              <label className='nameLabel'>Add Item Name</label>
              <div className='buttonsForm'>
                <input
                  className='addItemInput'
                  value={newItemName}
                  onChange={(e) => setNewItemName(e.target.value)}
                />
                <button className='submitAdd' onClick={handleOkAdd}>Ok</button>
                <button className='cancelAdd' onClick={handleCancel}>X</button>
              </div>
            </form>
          </>
        }
                {fornSearch &&
          <>
            <form className='addForm'>
              <label className='nameLabelSearch'>Find Item</label>
              <div className='buttonsForm'>
              <input className="searchItemInput" type="text" value={query} onChange={handleSearch}></input>
                <button className='cancelAdd' onClick={handleCancelSearch}>X</button>
              </div>
            </form>
          </>
        }
        {/* <div className='inputform'>
          <div className='nameLabelSearch'>Find item</div>
          <input className="searchItemInput" type="text" value={query} onChange={handleSearch}></input>
        </div> */}

        {allItems.map((item, index) => {
          if (allItems.length === index + 1) {
            return (
              <div className='oneItem' key={item.itemId} ref={lastItemElement}>
                <Item item={item} change={handleChange} onDelete={handleDeleteItem} />
              </div>
            );
          } else {
            return (
              <div className='oneItem' key={item.itemId}>
                <Item item={item} change={handleChange} onDelete={handleDeleteItem} />
              </div>
            );
          }
        })}

        {loading && <Loader />}
        <div>{error && "Error"}</div>
      </div>
    </div>

  )
}

export default ItemsByPages
