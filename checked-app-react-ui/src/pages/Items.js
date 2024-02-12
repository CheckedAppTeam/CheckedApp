import React, { useEffect, useState } from 'react'
import { itemEndpoints } from '../endpoints'
import axios from 'axios'
import Item from '../Components/Item'
import Grid from '@mui/material/Grid'
import '../styles/items.css'

export function Items() {
  const [allItems, setAllItems] = useState(null)

  const getAllItems = async () => {
    try {
      const response = await axios.get(itemEndpoints.getAllItems)
      setAllItems(response.data)
    } catch (e) {
      console.error(e)
    }
  }

  useEffect(() => {
    getAllItems()
  }, [])

  const handleItemUpdate = async () => {
    try {
      await getAllItems()
    } catch (e) {
      console.error(e)
    }
  }

  return (
    <div className='allItems'>
      <div className='body'>
        <h1>All Items</h1>
        <div className='AddItemButton'>
          <button>Add</button>
        </div>
        <Grid container spacing={{ xs: 2, md: 3 }} columns={{ xs: 2, sm: 4, md: 8 }}>
          {allItems &&
            allItems.map(item => (
              <Grid xs={2} sm={4} md={4} key={item.ItemId}>
                <Item key={item.ItemId} item={item} onUpdate={handleItemUpdate}>xs=2</Item>
              </Grid>
            ))}
        </Grid>
      </div>
    </div>
  )
}

export default Items
