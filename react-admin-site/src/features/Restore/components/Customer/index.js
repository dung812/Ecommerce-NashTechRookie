import CustomLoader from 'components/CustomLoader/Index';
import { fetchDisabledCustomers, restoreDisabledCustomer } from 'features/Restore/RestoreSlice';
import React, { useEffect } from 'react'
import { OverlayTrigger, Tooltip } from 'react-bootstrap';
import DataTable from 'react-data-table-component';
import { useDispatch, useSelector } from 'react-redux';
import Swal from 'sweetalert2';
import { FormatDateTime } from 'utils';

function Customer(props) {
    const dispatch = useDispatch();
    let disabledCustomerList = useSelector((state) => state.restore.disabledCustomers);
    let loading = useSelector((state) => state.restore.loading);

    function HandleRestore(customerId) {
        console.log(customerId)
        Swal.fire({
            title: 'Are you sure?',
            text: "You won't be able to unban this customer account!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, retore it!'
        })
        .then((result) => {
            if (result.isConfirmed) {
                dispatch(restoreDisabledCustomer(parseInt(customerId)))
                !loading && Swal.fire(
                    'Restore!',
                    'Customer account has been unban.',
                    'success'
                )
            }
        })
    }

    function HandleRemove(customerId) {
        console.log(customerId)
    }
    useEffect(() => {
        dispatch(fetchDisabledCustomers());
    }, [])


    const columns = [
        {
            name: "ID",
            selector: (row) => row.customerId,
            sortable: true,
        },
        {
            name: "Account",
            selector: (row) => (
                <div className='d-flex align-items-center gap-1 cursor-pointer'>
                    <img
                        src={`http://ntdung812-001-site1.btempurl.com/images/avatars/${row.avatar}`}
                        alt=""
                        className='img-fluid rounded-circle'
                        width="20%"
                    />
                    <div>
                        <p className='m-0 fw-bold'>{row.firstName + " " + row.lastName}</p>
                        <p className='m-0'>{row.email}</p>
                    </div>
                </div>
            ),
        },
        {
            name: "Resigter Date",
            selector: (row) => FormatDateTime(row.registerDate),
            sortable: true,
        },
        {
            name: "Total Money Puschased",
            selector: (row) => `$${row.totalMoneyPuschased}`,
            sortable: true,
        },
        {
            name: "Action",
            cell: (row) => (
                <div>
                    <OverlayTrigger
                        key={'restore'}
                        placement={'bottom'}
                        overlay={
                            <Tooltip id={`restore`}>
                                Restore
                            </Tooltip>
                        }
                    >
                        <button onClick={() => HandleRestore(row.customerId)} className='btn btn-warning me-1'><i className='bx bx-minus-back'></i></button>
                    </OverlayTrigger>

                    <OverlayTrigger
                        key={'delete'}
                        placement={'bottom'}
                        overlay={
                            <Tooltip id={`delete`}>
                                Delete
                            </Tooltip>
                        }
                    >
                        <button onClick={() => HandleRemove(row.customerId)} className='btn btn-danger'><i className='bx bx-trash'></i></button>
                    </OverlayTrigger>

                </div>
            )
        }
    ]
    return (
        <React.Fragment>
            <DataTable
                title='Disabled Customers'
                columns={columns}
                data={disabledCustomerList}
                pagination
                selectableRowsHighlight
                highlightOnHover
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
        </React.Fragment>
    )
}


export default Customer
