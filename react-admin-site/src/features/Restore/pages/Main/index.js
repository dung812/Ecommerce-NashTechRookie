import React, { useEffect, useState } from 'react';
import { Card, Tab, Tabs } from 'react-bootstrap';
import Product from 'features/Restore/components/Product';
import Customer from 'features/Restore/components/Customer';


function MainPage(props) {

    const [key, setKey] = useState('product');

    return (
        <React.Fragment>
            <Card>
                <Card.Body className='p-4'>
                    <h3 className='mb-4'>Restore - Delete data</h3>
                    <Tabs
                        id="controlled-tab-example"
                        activeKey={key}
                        onSelect={(k) => setKey(k)}
                        className="mb-3"
                    >
                        <Tab eventKey="product" title="Product">
                            <Product />
                        </Tab>
                        <Tab eventKey="category" title="Category">
                            Category
                        </Tab>
                        <Tab eventKey="manufacture" title="Manufacture">
                            Manufacture
                        </Tab>
                        <Tab eventKey="customer" title="Customer">
                            <Customer/>
                        </Tab>
                        <Tab eventKey="admin" title="Admin">
                            Admin
                        </Tab>
                        <Tab eventKey="order" title="Order">
                            Order
                        </Tab>
                    </Tabs>
                </Card.Body>
            </Card>
        </React.Fragment>
    )
}


export default MainPage
