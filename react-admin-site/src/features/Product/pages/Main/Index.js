import React, { useState, useEffect, useRef, useReducer } from 'react'
import PropTypes from 'prop-types'
import { Link, useNavigate } from "react-router-dom";
import axios from 'axios';
import DataTable from 'react-data-table-component';
import { Button, Card } from 'react-bootstrap';
import { useDispatch, useSelector } from 'react-redux';
import Swal from 'sweetalert2'
import withReactContent from 'sweetalert2-react-content'

import CustomLoader from 'components/CustomLoader/Index';
import { deleteProduct, fetchProducts, searchProduct } from 'features/Product/ProductSlice';

function MainPage(props) {

    const dispatch = useDispatch();
    const [search, setSearch] = useState("");

    let productList = useSelector((state) => state.products.products);
    let loading = useSelector((state) => state.products.loading);
    const [searchList, setSearchList] = useState([]);


    useEffect(() => {
        dispatch(fetchProducts());
    }, [])


    useEffect(() => {
        setSearchList([...productList])
    }, [loading])

    useEffect(() => {
        productList = [...searchList]
        const result = productList.filter((item) => {
            return item.productName.toLowerCase().includes(search.toLowerCase());
        });
        dispatch(searchProduct(result))
    }, [search])



    const navigate = useNavigate();
    function HandleCreate() {
        const createProductUrl = `/product/add`;
        navigate(createProductUrl);
    }
    function HandleUpdate(productId) {
        const editProductUrl = `/product/${productId}`;
        navigate(editProductUrl);
    }
    function HandleRemove(productId) {
        console.log(productId)
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
                dispatch(deleteProduct(parseInt(productId)))
                !loading && Swal.fire(
                    'Deleted!',
                    'Product has been deleted.',
                    'success'
                )
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
            selector: (row) => <img width={100} src={"https://localhost:44324/images/products/Image/" + row.image} />
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
                    <button onClick={() => HandleUpdate(row.productId)} className='btn btn-primary me-1'><i className='bx bx-edit'></i></button>
                    <button onClick={() => HandleRemove(row.productId)} className='btn btn-danger'><i className='bx bx-trash'></i></button>
                </div>
            )
        }
    ]
    return (
        <div>
            <Card>
                <Card.Body>
                    <DataTable
                        title='Product Data'
                        columns={columns}
                        data={productList}
                        pagination
                        fixedHeader
                        selectableRows
                        selectableRowsHighlight
                        highlightOnHover
                        actions={<button onClick={() => HandleCreate()} className='btn btn-success'><i className='bx bx-plus-circle'></i> Add new product</button>}
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
        </div>
    )
}

MainPage.propTypes = {}

export default MainPage
