import React, { useEffect, useState } from 'react';
import WeatherChart from './components/WeatherChart';

function App() {
    const dateFrominit = new Date();
    dateFrominit.setDate(dateFrominit.getDate() - 7); //set initial values 
    const dateToInit = new Date();
    dateToInit.setDate(dateToInit.getDate() + 1);  

    const [data, setData] = useState([]);
    const [from, setFrom] = useState(dateFrominit.toISOString().split('T')[0]);
    const [to, setTo] = useState(dateToInit.toISOString().split('T')[0]);

    const fetchData = async () => {
        const params = new URLSearchParams();
        if (from) params.append('from', new Date(from).toISOString());
        if (to) params.append('to', new Date(to).toISOString());

        const res = await fetch(`/api/weather/minMaxTemperature?${params}`);
        const json = await res.json();
        setData(json);
    };

    useEffect(() => { fetchData(); }, []);


    return (
        <div>
            <h2>Weather Data</h2>
            <label>From: <input type="date" value={from} onChange={e => setFrom(e.target.value)} /></label>
            <label>To: <input type="date" value={to} onChange={e => setTo(e.target.value)} /></label>
            <button onClick={fetchData}>Filter</button>
            <WeatherChart data={data} />
        </div>
    );
}

export default App;
