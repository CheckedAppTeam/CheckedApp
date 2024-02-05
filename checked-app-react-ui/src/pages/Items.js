import React, { useEffect, useState } from 'react'
import { itemEndpoints } from '../endpoints'
import axios from 'axios';
import Item from '../Components/Item';

export function Items() {
    const [allItems, setAllItems] = useState(null);
    const [loading, setLoading] = useState(false)

    const getAllItems = async () => {
        try {
            const response = await axios.get(itemEndpoints.getAllItems)
            setAllItems(response.data)
            setLoading(true)
        } catch (e) {
            console.error(e);
        }
    }

    useEffect(() => {
        getAllItems()
    }, [])

    return (
        <div className='allItems'>
            {allItems && allItems.map(item => (
                <h2 key={item.itemId}>
                    <Item item = {item}/>

                </h2>
            ))}
        </div>
    )
}


export default Items