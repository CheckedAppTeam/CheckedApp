import axios from 'axios'
import { useEffect, useState } from 'react'
import { itemEndpoints } from '../endpoints'

export default function useItemSearch(query, pageNumb) {
    const [loading, setLoading] = useState(true)
    const [error, setError] = useState(false)
    const [allItems, setAllItems] = useState([])
    const [hasMore, setHasMore] = useState(false)

    useEffect(() => {
        setAllItems([])
    }, [query])

    useEffect(() => {
    
        setLoading(true)
        setError(false)
        let cancel
        axios.get(itemEndpoints.getAllPages, {
            params: {
                SearchPharse: query,
                PageNumber: pageNumb,
                PageSize: 10
            },
            cancelToken: new axios.CancelToken(c => cancel = c)
        }).then(res => {
            setAllItems(prevItems => {
                return [...prevItems, ...res.data.items]
            })
            setHasMore(res.data.items.length === 10)
            setLoading(false)
            console.log(res.data)
        }).catch(e => {
            if (axios.isCancel(e)) return
            setError(true)
        })
        return () => cancel()
    }, [query, pageNumb])

    return { loading, error, allItems, hasMore }
}
