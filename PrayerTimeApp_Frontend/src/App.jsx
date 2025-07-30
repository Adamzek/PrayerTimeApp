import { useEffect, useState } from 'react';
import ZoneSelector from './Components/ZoneSelector';
import PrayerTimesTable from './Components/PrayerTimesTable';
import CurrentTime from  './Components/CurrentTime';
import NextPrayer from  './Components/NextPrayer';
import './App.css';

function App() {
    const [prayerTimes, setPrayerTimes] = useState([]);
    const [loading, setLoading] = useState(true);
    const [zones, setZones] = useState([]);
    const [selectedZone, setSelectedZone] = useState("SGR01");

    // Fetch available zones for dropdown
    useEffect(() => {
        fetch("http://localhost:6969/api/prayertimes/zones")
            .then(res => res.json())
            .then(data => setZones(data))
            .catch(error => console.error(error));
    }, []);

    // Fetch prayer times when selectedZone changes
    useEffect(() => {
        setLoading(true);
        fetch(`http://localhost:6969/api/prayertimes?zone=${selectedZone}`) //data.prayerTime is the array of prayer times 
            .then(res => res.json())
            .then(data => setPrayerTimes(data.prayerTime))
            .catch(error => console.error(error))
            .finally(() => setLoading(false));
    }, [selectedZone]); //dependancy array

    if (loading) return <p className="text-center mt-4">Loading prayer times...</p>;

    return (
        <div className="bg-black max-w-5xl mx-auto mt-10 p-4 rounded-2xl text-white">
            <h1 className="text-2xl font-bold text-center mb-6">Prayer Times</h1>
            <CurrentTime className="text-white text-lg mb-4">Current Time: </CurrentTime>
            <NextPrayer prayerTimes={prayerTimes} />
            <ZoneSelector
                zones={zones}
                selectedZone={selectedZone}
                onZoneChange={setSelectedZone}
            />
            <div className="overflow-x-auto shadow-md rounded-lg">
                <PrayerTimesTable prayerTimes={prayerTimes} />
            </div>
        </div>
    );
}

export default App;
