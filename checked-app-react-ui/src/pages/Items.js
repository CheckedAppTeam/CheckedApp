import React, { useEffect, useState } from 'react'
import { itemEndpoints } from '../endpoints'
import axios from 'axios'
import Item from '../Components/Item'
import Grid from '@mui/material/Grid'
import '../styles/items.css'

export function Items() {
  const [allItems, setAllItems] = useState(null)
  const [searchBtn, setSearchBtn] = useState(true)
  const [addBtn, setAddBtn] = useState(true)
  const [formAdd, setFormAdd] = useState(false)
  const [fornSearch, setFormSearch] = useState(false)
  const [newItemName, setNewItemName] = useState('')

  const [originalItems, setOriginalItems] = useState([]);

  // useEffect(() => {
  //   getAllItems();
  // }, []);


  const getAllItems = async () => {
    try {
      const response = await axios.get(itemEndpoints.getAllItems)
      setAllItems(response.data)
    } catch (e) {
      console.error(e)
    }
  }

  const alternateCase = (word) => {
    return word
      .split('')
      .map((char, index) => {
        return index === 0 ? char.toUpperCase() : char.toLowerCase()
      })
      .join('')
  }

  useEffect(() => {
    const storedToken = localStorage.getItem('token');
    if (storedToken) {
      axios.defaults.headers.common['Authorization'] = `Bearer ${storedToken}`;
    }
    getAllItems()
  }, [])

  // useEffect(() => {
  //   setOriginalItems(allItems);
  // }, [allItems]);

  // const handleOkSearch = () => {
  //   const filteredItems = originalItems.filter(item =>
  //     item.itemName.toLowerCase().includes(newItemName.toLowerCase())
  //   );
  //   setAllItems(filteredItems);
  // }

  const handleItemUpdate = async () => {
    try {
      await getAllItems()
    } catch (e) {
      console.error(e)
    }
  }

  const handleAddItem = () => {
    setAddBtn(false)
    setSearchBtn(false)
    setFormAdd(true)

  }

  const handleSearchItem = () => {
    setSearchBtn(false)
    setAddBtn(false)
    setFormSearch(true)
  }

  const handleOkAdd = async () => {
    try {
      const addedItem = { itemName: alternateCase(newItemName) };
      const response = await axios.post(itemEndpoints.addItem, addedItem);
      console.log(response)
      await getAllItems();
      setNewItemName('');
      handleCancel();
    } catch (error) {
      console.error(error);
    }
  }

  const handleOkSearch = (e) => {
    console.log(e)
    const filteredItems = allItems.filter(item =>
      item.itemName.toLowerCase().includes(e.toLowerCase())
    );
    setAllItems(filteredItems);
  }


  const handleCancel = () => {
    setAddBtn(true)
    setSearchBtn(true)
    setFormAdd(false)
    setFormSearch(false)
    getAllItems()
  }

  useEffect(() => {
    setOriginalItems(allItems);
  }, [allItems]);

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
                <input className='addItemInput'></input>
                <button className='submitAdd' onClick={handleOkAdd}>Ok</button>
                <button className='cancelAdd' onClick={handleCancel}>X</button>
              </div>
            </form>
          </>
        }
        {fornSearch &&
          <>
            <form className='addForm'>
              <label className='nameLabelSearch'>Search Item</label>
              <div className='buttonsForm'>
                <input
                  className='addItemInput'
                  value={newItemName}
                  onChange={(e) => {
                    setNewItemName(e.target.value);
                    handleOkSearch(e.target.value);
                  }}
                />
                <button className='cancelAdd' onClick={handleCancel}>X</button>
              </div>
            </form>
          </>
        }

        <Grid container spacing={{ xs: 2, md: 3 }} columns={{ xs: 2, sm: 4, md: 8 }}>
          {allItems &&
            allItems.map(item => (
              <Grid item xs={2} sm={4} md={4} key={item.itemId}>
                <Item item={item} onUpdate={handleItemUpdate} />
              </Grid>
            ))}
        </Grid>
      </div>
    </div>
  )
}

export default Items
