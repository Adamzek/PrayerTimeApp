import { useEffect, useState } from 'react'
import './App.css'

function App() {
    const [prayerTimes, setPrayerTimes] = useState([]);

    useEffect(() => {
        fetch("http://localhost:6969/api/prayertimes?zone=SGR01")
            .then(response => response.json())
            .then(data => setPrayerTimes(data.prayerTime))
            .catch(error => console.error(error));
    }, []); //dependancy array


  return (
    <div>
          <h1>Prayer Times</h1>
          <ul>
              {prayerTimes.map((p, index) => (  //.map() iterates over all elements in array
                  <li key={index}>
                      {p.date} , { p.fajr }
                  </li>
              ))}
          </ul>
    </div>
  )
}

export default App
