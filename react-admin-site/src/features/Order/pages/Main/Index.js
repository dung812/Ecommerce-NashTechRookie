import React, { useEffect, useState } from 'react'
import { useDispatch, useSelector } from 'react-redux';
import DataTable from 'react-data-table-component';
import { Card, Modal } from 'react-bootstrap';
import OverlayTrigger from 'react-bootstrap/OverlayTrigger';
import Tooltip from 'react-bootstrap/Tooltip';
import CustomLoader from 'components/CustomLoader/Index';
import Swal from 'sweetalert2'

import { cancelledOrder, checkedOrder, deleteOrder, fetchOrders, searchOrder, successOrder } from 'features/Order/OrderSlice';
import OrderDetail from 'features/Order/components/OrderDetail';
import axios from 'axios';


function MainPage(props) {
    const dispatch = useDispatch();
    const [search, setSearch] = useState("");
    const [searchList, setSearchList] = useState([]);
    
    const [orderDetail, setOrderDetail] = useState({});
    const [productOfOrder, setProductOfOrder] = useState([]);

    const [status, setStatus] = useState(1)

    let orderList = useSelector((state) => state.orders.orders);
    let loading = useSelector((state) => state.orders.loading);




    const [showModal, setShowModal] = useState(false);

    const HandleCloseModel = () => setShowModal(false);
    const HandleShowModel = () => setShowModal(true);


    useEffect(() => {
        dispatch(fetchOrders(status));
    }, [status])

    useEffect(() => {
        setSearchList([...orderList])
    }, [loading])

    useEffect(() => {
        orderList = [...searchList]
        const result = orderList.filter((item) => {
            return item.orderId.toLowerCase().includes(search.toLowerCase());
        });
        dispatch(searchOrder(result))
    }, [search])


    function HandleDetailOrder(orderId) {
        var myOrder = orderList.find(item => item.orderId === orderId)
        setOrderDetail(myOrder)

        axios.get(`${process.env.REACT_APP_API_URL}/Order/GetProductListOfOrder/${orderId}`)
        .then(res => setProductOfOrder(res.data))

        HandleShowModel()
    }

    function HandleCheckedOrder(orderId) {
        const statusOrder = parseInt(status)

        Swal.fire({
            title: 'Are you sure?',
            text: "You won't be able to revert this!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, check this order!'
        }).then((result) => {
            if (result.isConfirmed) {
                dispatch(checkedOrder({ statusOrder, orderId }))

                !loading && Swal.fire(
                    'Checked!',
                    'Your order has been checked.',
                    'success'
                )
            }
        })
    }

    function HandleSuccessOrder(orderId) {
        const statusOrder = parseInt(status)

        Swal.fire({
            title: 'Are you sure?',
            text: "You won't be able to revert this!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes!'
        }).then((result) => {
            if (result.isConfirmed) {
                dispatch(successOrder({ statusOrder, orderId }))

                !loading && Swal.fire(
                    'Success!',
                    'Success this order.',
                    'success'
                )
            }
        })
    }

    function HandleCancelOrder(orderId) {
        const statusOrder = parseInt(status)

        Swal.fire({
            title: 'Are you sure?',
            text: "You won't be able to revert this!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes!'
        }).then((result) => {
            if (result.isConfirmed) {
                dispatch(cancelledOrder({ statusOrder, orderId }))

                !loading && Swal.fire(
                    'Success!',
                    'Cancellation this order.',
                    'success'
                )
            }
        })
    }

    function HandleRemove(orderId) {
        const statusOrder = parseInt(status)

        Swal.fire({
            title: 'Are you sure?',
            text: "You won't be able to revert this!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, delete it!'
        }).then((result) => {
            if (result.isConfirmed) {
                dispatch(deleteOrder({ statusOrder, orderId }))

                Swal.fire(
                    'Deleted!',
                    'Your order has been deleted.',
                    'success'
                )
            }
        })
    }

    function FormatDateTime(datetime) {
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


    const columns = [
        {
            name: "Order ID",
            selector: (row) => row.orderId,
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
                    {row.orderStatus === 2 ? <span className="badge bg-primary">{FormatStatusOrder(row.orderStatus)}</span> : ""}
                    {row.orderStatus === 3 ? <span className="badge bg-success">{FormatStatusOrder(row.orderStatus)}</span> : ""}
                    {row.orderStatus === 4 ? <span className="badge bg-danger">{FormatStatusOrder(row.orderStatus)}</span> : ""}
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
        },
        {
            name: "Action",
            cell: (row) => (
                <div>
                    <button onClick={() => HandleDetailOrder(row.orderId)} className='btn btn-warning me-1'><i className='bx bx-search-alt-2 fs-6'></i></button>
                    {
                        row.orderStatus === 1 ?
                            <OverlayTrigger
                                key={'checked-order'}
                                placement={'bottom'}
                                overlay={
                                    <Tooltip id={`checked-order`}>
                                        Checked order.
                                    </Tooltip>
                                }
                            >
                                <button onClick={() => HandleCheckedOrder(row.orderId)} className='btn btn-primary me-1'><i className='bx bx-check-square fs-6' ></i></button>
                            </OverlayTrigger> : ""
                    }
                    {
                        row.orderStatus === 2 ?
                            <OverlayTrigger
                                key={'success-order'}
                                placement={'bottom'}
                                overlay={
                                    <Tooltip id={`success-order`}>
                                        Success order.
                                    </Tooltip>
                                }
                            >
                                <button onClick={() => HandleSuccessOrder(row.orderId)} className='btn btn-success me-1'><i className='bx bx-check-circle fs-6'></i></button>
                            </OverlayTrigger> : ""
                    }
                    {
                        row.orderStatus === 2 ?
                            <OverlayTrigger
                                key={'cancel-order'}
                                placement={'bottom'}
                                overlay={
                                    <Tooltip id={`cancel-order`}>
                                        Cancellation order.
                                    </Tooltip>
                                }
                            >
                                <button onClick={() => HandleCancelOrder(row.orderId)} className='btn btn-danger me-1'><i className='bx bx-x fs-6'></i></button>
                            </OverlayTrigger> : ""
                    }
                    {
                        row.orderStatus !== 2 ?
                            <OverlayTrigger
                                key={'delete-order'}
                                placement={'bottom'}
                                overlay={
                                    <Tooltip id={`delete-order`}>
                                        Delete order.
                                    </Tooltip>
                                }
                            >
                                <button onClick={() => HandleRemove(row.orderId)} className='btn btn-danger'><i className='bx bx-trash'></i></button>
                            </OverlayTrigger> : ""
                    }

                </div>
            )
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
                        pagination
                        fixedHeader
                        selectableRows
                        selectableRowsHighlight
                        highlightOnHover
                        subHeader
                        subHeaderAlign='left'
                        subHeaderWrap
                        subHeaderComponent={
                            <div>
                                <div className="d-flex align-items-center gap-3 mb-3">
                                    <span>Status: </span>
                                    <select id="" className="form-select" onChange={(e) => setStatus(e.target.value)} defaultValue={1}>
                                        <option value="1">New Order</option>
                                        <option value="2">Waiting delivery</option>
                                        <option value="3">Delivered</option>
                                        <option value="4">Cancelled</option>
                                    </select>
                                </div>
                                <div className="d-flex align-items-center gap-3">
                                    <span>Search:</span> <input type="text" className="form-control" placeholder="Enter order ID" value={search} onChange={(e) => setSearch(e.target.value)} />
                                </div>
                            </div>
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

            <Modal
                show={showModal}
                onHide={HandleCloseModel}
                size="xl"
            >
                <Modal.Header closeButton>
                    <Modal.Title>Order detail: #{orderDetail.orderId}</Modal.Title>
                </Modal.Header>
                <Modal.Body>

                    <OrderDetail
                        order={orderDetail}
                        productOfOrder={productOfOrder}
                    />

                </Modal.Body>
            </Modal>
        </React.Fragment>
    )
}


export default MainPage
