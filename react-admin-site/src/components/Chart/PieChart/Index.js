import React from 'react';
import { Chart as ChartJS, ArcElement, Tooltip, Legend } from 'chart.js';
import { Pie } from 'react-chartjs-2';

ChartJS.register(ArcElement, Tooltip, Legend);



const PieChart = (props) => {
    const { data } = props
    return (
        <React.Fragment>
            <Pie 
                data={data} 
                redraw
            />
        </React.Fragment>
    );
};

export default PieChart;
