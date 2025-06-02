import React from 'react';
import { Bar } from 'react-chartjs-2';
import 'chart.js/auto';

export default function WeatherChart({ data }) {
    const records = [];

    [...data].forEach(item => {
            records.push(item);
    });
    console.log(records)
    const cities = records.map(d => d.city);
    const minTemps = records.map(d => d.minTemperature);
    const maxTemps = records.map(d => d.maxTemperature);
    const minLastUpdate = records.map(d => d.minTemperatureLastUpdate);
    const maxLastUpdate = records.map(d => d.maxTemperatureLastUpdate);


    const chartData = {
        labels: cities,
        datasets: [
            {
                label: 'Min Temperature',
                data: minTemps,
                backgroundColor: 'blue',
                extra: minLastUpdate
            },
            {
                label: 'Max Temperature',
                data: maxTemps,
                backgroundColor: 'red',
                extra: maxLastUpdate
            }
        ]
    };

    const options = {
        responsive: true,
        maintainAspectRatio: false,
        plugins: {
            legend: {
                position: 'bottom'
            },
            tooltip: {
                mode: 'index',
                intersect: false
            },
            tooltip: {
                callbacks: {
                    label: function (context) {
                        const label = context.dataset.label || '';
                        const value = context.parsed.y;
                        const date = context.dataset.extra[context.dataIndex];
                        const formattedDate = new Date(date).toLocaleString();
                        return `${label}: ${value}\u00B0C (on ${formattedDate})`;
                    }
                }
            }
        },
        scales: {
            x: {
                title: { display: true, text: 'City' }
            },
            y: {
                beginAtZero: true,
                title: { display: true, text: 'Temperature (\u00B0C)' }
            }
        }
    };

    return (
        <div>
            <h3>Latest Min/Max Temperatures by City</h3>
            <div style={{ height: '500px' }}>
                <Bar data={chartData} options={options} />
            </div>
        </div>
    );
}

