import React from 'react';
import { Bar } from 'react-chartjs-2';
import 'chart.js/auto';

export default function WeatherChart({ data }) {
    //get latest record per city
    const latestPerCity = [];
    const seen = new Set();

    //  get the latest record
    [...data].reverse().forEach(item => {
        if (!seen.has(item.city)) {
            latestPerCity.push(item);
            seen.add(item.city);
        }
    });
    console.log(latestPerCity)
    const cities = latestPerCity.map(d => d.city);
    const minTemps = latestPerCity.map(d => d.minTemperature);
    const maxTemps = latestPerCity.map(d => d.maxTemperature);
    const minLastUpdate = latestPerCity.map(d => d.minTemperatureLastUpdate);
    const maxLastUpdate = latestPerCity.map(d => d.maxTemperatureLastUpdate);


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

