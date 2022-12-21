import axios from 'axios';
import Linechart from 'components/Chart/LineChart/Index';
import PieChart from 'components/Chart/PieChart/Index';
import React, { useEffect, useState } from 'react';
import { Card } from 'react-bootstrap';



function MainPage(props) {

    const [dataStatisticProfit, setDataStatisticProfit] = useState();
    const [dataStatisticProduct, setDataStatisticProduct] = useState();
    const [dataStatisticOrder, setDataStatisticOrder] = useState();
    const [statisticOrder, setStatisticOrder] = useState([]);


    const handleShowAnalytic = (e) => {
        var statisticItem = document.querySelectorAll(".statistic-item");
        [...statisticItem].forEach(item => item.classList.add("d-none"));
        [...statisticItem].forEach(item => {
            if (item.getAttribute("id") === e.target.value) {
                item.classList.remove("d-none")
            }
        })
    }

    const handleAnalyticProfit = () => {
        setDataStatisticProfit({
            labels: ['00:00', '03:00', '06:00', '09:00', '12:00', '15:00', '18:00', '21:00', '24:00'],
            datasets: [
                {
                    label: 'Revenue',
                    data: [100, 1000, 300, 200, 200, 600, 700, 100, 240],
                    borderColor: 'rgb(53, 162, 235)',
                    backgroundColor: 'rgba(53, 162, 235, 0.5)',
                }
            ],
        });
    }

    const handleAnalyticProduct = () => {
        setDataStatisticProduct({
            labels: ['00:00', '03:00', '06:00', '09:00', '12:00', '15:00', '18:00', '21:00', '24:00'],
            datasets: [
                {
                    label: 'Revenue',
                    data: [100, 1000, 300, 200, 200, 600, 700, 100, 240],
                    borderColor: 'rgb(53, 162, 235)',
                    backgroundColor: 'rgba(53, 162, 235, 0.5)',
                }
            ],
        });
    }

    const handleAnalyticOrder = () => {
        axios.get(`${process.env.REACT_APP_API_URL}/Order/GetStatisticOrder`)
            .then(res => {
                console.log(res.data)
                setDataStatisticOrder({
                    labels: res.data.map(item => item.orderType),
                    datasets: [
                        {
                            label: 'Order',
                            data: res.data.map(item => item.quantityOrder),
                            backgroundColor: [
                                'rgba(54, 162, 235, 0.2)',
                                'rgba(255, 206, 86, 0.2)',
                                'rgba(75, 192, 192, 0.2)',
                                'rgba(255, 99, 132, 0.2)',
                            ],
                            borderWidth: 1,
                        },
                    ],
                });
            })

    }

    return (
        <React.Fragment>
            <Card className='py-4'>
                <Card.Body className='p-4'>
                    <h3 className='mb-4'>analytic data</h3>
                    <div className="row justify-content-center container">
                        <div className="col-md-6">
                            <select className="form-control" id="statistic-select" onChange={(e) => handleShowAnalytic(e)}>
                                <option value="">Please select</option>
                                <option value="statistic-profit">Thống kê doanh thu theo thời gian</option>
                                <option value="statistic-order">Thống kê hóa đơn theo trạng thái</option>
                                <option value="statistic-product">Thống kê tổng số sản phẩm bán được theo thời gian</option>
                            </select>
                        </div>
                    </div>

                    <div className="card-body statistic-item d-none" id="statistic-profit">
                        <div className="row">
                            <div className="col-md-4">
                                <p className="mb-1">Thống kê doanh thu theo thời gian</p>
                                <input
                                    className="form-control"
                                    type="month"
                                    name="statistic-profit"
                                    onChange={handleAnalyticProfit}
                                />
                            </div>
                        </div>

                        <div className="row justify-content-center mt-4">
                            <div className="col-8">
                                {
                                    dataStatisticProfit &&
                                    <Linechart
                                        data={dataStatisticProfit}
                                    />
                                }

                            </div>
                        </div>
                    </div>

                    <div className="card-body statistic-item d-none" id="statistic-product">
                        <div className="row">
                            <div className="col-md-4">
                                <p className="mb-1">Thống kê tổng số sản phẩm bán được theo thời gian</p>
                                <input
                                    className="form-control"
                                    type="month"
                                    name="statistic-product"
                                    onChange={handleAnalyticProduct}
                                />
                            </div>
                        </div>

                        <div className="row justify-content-center mt-4">
                            <div className="col-8">
                                {
                                    dataStatisticProduct &&
                                    <Linechart
                                        data={dataStatisticProduct}
                                    />
                                }
                            </div>
                        </div>
                    </div>

                    <div className="card-body statistic-item d-none" id="statistic-order">
                        <div className="row">
                            <div className="col-md-4">
                                <p className="mb-1">Thống kê hóa đơn theo trạng thái</p>
                                <input
                                    className="form-control"
                                    type="month"
                                    name="statistic-order"
                                    onChange={handleAnalyticOrder}
                                />
                            </div>
                        </div>

                        <div className="row justify-content-center mt-4">
                            <div className="col-4">
                                {
                                    dataStatisticOrder &&
                                    <PieChart
                                        data={dataStatisticOrder}
                                    />
                                }

                            </div>
                        </div>
                    </div>
                </Card.Body>
            </Card>

        </React.Fragment>
    )
}


export default MainPage
