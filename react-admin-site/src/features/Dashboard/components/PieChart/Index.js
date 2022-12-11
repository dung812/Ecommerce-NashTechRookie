import React from 'react';
import { Chart as ChartJS, ArcElement, Tooltip, Legend } from 'chart.js';
import { Pie } from 'react-chartjs-2';

ChartJS.register(ArcElement, Tooltip, Legend);



const PieChart = (props) => {
    const { data } = props
    
    const dataPieChart = {
        labels: data.map(item => item.orderType),
        datasets: [
            {
                label: 'Order',
                data: data.map(item => item.quantityOrder),
                backgroundColor: [
                    'rgba(54, 162, 235, 0.2)',
                    'rgba(255, 206, 86, 0.2)',
                    'rgba(75, 192, 192, 0.2)',
                    'rgba(255, 99, 132, 0.2)',
                ],
                borderWidth: 1,
            },
        ],
    };

    return (
        <React.Fragment>
            <Pie
                data={dataPieChart}
                redraw
            />
        </React.Fragment>
    );
};

export default PieChart;
