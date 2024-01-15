import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { itemListEndpoints } from '../endpoints';

export function ItemListAxios(itemListId, userId) {
  const [allItemListsResponseData, setAllItemListsResponseData] = useState(null);
  const [itemListsByIdResponseData, setItemListsByIdResponseData] = useState(null);
  const [itemListsByUserIdResponseData, setItemListsByUserIdResponseData] = useState(null);

  useEffect(() => {
    // Fetch all item lists
    axios
      .get(itemListEndpoints.getAllLists)
      .then((response) => {
        setAllItemListsResponseData(response.data);
      })
      .catch((error) => {
        console.log(error);
      });

    axios
      .get(itemListEndpoints.findById(itemListId))
      .then((response) => {
        setItemListsByIdResponseData(response.data);
      })
      .catch((error) => {
        console.log(error);
      });

    axios
    .get(itemListEndpoints.getByUserId(userId))
    .then((response) => {
        setItemListsByUserIdResponseData(response.date);
    })
    .catch((error) => {
        console.log(error);
    })
  }, []);

  // You can return an object with both responses or the one you need
  return {
    allItemLists: allItemListsResponseData,
    itemListsById: itemListsByIdResponseData,
    itemListsByUserId : itemListsByUserIdResponseData
  };
}
