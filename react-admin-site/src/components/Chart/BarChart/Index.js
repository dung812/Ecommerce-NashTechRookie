import React from 'react';
import {
    Chart as ChartJS,
    CategoryScale,
    LinearScale,
    BarElement,
    Title,
    Tooltip,
    Legend,
} from 'chart.js';
import { Bar } from 'react-chartjs-2';

ChartJS.register(
    CategoryScale,
    LinearScale,
    BarElement,
    Title,
    Tooltip,
    Legend
);

const options = {
    responsive: true,
    plugins: {
        legend: {
            display: false,
        },
        title: {
            display: false,
        },
    },
};


const BarChart = (props) => {
    return (
        <React.Fragment>
            <Bar 
                options={options} 
                data={props.data} 
                redraw
                height="90%"
            />
        </React.Fragment>
    );
};



export default BarChart;
