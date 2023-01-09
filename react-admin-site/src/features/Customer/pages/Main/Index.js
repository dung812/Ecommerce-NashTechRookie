import React, { useEffect, useState } from 'react'
import PropTypes from 'prop-types'
import { useDispatch, useSelector } from 'react-redux';
import DataTable from 'react-data-table-component';
import { Card } from 'react-bootstrap';
import CustomLoader from 'components/CustomLoader/Index';


import { fetchCustomers, handleDeleteCustomer, searchCustomer } from '../../CustomerSlice';
import ModalDetail from 'features/Customer/components/ModalDetail';
import Swal from 'sweetalert2';

MainPage.propTypes = {}

function MainPage(props) {
    const dispatch = useDispatch();
    const [search, setSearch] = useState("");
    const [searchList, setSearchList] = useState([]);

    const [showModalDetail, setShowModalDetail] = useState(false);
    const [customerId, setCustomerId] = useState();

    let customerList = useSelector((state) => state.customers.customers);
    let loading = useSelector((state) => state.customers.loading);

    useEffect(() => {
        dispatch(fetchCustomers());
    }, [])

    useEffect(() => {
        setSearchList([...customerList])
    }, [loading])

    useEffect(() => {
        customerList = [...searchList]
        const result = customerList.filter((item) => {
            return item.email.toLowerCase().includes(search.toLowerCase());
        });
        dispatch(searchCustomer(result))
    }, [search])

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
                dispatch(handleDeleteCustomer(parseInt(customerId)))
                !loading && Swal.fire(
                    'Deleted!',
                    'Customer has been banned.',
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

    const handleWatchDetail = (customerId) => {
        setCustomerId(customerId)
        setShowModalDetail(true);
    };
    const HandleCloseModalDetail = (event) => {
        setShowModalDetail(false);
    };

    const columns = [
        {
            name: "ID",
            selector: (row) => row.customerId,
            sortable: true,
        },
        {
            name: "Account",
            selector: (row) => (
                <div onClick={() => handleWatchDetail(row.customerId)} className='d-flex align-items-center gap-1 cursor-pointer'>
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
                    <button onClick={() => HandleRemove(row.customerId)} className='btn btn-danger'><i className='bx bx-block'></i></button>
                </div>
            )
        }
    ]


    return (
        <React.Fragment>
            <Card>
                <Card.Body>
                    <DataTable
                        title='Customer Data'
                        columns={columns}
                        data={customerList}
                        pagination
                        fixedHeader
                        selectableRows
                        selectableRowsHighlight
                        highlightOnHover
                        subHeader
                        subHeaderComponent={<span>Search: <input type="text" value={search} onChange={(e) => setSearch(e.target.value)} /></span>}
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

            <ModalDetail
                IsShow={showModalDetail}
                OnclickCloseModalDetail={HandleCloseModalDetail}
                CustomerId={customerId}
            />
        </React.Fragment>
    )
}


export default MainPage
