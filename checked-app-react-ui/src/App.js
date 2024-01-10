
import axios from 'axios';
import { useEffect, useState } from 'react';
import './App.css';
import { urlGetAllItems } from './endpoints';

function App() {
  const [responseData, setResponseData] = useState(null);

  useEffect(()=>{
    axios.get(urlGetAllItems)
    .then(response=>{
      setResponseData(response.data);
      
    }).catch(error => {
      console.log(error);
    })
  }, [])
  
  
  return (
    <div className="App">
      My react app
      <p>
        Communicatin'
      </p>
      {responseData&&(
        <div>
          <h2>
            <pre>
              {JSON.stringify(responseData,null,2)}
            </pre>
          </h2>
        </div>
      )}
    </div>
  );
}

export default App;
