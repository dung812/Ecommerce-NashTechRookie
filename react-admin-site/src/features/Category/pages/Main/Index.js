import React, { useEffect, useState } from 'react'
import PropTypes from 'prop-types'
import { useDispatch, useSelector } from 'react-redux';
import DataTable from 'react-data-table-component';
import { Button, Card, Modal } from 'react-bootstrap';
import CustomLoader from 'components/CustomLoader/Index';

import { addNewCategory, deleteCategory, fetchCategories, searchCategory, updateCategory } from '../../CategorySlice';
import CategoryForm from 'features/Category/components/CategoryForm/Index';
import Swal from 'sweetalert2';

MainPage.propTypes = {}

function MainPage(props) {

    const dispatch = useDispatch();
    const [search, setSearch] = useState("");
    const [searchList, setSearchList] = useState([]);
    const [initialValues, setInitialValues] = useState({
        name: ""
    });
    const [isAddMode, setIsAddMode] = useState();

    const [showModal, setShowModal] = useState(false);

    const handleClose = () => setShowModal(false);

    let categoryList = useSelector((state) => state.categories.categories);
    let loading = useSelector((state) => state.categories.loading);


    useEffect(() => {
        dispatch(fetchCategories());
    }, [])

    useEffect(() => {
        setSearchList([...categoryList])
    }, [loading])

    useEffect(() => {
        categoryList = [...searchList]
        const result = categoryList.filter((item) => {
            return item.name.toLowerCase().includes(search.toLowerCase());
        });
        dispatch(searchCategory(result))
    }, [search])



    const onSubmit = data => {
        if (isAddMode) {
            // Case add
            console.log('add')
            const newCategory = {
                Name: data.name
            }
            dispatch(addNewCategory(newCategory))
            !loading && Swal.fire('Saved!', '', 'success')
            setShowModal(false)
        }
        else {
            // Case update
            console.log('update')
            const updateData = {
                CatalogId: initialValues.catalogId,
                Name: data.name
            }

            Swal.fire({
                title: 'Do you want to save the changes?',
                showDenyButton: true,
                showCancelButton: true,
                confirmButtonText: 'Save',
                denyButtonText: `Don't save`,
            }).then((result) => {
                /* Read more about isConfirmed, isDenied below */
                if (result.isConfirmed) {
                    dispatch(updateCategory(updateData))

                    !loading && Swal.fire('Saved!', '', 'success')
                    setShowModal(false)
                } else if (result.isDenied) {
                    Swal.fire('Changes are not saved', '', 'info')
                    setShowModal(false)
                }
            })
            
        }

    }

    function HandleCreate() {
        setShowModal(true)
        setInitialValues({
            name: ""
        })
        setIsAddMode(true)
    }
    function HandleUpdate(categoryId) {
        let editedCategory = categoryList.find(category => category.catalogId === parseInt(categoryId));
        setInitialValues(editedCategory)

        setIsAddMode(false)
        setShowModal(true)
    }


    function HandleRemove(categoryId) {
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
                dispatch(deleteCategory(categoryId))

                !loading && Swal.fire(
                    'Deleted!',
                    'Category has been deleted.',
                    'success'
                )
            }
        })
    }

    const columns = [
        {
            name: "ID",
            selector: (row) => row.catalogId,
            sortable: true,
        },
        {
            name: "ID",
            selector: (row) => row.name,
            sortable: true,
        },
        {
            name: "Action",
            cell: (row) => (
                <React.Fragment>
                    <button onClick={() => HandleUpdate(row.catalogId)} className='btn btn-primary me-2'><i className='bx bx-edit'></i></button>
                    <button onClick={() => HandleRemove(row.catalogId)} className='btn btn-danger'><i className='bx bx-trash'></i></button>
                </React.Fragment>
            )
        }
    ]


    return (
        <React.Fragment>
            <Card>
                <Card.Body>
                    <DataTable
                        title='Category Data'
                        columns={columns}
                        data={categoryList}
                        pagination
                        fixedHeader
                        selectableRows
                        selectableRowsHighlight
                        highlightOnHover
                        actions={<button onClick={() => HandleCreate()} className='btn btn-success'><i className='bx bx-plus-circle'></i> Add new category</button>}
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

            {/* <Button variant="primary" onClick={handleShow}>
                Launch static backdrop modal
            </Button> */}

            <Modal
                show={showModal}
                onHide={handleClose}
                backdrop="static"
                keyboard={false}
            >
                <Modal.Header closeButton>
                    <Modal.Title>{isAddMode ? "Add new product category" : "Update category"}</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <CategoryForm
                        initialValues={initialValues}
                        isAddMode={isAddMode}
                        onSubmit={onSubmit}
                    />
                </Modal.Body>
            </Modal>
        </React.Fragment>
    )
}


export default MainPage
