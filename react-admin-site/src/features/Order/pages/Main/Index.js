import React, { useEffect, useState, useRef } from 'react'
import { useReactToPrint } from 'react-to-print';
import DatePicker from 'react-datepicker';
import { useDispatch, useSelector } from 'react-redux';
import DataTable from 'react-data-table-component';
import { Card, Modal } from 'react-bootstrap';
import OverlayTrigger from 'react-bootstrap/OverlayTrigger';
import Tooltip from 'react-bootstrap/Tooltip';
import CustomLoader from 'components/CustomLoader/Index';
import Swal from 'sweetalert2'
import './Main.scss'

import { cancelledOrder, checkedOrder, deleteOrder, fetchOrders, searchOrder, successOrder } from 'features/Order/OrderSlice';
import OrderDetail from 'features/Order/components/OrderDetail';
import axios from 'axios';
import { UTCWithoutHour } from 'utils';


function MainPage(props) {
    const dispatch = useDispatch();
    const [search, setSearch] = useState("");
    const [searchList, setSearchList] = useState([]);

    const [orderDetail, setOrderDetail] = useState({});
    const [productOfOrder, setProductOfOrder] = useState([]);

    const [status, setStatus] = useState(1)
    const [fromDate, setFromDate] = useState(null);
    const [toDate, setToDate] = useState(null);

    let orderList = useSelector((state) => state.orders.orders);
    let loading = useSelector((state) => state.orders.loading);

    const [showModal, setShowModal] = useState(false);

    const HandleCloseModel = () => setShowModal(false);
    const HandleShowModel = () => setShowModal(true);

    useEffect(() => {
        const navItems = document.querySelectorAll('.nav-items');
        navItems.forEach(item =>  item.classList.remove('active'))
        navItems.forEach(item => {
            if (item.innerText === 'Order') 
                item.classList.add('active')
        })
    }, [])

    useEffect(() => {
        let  params = {};
        if (fromDate == null || toDate == null) {
            params = {
                status: status,
                FromDate: null,
                ToDate: null
            };
        }
        else {
            params = {
                status: status,
                FromDate: UTCWithoutHour(fromDate),
                ToDate: UTCWithoutHour(toDate)
            };
        }

        dispatch(fetchOrders(params));
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
                const params = {
                    status: statusOrder,
                    FromDate: fromDate ? UTCWithoutHour(fromDate) : null,
                    ToDate: toDate ? UTCWithoutHour(toDate) : null
                };

                dispatch(checkedOrder({ orderId, params }))

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
                const params = {
                    status: statusOrder,
                    FromDate: fromDate ? UTCWithoutHour(fromDate) : null,
                    ToDate: toDate ? UTCWithoutHour(toDate) : null
                };
                dispatch(successOrder({ orderId, params }))

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
                const params = {
                    status: statusOrder,
                    FromDate: fromDate ? UTCWithoutHour(fromDate) : null,
                    ToDate: toDate ? UTCWithoutHour(toDate) : null
                };
                dispatch(cancelledOrder({ orderId, params }))

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
                const params = {
                    status: statusOrder,
                    FromDate: fromDate ? UTCWithoutHour(fromDate) : null,
                    ToDate: toDate ? UTCWithoutHour(toDate) : null
                };
                dispatch(deleteOrder({ orderId, params }))

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

    function HandleFilterDate() {
        if (fromDate == null || toDate == null) {
            Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: 'Date null!',
            })
            setFromDate(null)
            setToDate(null)
            return;
        }
        const convertFormDate = UTCWithoutHour(fromDate);
        const convertToDate = UTCWithoutHour(toDate);
        console.log({convertFormDate, convertToDate})
        try {
            const params = {
                status: status,
                FromDate: UTCWithoutHour(fromDate),
                ToDate: UTCWithoutHour(toDate)
            };
            dispatch(fetchOrders(params));
        } catch (error) {
            console.log('Failed to fetch order list: ', error);
        }
    }

    function ResetFilterDate() {
        setFromDate(null)
        setToDate(null)
        try {
            const params = {
                status: status,
                FromDate: null,
                ToDate: null
            };
            dispatch(fetchOrders(params));
        } catch (error) {
            console.log('Failed to fetch order list: ', error);
        }
    }

    const columns = [
        {
            name: "Order ID",
            selector: (row) => <div className='cursor-pointer' onClick={() => HandleDetailOrder(row.orderId)}>{row.orderId}</div>,
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
        },
        {
            name: "Action",
            cell: (row) => (
                <div>
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
                                <button onClick={() => HandleSuccessOrder(row.orderId)} className='btn btn-success me-1'><i className='bx bx-task'></i></button>
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
                                <button onClick={() => HandleCancelOrder(row.orderId)} className='btn btn-warning me-1'><i className='bx bx-task-x'></i></button>
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

    const componentRef = useRef();
    const handlePrint = useReactToPrint({
        content: () => componentRef.current,
    });

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
                            <React.Fragment>
                                <div className='row w-100 mb-3'>
                                    <div className="col-8">
                                        <div className="row">
                                            <div className="col-6">
                                                <div className="d-flex align-items-center gap-3">
                                                    <span>Status: </span>
                                                    <select id="" className="form-select" onChange={(e) => setStatus(e.target.value)} defaultValue={1}>
                                                        <option value="1">New Order</option>
                                                        <option value="2">Waiting delivery</option>
                                                        <option value="3">Delivered</option>
                                                        <option value="4">Cancelled</option>
                                                        <option value="5">All</option>
                                                    </select>
                                                </div>
                                            </div>
                                            <div className="col-6">
                                            </div>
                                        </div>
                                    </div>
                                    <div className="col-4">
                                        <div className="d-flex align-items-center gap-3">
                                            <span>Search:</span> <input type="text" className="form-control" placeholder="Enter order ID" value={search} onChange={(e) => setSearch(e.target.value)} />
                                        </div>
                                    </div>
                                </div>
                                <div className="row w-100">
                                    <div className="w-100 d-flex align-items-center">
                                        <div className="col-3">
                                            <span>Filter by order date: </span>
                                        </div>
                                        <div className="col-9 p-0">
                                            <div className="d-flex gap-2">
                                                <DatePicker
                                                    autoComplete="on"
                                                    dateFormat="dd/MM/yyyy"
                                                    showMonthDropdown
                                                    showYearDropdown
                                                    dropdownMode="select"
                                                    placeholderText="From Date"
                                                    selected={fromDate}
                                                    onChange={(date) => setFromDate(date)}
                                                    className="form-control"
                                                />
                                                <DatePicker
                                                    autoComplete="on"
                                                    dateFormat="dd/MM/yyyy"
                                                    showMonthDropdown
                                                    showYearDropdown
                                                    dropdownMode="select"
                                                    placeholderText="To Date"
                                                    selected={toDate}
                                                    onChange={(date) => setToDate(date)}
                                                    className="form-control"
                                                />
                                                <button className='btn btn-primary' onClick={HandleFilterDate}><i className='bx bx-filter-alt'></i></button>
                                                <button className='btn btn-outline-dark' onClick={ResetFilterDate}><i className='bx bx-x'></i></button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div className="w-100 text-end">

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
                        ref={componentRef}
                    />

                </Modal.Body>
                <Modal.Footer>
                    <button className='btn btn-primary' onClick={handlePrint}><i className='bx bx-printer'></i> Print</button>
                </Modal.Footer>
            </Modal>
        </React.Fragment>
    )
}


export default MainPage
