import customerApi from 'api/customerAPI';
import axios from 'axios';
import CustomLoader from 'components/CustomLoader/Index';
import React, { useEffect, useState } from 'react';
import { OverlayTrigger, Tooltip } from 'react-bootstrap';
import DataTable from 'react-data-table-component';
import Swal from 'sweetalert2';
import { utils, writeFile } from 'xlsx';

const ReportOrder = () => {
    var today = new Date();
    var dd = String(today.getDate()).padStart(2, '0');
    var mm = String(today.getMonth() + 1).padStart(2, '0');
    var yyyy = today.getFullYear();

    const [reportList, setReportList] = useState([]);
    const [loading, setLoading] = useState(false);
    useEffect(() => {
        axios.get(process.env.REACT_APP_API_URL + '/Statistic/ReportCustomerCurrentDate')
            .then(res => setReportList(res.data.sort((a, b) => b.totalOrderCancelled - a.totalOrderCancelled)))
    }, [])

     function HandleRemove(customerId) {
        Swal.fire({
            title: 'Are you sure to ban this customer?',
            text: "You won't be able to revert this!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, delete it!'
        })
        .then((result) => {
            if (result.isConfirmed) {
                DeleteCustomer(customerId);
                !loading && Swal.fire(
                    'Deleted!',
                    'Customer has been banned.',
                    'success'
                )
            }

        })
    }

    async function DeleteCustomer(customerId) {
        setLoading(true);
        await customerApi.deleteCustomer(customerId);
        await axios.get(process.env.REACT_APP_API_URL + '/Statistic/ReportCustomerCurrentDate')
                .then(res => setReportList(res.data.sort((a, b) => b.totalOrderCancelled - a.totalOrderCancelled)))
        setLoading(false);
    }

    function HandleOnExport() {
        let Heading = [['ID', 'Name', 'Email', 'Total New Order', 'Total Order Waiting', 'Total Order Success', 'Total Order Cancelled']];
        //Had to create a new workbook and then add the header
        const wb = utils.book_new();
        const ws = utils.json_to_sheet([]);
        utils.sheet_add_aoa(ws, Heading);
        //Starting in the second row to avoid overriding and skipping headers
        utils.sheet_add_json(ws, reportList, { origin: 'A2', skipHeader: true });
        utils.book_append_sheet(wb, ws, 'Sheet1');
        writeFile(wb, 'Report.xlsx');
    }

    const columnsOrder = [
        {
            name: "Email",
            selector: (row) => row.email,
            sortable: true,
        },
        {
            name: "Total New Order",
            selector: (row) => row.totalNewOrder,
            sortable: true,
        },
        {
            name: "Total Order Waiting",
            selector: (row) => row.totalOrderWaiting,
            sortable: true,
        },
        {
            name: "Total Order Success",
            selector: (row) => row.totalOrderSuccess,
            sortable: true,
        },
        {
            name: "Total Order Cancelled",
            selector: (row) => row.totalOrderCancelled,
            sortable: true,
        },
        {
            name: "Action",
            cell: (row) => (
                <React.Fragment>
                    <OverlayTrigger
                        key={'delete-order'}
                        placement={'bottom'}
                        overlay={
                            <Tooltip id={`delete-customer`}>
                                Ban this account.
                            </Tooltip>
                        }
                    >
                        <button onClick={() => HandleRemove(row.key)} className='btn btn-danger'><i className='bx bx-block'></i></button>
                    </OverlayTrigger>
                    
                </React.Fragment>
            )
        }
    ]
    return (
        <div>
            <h4 className='text-dark fw-normal ms-3 mt-4'>Report orders of customers in current date: <span className='fw-bold'>{dd + '/' + mm + '/' + yyyy}</span></h4>
            <DataTable
                columns={columnsOrder}
                data={reportList}
                pagination
                fixedHeader
                selectableRowsHighlight
                highlightOnHover
                subHeader
                subHeaderComponent={
                    <button 
                        className="btn btn-primary" 
                        onClick={HandleOnExport}
                        disabled={reportList.length === 0}
                    >
                        <i className="uil uil-print"></i> Export
                    </button>
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
        </div>
    );
};


export default ReportOrder;
