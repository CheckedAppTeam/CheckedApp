import React, { useEffect, useState, useRef, useCallback } from 'react'
import { itemEndpoints } from '../endpoints'
import axios from 'axios'
import Item from '../Components/Item'
import Grid from '@mui/material/Grid'
import '../styles/items.css'
import useItemSearch from '../Components/useItemSearch'
// import { entries } from 'core-js/core/array'


export function ItemsByPages() {
//   const [allItems, setAllItems] = useState(null)
//   const [searchBtn, setSearchBtn] = useState(true)
//   const [addBtn, setAddBtn] = useState(true)
//   const [formAdd, setFormAdd] = useState(false)
//   const [fornSearch, setFormSearch] = useState(false)
//   const [newItemName, setNewItemName] = useState('')

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
        error
    } = useItemSearch(query, pageNumb)

    const observer = useRef()
    const lastItemElement = useCallback(node =>{
        if(loading) return
        if(observer.current) observer.current.disconnect()
        observer.current = new IntersectionObserver(entries => {
            if(entries[0].isIntersecting && hasMore) {
                setPageNumb(prevPageNumb => prevPageNumb + 1 )
            }
        })
        if(node) observer.current.observe(node)
        console.log(node)
    }, [loading, hasMore])

  


// const handleItemUpdate = async () => {
//     try {
//       await getAllPages()
//     } catch (e) {
//       console.error(e)
//     }
//   }
// onUpdate={handleItemUpdate}

  return (
<>
<input className="itemInput" type="text" value={query} onChange={handleSearch}></input>
{allItems.map((item, index) => {
{console.log(item.itemName)}
    if(allItems.length === index + 1){
        return <div className='oneItem'ref={lastItemElement} key={item.itemId}>{item.itemName}</div>
    } else {
        return <div className='oneItem' key={item.itemId}>{item.itemName}</div>
    }
    
})}
<div>{loading && "Loading..."}</div>
<div>{error && "Error"}</div>
{/* {allItemsPages &&
            allItemsPages.map(item => (
              <Grid item xs={2} sm={4} md={4} key={item.itemId}>
                <Item item={item}  />
              </Grid>
            ))} */}
        {/* <Grid container spacing={{ xs: 2, md: 3 }} columns={{ xs: 2, sm: 4, md: 8 }}>
          {allItemsPages &&
            allItemsPages.map(item => (
              <Grid item xs={2} sm={4} md={4} key={item.itemId}>
                <Item item={item}  />
              </Grid>
            ))}
        </Grid> */}

</>
        // <Grid container spacing={{ xs: 2, md: 3 }} columns={{ xs: 2, sm: 4, md: 8 }}>
        //   {allItemsPages &&
        //     allItemsPages.map(item => (
        //       <Grid item xs={2} sm={4} md={4} key={item.itemId}>
        //         <Item item={item}  />
        //       </Grid>
        //     ))}
        // </Grid>
  )
}

export default ItemsByPages
