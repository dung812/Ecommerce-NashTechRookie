import { Modal } from 'bootstrap';
import CustomLoader from 'components/CustomLoader/Index';
import { fetchOrders, fetchOrdersOfCustomer, searchOrder } from 'features/Order/OrderSlice';
import React, { useEffect, useState } from 'react';
import { Card } from 'react-bootstrap';
import DataTable from 'react-data-table-component';
import { useDispatch, useSelector } from 'react-redux';
import { useNavigate, useParams } from 'react-router-dom';
import { FormatDateTime, FormatStatusOrder } from 'utils';
import OrderDetail from '../OrderDetail';
import axios from 'axios';
import { useRef } from 'react';
import { useReactToPrint } from 'react-to-print';

const OrderOfCustomer = () => {
    const dispatch = useDispatch();
    let navigate = useNavigate();
    const { customerId } = useParams();
    const [status, setStatus] = useState(5)

    const [search, setSearch] = useState("");
    const [searchList, setSearchList] = useState([]);



    let orderList = useSelector((state) => state.orders.orders);
    let loading = useSelector((state) => state.orders.loading);

    useEffect(() => {
        setSearchList([...orderList])
    }, [loading])

    useEffect(() => {
        const params = {
            customerId: parseInt(customerId),
            status: status,
        };

        dispatch(fetchOrdersOfCustomer(params));
    }, [status])

    useEffect(() => {
        orderList = [...searchList]
        const result = orderList.filter((item) => {
            return item.orderId.toLowerCase().includes(search.toLowerCase());
        });
        dispatch(searchOrder(result))
    }, [search])

    const columns = [
        {
            name: "Order ID",
            selector: (row) => row.orderId,
            sortable: true,
        },
        {
            name: "Customer",
            selector: (row) => row.customerName,
            sortable: true,
        },
        {
            name: "Date",
            selector: (row) => FormatDateTime(row.orderDate),
            sortable: true,
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
            name: "Total",
            selector: (row) => `$${row.totalMoney}`,
            sortable: true,
        },
        {
            name: "Payment Method",
            selector: (row) => row.paymentName,
            sortable: true,
        }
    ]

    return (
        <React.Fragment>
            <Card>
                <Card.Body>
                    <DataTable
                        title='Order Data'
                        columns={columns}
                        data={orderList}
                        actions={<button onClick={() => navigate('/order')} className='btn btn-success'>All order</button>}
                        pagination
                        fixedHeader
                        selectableRows
                        selectableRowsHighlight
                        highlightOnHover
                        subHeader
                        subHeaderAlign='left'
                        subHeaderWrap
                        subHeaderComponent=
                        {
                            <React.Fragment>
                                <div className='d-flex justify-content-between w-100 mb-3'>
                                    <div className="d-flex align-items-center gap-3">
                                        <span>Status: </span>
                                        <select id="" className="form-select" onChange={(e) => setStatus(e.target.value)} defaultValue={5}>
                                            <option value="1">New Order</option>
                                            <option value="2">Waiting delivery</option>
                                            <option value="3">Delivered</option>
                                            <option value="4">Cancelled</option>
                                            <option value="5">All</option>
                                        </select>
                                    </div>

                                    <div>
                                        <div className="d-flex align-items-center gap-3">
                                            <span>Search: <input type="text" value={search} onChange={(e) => setSearch(e.target.value)} /></span>
                                        </div>
                                    </div>
                                </div>
                            </React.Fragment>
                        }
                        progressPending={loading}
                        progressComponent={
                            <div>
                                {Array(10)
                                    .fill("")
                                    .map((e, i) => (
                                        <CustomLoader key={i} style={{ opacity: Number(2 / i).toFixed(1) }} />
                                    ))}
                            </div>
                        }
                    />
                </Card.Body>
            </Card>
        </React.Fragment>
    );
};


export default OrderOfCustomer;
