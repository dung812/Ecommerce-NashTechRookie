import React, { useEffect, useState } from 'react';
import { Card } from 'react-bootstrap';
import { MultiSelect } from "react-multi-select-component";
import DatePicker from 'react-datepicker';
import ReactPaginate from 'react-paginate';
import DataTable from 'react-data-table-component';
import { FormatDateTime, UTCWithoutHour } from 'utils';
import { useDispatch, useSelector } from 'react-redux';
import CustomLoader from 'components/CustomLoader/Index';
import { fetchActivities, searchActivity } from 'features/Activity/ActivitySlice';
import './Main.scss'

function MainPage(props) {
    const dispatch = useDispatch();
    const [selected, setSelected] = useState([]);
    const [date, setDate] = useState(null);
    const [search, setSearch] = useState('');
    const [searchList, setSearchList] = useState([]);


    let adminActivityId = useSelector((state) => state.activity.adminActivityId);
    let activityList = useSelector((state) => state.activity.activities);
    let loading = useSelector((state) => state.activity.loading);

    useEffect(() => {
        // Initialized data on table
        try {
            const params = {
                adminId: null,
                objectType: null,
                time: null
            };
            dispatch(fetchActivities(params));
        } catch (error) {
            console.log('Failed to fetch activity list: ', error);
        }
    }, [dispatch])

    useEffect(() => {
        setSearchList([...activityList])
    }, [loading])

    useEffect(() => {
        activityList = [...searchList]
        const result = activityList.filter((item) => {
            return item.objectName.toLowerCase().includes(search.toLowerCase());
        });
        dispatch(searchActivity(result))
    }, [search])

    const options = [
        { label: "Product", value: "Product" },
        { label: "Catalog", value: "Catalog" },
        { label: "Manufacture", value: "Manufacture" },
        { label: "Admin", value: "Admin" },
        { label: "Customer", value: "Customer" },
        { label: "Order", value: "Order" },
    ];


    useEffect(() => {
        const params = {
            adminId: adminActivityId,
            objectType: selected.map(item => item.value).toString(),
            time: date,
        }
        dispatch(fetchActivities(params));
    }, [selected])

    useEffect(() => {
        if (date) {
            const params = {
                adminId: adminActivityId,
                objectType: selected.map(item => item.value).toString(),
                time: UTCWithoutHour(date),
            }
            dispatch(fetchActivities(params));
        }
    }, [date])

    const columns = [
        {
            name: "Admin ID",
            selector: (row) => row.adminId,
            sortable: true,
        },
        {
            name: "Time",
            selector: (row) => FormatDateTime(row.time),
            sortable: true,
        },
        {
            name: "Activity type",
            selector: (row) => row.activityType,
            sortable: true,
        },
        {
            name: "Object type",
            selector: (row) => row.objectType,
            sortable: true,
        },
        {
            name: "Object name",
            selector: (row) => row.objectName,
            sortable: true,
        },
    ]

    return (
        <React.Fragment>
            <Card>
                <Card.Body className='p-4'>
                    <DataTable
                        title='Activity of admin'
                        columns={columns}
                        data={activityList}
                        pagination
                        fixedHeader
                        selectableRows
                        selectableRowsHighlight
                        highlightOnHover
                        subHeader
                        subHeaderAlign='left'
                        subHeaderWrap
                        subHeaderComponent={
                            <div className='row w-100 mb-3'>
                                <div className="col-8 p-0">
                                    <div className="row">
                                        <div className="col-6">
                                            <MultiSelect
                                                placeholder="Select columns"
                                                options={options}
                                                value={selected}
                                                onChange={setSelected}
                                                labelledBy="Test"
                                            />
                                        </div>
                                        <div className="col-6">
                                            <DatePicker
                                                autoComplete="on"
                                                dateFormat="dd/MM/yyyy"
                                                showMonthDropdown
                                                showYearDropdown
                                                dropdownMode="select"
                                                placeholderText="Assigned Date"
                                                selected={date}
                                                onChange={(date) => setDate(date)}
                                                className="form-control"
                                            />
                                        </div>
                                    </div>
                                </div>
                                <div className="col-4">
                                    <div className="d-flex align-items-center gap-3">
                                        <span>Search:</span> <input type="text" className="form-control" placeholder="Enter object name" value={search} onChange={(e) => setSearch(e.target.value)} />
                                    </div>
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
        </React.Fragment>
    )
}


export default MainPage
