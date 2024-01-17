// import { useOutletContext, useParams } from 'react-router-dom'

// export function ItemList() {
//   const { id } = useParams()
//   const obj = useOutletContext()
//   return (
//     <h1>
//       ItemList {id} {obj.hello}
//     </h1>
//   )
// }
import { useState, useEffect } from 'react';
import axios from 'axios';
import { itemListEndpoints, userItemEndpoints } from '../endpoints';
// import '../styles/itemList.css';
import { Link } from 'react-router-dom';

export function ItemList({ match }) {
  const itemListId = match?.params?.itemListId; 
  // const { itemListId } = match.params;
  const [items, setItems] = useState([]);

  useEffect(() => {
    if (itemListId) {
      // Fetch items based on the itemListId parameter
      axios
        .get(userItemEndpoints.getAllUsersItemsByListId(itemListId))
        .then((response) => {
          setItems(response.data);
        })
        .catch((error) => {
          console.log(error);
        });
    }
  }, [itemListId]);


  return (
    <div className="item-list-container">
      <h2>Items for List: {itemListId}</h2>
      {items.map((item, index) => (
      <div className = 'item' key={index}>
            <Link to={`/itemlists/${item.itemListId}`}>
      {item.listName}
    </Link>
      {/* <a href={`/itemLists/${item.listName}`}>
        {item.listName}
      </a> */}
      <p>{item.travelDestination}</p>
      <p>{formatDate(item.travelDate)}</p>
      {item.isPublic ? <p className="public">public</p> : <p className="private">private</p>}
    </div>
      ))}
    </div>
  );
}

function formatDate(dateString) {
  const date = new Date(dateString);
  const formattedDate = date.toISOString().split('T')[0];
  return formattedDate;
}