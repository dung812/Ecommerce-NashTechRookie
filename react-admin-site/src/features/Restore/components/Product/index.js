import React, { useEffect } from 'react'
import PropTypes from 'prop-types'
import { useDispatch, useSelector } from 'react-redux';
import DataTable from 'react-data-table-component';
import { fetchProducts } from 'features/Product/ProductSlice';
import CustomLoader from 'components/CustomLoader/Index';
import { OverlayTrigger, Tooltip } from 'react-bootstrap';
import Swal from 'sweetalert2';
import { deleteDisabledProduct, fetchDisabledProducts, restoreDisabledProduct } from 'features/Restore/RestoreSlice';
import axios from 'axios';

function Product(props) {
    const dispatch = useDispatch();
    let disabledProductList = useSelector((state) => state.restore.disabledProducts);
    let loading = useSelector((state) => state.restore.loading);

    useEffect(() => {
        dispatch(fetchDisabledProducts());
    }, [])

    function HandleRestore(productId) {
        Swal.fire({
            title: 'Are you sure?',
            text: "You won't be able to restore this!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, retore it!'
        })
        .then((result) => {
            if (result.isConfirmed) {
                dispatch(restoreDisabledProduct(parseInt(productId)))
                !loading && Swal.fire(
                    'Restore!',
                    'Product has been restored.',
                    'success'
                )
            }
        })
    }

    function HandleRemove(productId) {
        Swal.fire({
            title: 'Are you sure?',
            text: "You won't be able to delete this!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, delete it!'
        })
        .then((result) => {
            if (result.isConfirmed) {
                axios.get(process.env.REACT_APP_API_URL + `/Product/CheckProductCanDelete/${parseInt(productId)}`)
                .then((res) => {
                    dispatch(deleteDisabledProduct(parseInt(productId)))
                    !loading && Swal.fire(
                        'Deleted!',
                        'Product has been deleted.',
                        'success'
                    )
                })
                .catch((err) => {
                    Swal.fire({
                        icon: 'error',
                        title: 'Oops...',
                        text: "Can't delete this product because existed another order has this product!",
                    })
                })

            }
        })
    }
    const columns = [
        {
            name: "Product Id",
            selector: (row) => row.productId,
            sortable: true,
        },
        {
            name: "Image",
            selector: (row) => <img width={100} src={"http://ntdung812-001-site1.btempurl.com/images/products/Image/" + row.imageFileName} />
        },
        {
            name: "Name",
            selector: (row) => row.productName,
            sortable: true,
        },
        {
            name: "Gender",
            selector: (row) => row.gender,
            sortable: true,
        },
        {
            name: "Manufacture",
            selector: (row) => row.manufactureName,
            sortable: true,
        },
        {
            name: "Catalog",
            selector: (row) => row.catalogName,
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
                        <button onClick={() => HandleRestore(row.productId)} className='btn btn-warning me-1'><i className='bx bx-minus-back'></i></button>
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
                        <button onClick={() => HandleRemove(row.productId)} className='btn btn-danger'><i className='bx bx-trash'></i></button>
                    </OverlayTrigger>

                </div>
            )
        }
    ]
    return (
        <React.Fragment>
            <DataTable
                title='Disabled Products'
                columns={columns}
                data={disabledProductList}
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
export default Product
