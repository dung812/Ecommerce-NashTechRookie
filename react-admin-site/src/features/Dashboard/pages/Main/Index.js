import './Main.scss'
import React, { useState, useEffect, useRef } from 'react';
import axios from 'axios';
import { Card } from 'react-bootstrap';
import DataTable from 'react-data-table-component';
import { NavLink } from 'react-router-dom';

import Linechart from 'components/Chart/LineChart/Index';
import PieChart from 'components/Chart/PieChart/Index';
import CardStatistic from 'features/Dashboard/components/CardStatistic/Index';



function MainPage(props) {

    const [recentOrders, setRecentOrders] = useState([]);
    const [statisticOrder, setStatisticOrder] = useState([]);
    const [cardStatisticData, setCardStatisticData] = useState([]);

    useEffect(() => {
        axios.get(`${process.env.REACT_APP_API_URL}/Order/GetRecentOrders`)
            .then(res => setRecentOrders(res.data))
        axios.get(`${process.env.REACT_APP_API_URL}/Order/GetStatisticOrder`)
            .then(res => setStatisticOrder(res.data))
        axios.get(`${process.env.REACT_APP_API_URL}/Statistic/GetStatisticCardNumber`)
            .then(res => setCardStatisticData(res.data))
    }, [])

    function FormatDateTime(datetime)
    {
        let date = `${datetime.split("T")[0].split("-")[2]}-${datetime.split("T")[0].split("-")[1]}-${datetime.split("T")[0].split("-")[0]}`;
        let time = `${datetime.split("T")[1].split(":")[0]}:${datetime.split("T")[1].split(":")[1]}`;
        return `${date} ${time}`;
    }

    function FormatStatusOrder(orderStatus) {
        let result;
        if (orderStatus === 1)
            result = `New Order`
        else if (orderStatus === 2)
            result = `Waiting Delivery`
        else if (orderStatus === 3)
            result = `Delivered`
        else if (orderStatus === 4)
            result = `Cancelled`

        return result;
    }

    const dataRevenueLineChart = {
        labels: ['00:00', '03:00', '06:00', '09:00', '12:00', '15:00', '18:00', '21:00', '24:00'],
        datasets: [
            {
                label: 'Revenue',
                data: [100, 1000, 300, 200, 200, 600, 700, 100, 240],
                borderColor: 'rgb(53, 162, 235)',
                backgroundColor: 'rgba(53, 162, 235, 0.5)',
            }
        ],
    };

    const dataOrderPieChart = {
        labels: statisticOrder.map(item => item.orderType),
        datasets: [
            {
                label: 'Order',
                data: statisticOrder.map(item => item.quantityOrder),
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

    const columns = [
        {
            name: 'Order Id',
            selector: row => row.orderId,
        },
        {
            name: "Status",
            cell: (row) => (
                <div>
                    {row.orderStatus === 1 ? <span className="badge bg-info">{FormatStatusOrder(row.orderStatus)}</span> : ""}
                    {row.orderStatus === 2 ? <span className="badge bg-warning"><i className='bx bx-time-five'></i> {FormatStatusOrder(row.orderStatus)}</span> : ""}
                    {row.orderStatus === 3 ? <span className="badge bg-success"><i className='bx bx-money'></i> {FormatStatusOrder(row.orderStatus)}</span> : ""}
                    {row.orderStatus === 4 ? <span className="badge bg-danger"><i className='bx bx-x'></i> {FormatStatusOrder(row.orderStatus)}</span> : ""}
                </div>
            )
        },
        {
            name: 'Date',
            selector: row => FormatDateTime(row.orderDate),
            sortable: true,
        },
        {
            name: 'Name',
            selector: row => row.orderName,
            sortable: true,
        },
        {
            name: 'Ship To',
            selector: row => row.address,
        },
        {
            name: 'Payment Method',
            selector: row => row.paymentName,
        },
        {
            name: 'Sale Amount',
            selector: row => `$${row.totalMoney}`,
            sortable: true,
        },
    ];

    return (
        <React.Fragment>
            <h5 className='mb-4'>Dashboard</h5>

            <div className="row mb-3">
                {
                    cardStatisticData.map((item, index) => {
                        return (
                            <div className='col-md-3' key={index}>
                                <CardStatistic
                                    title={item.title}
                                    number={item.number}
                                    isUp={item.isUp}
                                    percent={item.percent}
                                    prefix={item.prefix}
                                    suffix={item.suffix}
                                />
                            </div>
                        )
                    })
                }

            </div>

            <div className="row mb-3">
                <div className="col-md-9">
                    <Card>
                        <Card.Body>
                            <h4 className='mb-2 text-center'>Revenue Today</h4>
                            <Linechart
                                data={dataRevenueLineChart}
                            />
                        </Card.Body>
                    </Card>
                </div>
                <div className="col-md-3">
                    <Card>
                        <Card.Body>
                            <h4 className='mb-2 text-center'>Order Statistic</h4>
                            <PieChart
                                data={dataOrderPieChart}
                            />
                        </Card.Body>
                    </Card>
                </div>
            </div>

            <div className="row mb-3">
                <div className="col-md-12">
                    <Card>
                        <Card.Body>
                            <h4 className='mb-2 text-left'>Recent Orders</h4>
                            <DataTable
                                columns={columns}
                                data={recentOrders}
                                className='mb-2'
                            />
                            <NavLink to="/order">See more orders</NavLink>
                        </Card.Body>
                    </Card>
                </div>
            </div>

        </React.Fragment>
    )
}


export default MainPage
