import React, { useEffect, useState } from 'react';
import { BrowserRouter as Router, Routes, Route, Link } from 'react-router-dom';
import Logs from './components/Logs';
import WeatherChartPage from './components/WeatherChartPage';
export default function App() {
    return(
    <Router>
        <nav>
            <Link to="/Logs">Logs</Link> | <Link to="/weatherChartPage">Chart</Link>
        </nav>
        <Routes>
            <Route path="/logs" element={<Logs />} />
            <Route path="/weatherChartPage" element={<WeatherChartPage />} />
        </Routes>
        </Router>
    )
}

