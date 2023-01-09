import ReportCustomer from 'features/Report/components/ReportCustomer';
import ReportOrder from 'features/Report/components/ReportOrder';
import ReportProduct from 'features/Report/components/ReportProduct';
import React, { useState } from 'react';
import { Card, Tabs, Tab } from 'react-bootstrap';

const MainPage = () => {
    const [key, setKey] = useState('product');
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
                        <Tab eventKey="product" title="Top Product">
                            <ReportProduct />
                        </Tab>
                        <Tab eventKey="customer" title="Loyal Customer">
                            <ReportCustomer/>
                        </Tab>
                        <Tab eventKey="order" title="Customer Order">
                            <ReportOrder/>
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
