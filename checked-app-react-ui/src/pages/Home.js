import Map from '../Components/Map/Map'
import axios from "axios";
import React, { useState, useEffect } from "react";

export function Home() {
  // const [data, setData] = useState("default");

  useEffect(() => {
   const token = localStorage.getItem("token");

    if (token) {
      axios.defaults.headers.common["Authorization"] = `Bearer ${token}`;
    }
    // if (data == "default") {
    //   axios
    //     .get("https://localhost:7000/api/resources")
    //     .then((response) => {
    //       const data = response.data;

    //       setData(data);
    //     })
    //     .catch((err) => console.log(err));
    // }
  });

  return (
    <div className='map-container'>
      <h1>Home</h1>
      <Map />
    </div>
  )
}
