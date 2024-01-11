import { useState, useEffect } from "react";
import axios from "axios";
import { itemListEndpoints } from "../endpoints";
import { userEndpoints } from "../endpoints";

export function ItemLists() {
    const [itemListResponseData, setItemListResponseData] = useState(null);
    const [allItemListsResponseData, setAllitemListsResponseData] = useState(null);

  useEffect(()=>{
    axios.get(itemListEndpoints.getAllList)
    .then(response=>{
      setItemListResponseData(response.data);
      
    }).catch(error => {
      console.log(error);
    });
    const userId = 1; // Replace with the actual user ID
    axios.get(userEndpoints.getUserData(userId))
      .then(response => {
        setAllitemListsResponseData(response.data);
      })
      .catch(error => {
        console.log(error);
      });
  }, [])

    return (
    <>
        <h1>Item Lists</h1>
        <div>
            <pre>
              {/* <h2>{JSON.stringify(itemListResponseData,null,2)}</h2> */}
              <p>{JSON.stringify(allItemListsResponseData,null,2)}</p>
            </pre>
        </div>
    </>
    )
}