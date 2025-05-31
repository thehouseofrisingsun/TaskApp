import React, { useEffect, useState } from 'react';

export default function LogsPage() {
    const [logs, setLogs] = useState([]);

    useEffect(() => {
        fetch('/api/logs/logs') 
            .then(res => res.json())
            .then(setLogs)
            .catch(err => console.error('Failed to load logs', err));
    }, []);

    return (
        <div>
            <h2>Logs</h2>
            <ul>
                {logs.map((log, index) => (
                    <li key={index}>
                        {log.date} - {log.statusCode} - {log.requestUri}
                    </li>
                ))}
            </ul>
        </div>
    );
}

