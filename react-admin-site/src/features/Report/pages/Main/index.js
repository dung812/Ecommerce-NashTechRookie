import productApi from 'api/productAPI';
import axios from 'axios';
import { fetchProducts } from 'features/Product/ProductSlice';
import ReportCustomer from 'features/Report/components/ReportCustomer';
import ReportIncome from 'features/Report/components/ReportIncome';
import ReportProduct from 'features/Report/components/ReportProduct';
import React, { useEffect, useState } from 'react';
import { Card, Tabs, Tab } from 'react-bootstrap';
import { useDispatch, useSelector } from 'react-redux';

const MainPage = () => {
    const dispatch = useDispatch();
    const [key, setKey] = useState('product');
    let products = useSelector((state) => state.products.products);
    const [productList, setProductList] = useState();

    const [customerList, setCustomerList] = useState();

    useEffect(() => {
        var list = products.slice().sort((a, b) => b.income - a.income).filter((element, index) => index < 10);
        var convertList = list.map(item => {
            return {
                productId: item.productId,
                productName: item.productName,
                income: item.income,
            }
        });
        setProductList(convertList);
    }, [products])

    useEffect(() => {
        dispatch(fetchProducts());
        axios.get(process.env.REACT_APP_API_URL + '/Statistic/ReportCustomer')
            .then(res => setCustomerList(res.data))
    }, [])


    const columnsProduct = [
        {
            name: "Product Id",
            selector: (row) => row.productId,
            sortable: true,
        },
        {
            name: "Name",
            selector: (row) => row.productName,
            sortable: true,
        },
        {
            name: 'Income',
            selector: row => `$${row.income}`,
            sortable: true,
        },
    ];
    const columnsCustomer = [
        {
            name: "Full name",
            selector: (row) => row.fullName,
            sortable: true,
        },
        {
            name: "Total Order Success",
            selector: (row) => row.totalOrderSuccess,
            sortable: true,
        },
        {
            name: 'Total Order Cancelled',
            selector: row => `$${row.totalOrderCancelled}`,
            sortable: true,
        },
        {
            name: 'Total Order Waiting',
            selector: row => `$${row.totalOrderWaiting}`,
            sortable: true,
        },
        {
            name: 'Total Money Purchased',
            selector: row => `$${row.totalMoneyPurchased}`,
            sortable: true,
        },
    ];

    return (
        <React.Fragment>
            <Card>
                <Card.Body>
                    <h3 className='mb-4'>Report</h3>

                    <Tabs
                        id="controlled-tab-example"
                        activeKey={key}
                        onSelect={(k) => setKey(k)}
                        className="mb-3"
                    >
                        <Tab eventKey="product" title="Product">
                            <ReportProduct
                                data={productList}
                                columns={columnsProduct}
                            />
                        </Tab>
                        <Tab eventKey="customer" title="Customer">
                            <ReportCustomer
                                data={customerList}
                                columns={columnsCustomer}
                            />
                        </Tab>
                        <Tab eventKey="income" title="Income">
                            {/* <ReportIncome
                                
                            /> */}
                            <h1 className='text-center'>Income</h1>
                            <p className='text-center'>Total money earned in the last 30 days: <span className='text-danger'>$800.000</span></p>
                        </Tab>

                    </Tabs>
                </Card.Body>
            </Card>
        </React.Fragment>
    );
};

export default MainPage;
