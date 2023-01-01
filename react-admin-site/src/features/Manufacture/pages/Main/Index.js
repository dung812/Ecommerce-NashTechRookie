import React, { useEffect, useState } from 'react'
import { useDispatch, useSelector } from 'react-redux';
import DataTable from 'react-data-table-component';
import { Card } from 'react-bootstrap';
import CustomLoader from 'components/CustomLoader/Index';
import { deleteManufacture, fetchManufactures, searchManufacture } from 'features/Manufacture/ManufactureSlice';
import { useNavigate } from 'react-router-dom';
import Swal from 'sweetalert2';

function MainPage(props) {
    const dispatch = useDispatch();
    const navigate = useNavigate();
    const [search, setSearch] = useState("");
    const [searchList, setSearchList] = useState([]);

    let manufactureList = useSelector((state) => state.manufactures.manufactures);
    let loading = useSelector((state) => state.manufactures.loading);

    useEffect(() => {
        dispatch(fetchManufactures());
    }, [])

    useEffect(() => {
        setSearchList([...manufactureList])
    }, [loading])

    useEffect(() => {
        manufactureList = [...searchList]
        const result = manufactureList.filter((item) => {
            return item.name.toLowerCase().includes(search.toLowerCase());
        });
        dispatch(searchManufacture(result))
    }, [search])
    

    function HandleRemove(manufactureId) {
        console.log(manufactureId)
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
                dispatch(deleteManufacture(parseInt(manufactureId)))
                !loading && Swal.fire(
                    'Deleted!',
                    'Manufacture has been deleted.',
                    'success'
                )
            }
        })
    }


    const columns = [
        {
            name: "ID",
            selector: (row) => <div className='cursor-pointer' onClick={() => navigate(`/manufacture/${row.manufactureId}`)}>{row.manufactureId}</div>,
            sortable: true,
        },
        {
            name: "Logo",
            selector: (row) => <img className='cursor-pointer' onClick={() => navigate(`/manufacture/${row.manufactureId}`)} width={100} src={"https://localhost:44324/images/brand/" + row.logo} />
        },
        {
            name: "Name",
            selector: (row) => <div className='cursor-pointer' onClick={() => navigate(`/manufacture/${row.manufactureId}`)}>{row.name}</div>,
            sortable: true,
        },
        {
            name: "Action",
            cell: (row) => (
                <div>
                    <button onClick={() => HandleRemove(row.manufactureId)} className='btn btn-danger'><i className='bx bx-trash'></i></button>
                </div>
            )
        }
    ]

    return (
        <React.Fragment>
            <Card>
                <Card.Body>
                    <DataTable
                        title='Manufacture Data'
                        columns={columns}
                        data={manufactureList}
                        pagination
                        fixedHeader
                        selectableRows
                        selectableRowsHighlight
                        highlightOnHover
                        actions={<button onClick={() => navigate('/manufacture/add')} className='btn btn-success'><i className='bx bx-plus-circle'></i> Add new manufacture</button>}
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
        </React.Fragment>
    )
}


export default MainPage
