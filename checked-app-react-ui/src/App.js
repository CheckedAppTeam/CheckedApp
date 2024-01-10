
//import axios from 'axios';
import { useEffect, useState } from 'react';
import './App.css';
import { axios, itemEndpoints, userEndpoints } from './endpoints';

function App() {
  const [itemResponseData, setItemResponseData] = useState(null);
  const [allItemsResponseData, setAllitemsResponseData] = useState(null);

  useEffect(()=>{
    axios.get(itemEndpoints.getAllItems)
    .then(response=>{
      setItemResponseData(response.data);
      
    }).catch(error => {
      console.log(error);
    });
    const userId = 1; // Replace with the actual user ID
    axios.get(userEndpoints.getUserData(userId))
      .then(response => {
        setAllitemsResponseData(response.data);
      })
      .catch(error => {
        console.log(error);
      });
  }, [])
  
  
  return (
    <div className="App">
      My react app
      <p>
        Communicatin'
      </p>
      
        <div>
            <pre>
              
              <h2>{JSON.stringify(itemResponseData,null,2)}</h2>
              <p>{JSON.stringify(allItemsResponseData,null,2)}</p>
            </pre>
        </div>
      )
    </div>
  );
}

export default App;
