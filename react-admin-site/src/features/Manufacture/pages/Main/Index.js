import React, { useEffect, useState } from 'react'
import { useDispatch, useSelector } from 'react-redux';
import DataTable from 'react-data-table-component';
import { Card } from 'react-bootstrap';
import CustomLoader from 'components/CustomLoader/Index';
import { fetchManufactures, searchManufacture } from 'features/Manufacture/ManufactureSlice';

function MainPage(props) {
    const dispatch = useDispatch();
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
    
    function HandleRemove(customerId) {
        console.log(customerId)
    }

    const columns = [
        {
            name: "ID",
            selector: (row) => row.manufactureId,
            sortable: true,
        },
        {
            name: "Logo",
            selector: (row) => <img width={100} src={"https://localhost:44324/images/brand/" + row.logo} />
        },
        {
            name: "Name",
            selector: (row) => row.name,
            sortable: true,
        },
        {
            name: "Action",
            cell: (row) => (
                <div>
                    <button onClick={() => HandleRemove(row.customerId)} className='btn btn-warning me-1'><i className='bx bx-detail'></i></button>
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
                        title='Manufacture Data'
                        columns={columns}
                        data={manufactureList}
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
        </React.Fragment>
    )
}


export default MainPage
