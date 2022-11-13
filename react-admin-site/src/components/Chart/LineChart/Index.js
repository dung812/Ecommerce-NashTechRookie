import React from 'react';
import {
    Chart as ChartJS,
    CategoryScale,
    LinearScale,
    PointElement,
    LineElement,
    Title,
    Tooltip,
    Legend,
} from 'chart.js';
import { Line } from 'react-chartjs-2';


ChartJS.register(
    CategoryScale,
    LinearScale,
    PointElement,
    LineElement,
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

const Linechart = (props) => {
    const { data } = props

    return (
        <React.Fragment>
            <Line 
                options={options} 
                data={data} 
                redraw
                height="90%"
            />
        </React.Fragment>
    );
};


export default Linechart;
