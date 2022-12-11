import React, { useEffect, useRef, useState } from 'react';
import { Card } from 'react-bootstrap';
import { MultiSelect } from "react-multi-select-component";
import DatePicker from 'react-datepicker';
import ReactPaginate from 'react-paginate';
import './Main.scss'
import ModalDetail from 'features/Admin/components/ModalDetail';
import { useDispatch, useSelector } from 'react-redux';
import { fetchAdmins, fetchAdminsOnclick, filterAdmins, getAdmin } from 'features/Admin/AdminSlice';
import CustomLoader from 'components/CustomLoader/Index';
import { formatDate, FormatDateTime, UTCWithoutHour } from 'utils';


function MainPage(props) {
    const dispatch = useDispatch();
    const [showModalDetail, setShowModalDetail] = useState(false);
    const [selected, setSelected] = useState([]);
    const [joinDate, setJoinDate] = useState(null);

    const options = [
        { label: "Admin", value: "1" },
        { label: "Employee", value: "2" },
    ];

    const headers = [
        {
            label: "ID",
            value: "id"
        },
        {
            label: "Account",
            value: "userName"
        },
        {
            label: "Full name",
            value: "lastName"
        },
        {
            label: "JoinDate",
            value: "registerDate"
        },
        {
            label: "Role",
            value: "role"
        },
    ];

    let adminList = useSelector((state) => state.admins.admins);
    let loading = useSelector((state) => state.admins.loading);
    let firstLoading = useSelector((state) => state.admins.firstLoading);
    let page = useSelector((state) => state.admins.page);
    let lastPage = useSelector((state) => state.admins.lastPage);

    let pageSize = useRef(2);
    let [fieldNameSorting, setFieldNameSorting] = useState();
    let [typeSorting, setTypeSorting] = useState();
    let search = useRef();
    let filterStr = useRef(null);
    let dateStr = useRef(null);

    useEffect(() => {
        // Initialized data on table
        try {
            const params = {
                filterByRole: null,
                filterByDate: null,
                searchString: null,
                fieldName: null,
                sortType: null,
                page: 1,
                limit: pageSize.current,
            };
            dispatch(fetchAdmins(params));
        } catch (error) {
            console.log('Failed to fetch admin list: ', error);
        }
    }, [dispatch])

    const fetchAdminList = async (filterByRole, filterByDate, searchString, fieldName, sortType, currentPage, limit) => {
        try {
            const params = {
                filterByRole,
                filterByDate,
                searchString,
                fieldName,
                fieldName,
                sortType,
                page: currentPage,
                limit
            };
            dispatch(fetchAdminsOnclick(params));
        } catch (error) {
            console.log('Failed to fetch assignment list: ', error);
        }
    }

    //? Sorting
    const HandleSortingClick = (event, fieldName) => {
        setFieldNameSorting(fieldName)
        if (typeSorting != null && fieldNameSorting == fieldName) {
            if (typeSorting == 'asc') {
                setTypeSorting('desc');
            }
            else if (typeSorting == 'desc') {
                setTypeSorting('asc');
            }
        }
        else {
            setTypeSorting('asc');
        }
    }
    useEffect(() => {
        fetchAdminList(filterStr.current, dateStr.current, search.current.value, fieldNameSorting, typeSorting, page, pageSize.current);
    }, [fieldNameSorting, typeSorting])

    //? Filter role
    useEffect(() => {
        // Handle filter
        filterStr.current = '';
        resetPaging()

        if (selected.length === options.length && selected.length === 0) {
            filterStr.current = 'All';
        }
        else {
            filterStr.current = selected.map(item => item.label).toString();
        }

        const params = {
            filterByRole: selected.length > 0 ? filterStr.current : "",
            filterByDate: dateStr.current,
            searchString: search.current.value,
            fieldName: fieldNameSorting,
            sortType: typeSorting,
            page: 1,
            limit: pageSize.current,
        };
        dispatch(filterAdmins(params));


    }, [selected])

    //! Filter date
    useEffect(() => {
        if (joinDate) {
            dateStr.current = UTCWithoutHour(joinDate);
            const params = {
                filterByRole: filterStr.current,
                filterByDate: dateStr.current,
                searchString: search.current.value,
                fieldName: fieldNameSorting,
                sortType: typeSorting,
                page: 1,
                limit: pageSize.current,
            };
            dispatch(filterAdmins(params));
        }
    }, [joinDate])


    //? searching
    const handleSubmitSearch = () => {
        const valueInput = search.current.value;
        fetchAdminList(filterStr.current, dateStr.current, valueInput, fieldNameSorting, typeSorting, 1, pageSize.current);
        resetPaging();
    }


    //? Paging
    const HandlePageClick = (data) => {
        let currentPage = data.selected + 1;
        fetchAdminList(filterStr.current, dateStr.current, search.current.value, fieldNameSorting, typeSorting, currentPage, pageSize.current);
    }

    const resetPaging = () => {
        // Reset paging
        let paglink = document.querySelectorAll('.page-item');
        paglink[1].firstChild.click();
    }

    const HandleCloseModalDetail = (event) => {
        setShowModalDetail(false);
    };

    const handleWatchDetail = (adminId) => {
        dispatch(getAdmin(adminId))
        setShowModalDetail(true);
    };

    const HandleCreate = () => {
    };


    return (
        <React.Fragment>
            <Card>
                <Card.Body className='p-4'>
                    <h3 className='mb-4'>Admin data</h3>

                    <div className="row">
                        <div className="col-12 text-end mb-3">
                            <button
                                onClick={() => HandleCreate()}
                                className='btn btn-success'
                            >
                                <i className='bx bx-plus-circle'></i>
                                Add new admin
                            </button>
                        </div>
                    </div>
                    <div className="row mb-2">
                        <div className={`col-3 ${loading ? ' table-loading' : ''}`}>
                            <MultiSelect
                                placeholder="Select columns"
                                options={options}
                                value={selected}
                                onChange={setSelected}
                                labelledBy="Test"
                            />
                        </div>
                        <div className={`col-3 ${loading ? ' table-loading' : ''}`}>
                            <DatePicker
                                autoComplete="on"
                                dateFormat="dd/MM/yyyy"
                                showMonthDropdown
                                showYearDropdown
                                dropdownMode="select"
                                placeholderText="Join Date"
                                selected={joinDate}
                                onChange={(date) => setJoinDate(date)}
                                className="form-control"
                            ></DatePicker>
                        </div>
                        <div className="col-3"></div>
                        <div className={`col-3 ${loading ? ' table-loading' : ''}`}>
                            <div className="input-group">

                                {/* <Autocomplete
                                    getItemValue={(item) => item.label}
                                    items={[
                                        { label: 'apple' },
                                        { label: 'banana' },
                                        { label: 'pear' }
                                    ]}
                                    renderItem={(item, isHighlighted) =>
                                        <div style={{ background: isHighlighted ? 'lightgray' : 'white' }}>
                                            {item.label}
                                        </div>
                                    }
                                    value={value}
                                    onChange={(e) => value = e.target.value}
                                    onSelect={(val) => value = val}
                                /> */}

                                <input
                                    type="search"
                                    className="form-control"
                                    name="search"
                                    ref={search}
                                />
                                <button
                                    className="btn btn-outline-secondary"
                                    type="button"
                                    onClick={handleSubmitSearch}
                                    style={{ border: "#c7c5c5 1px solid" }}
                                >
                                    <i className="bi bi-search"></i>
                                </button>
                            </div>

                        </div>


                    </div>

                    <div className={`table-responsive ${loading ? ' table-loading' : ''}`}>
                        {
                            !firstLoading ?
                                <table className='table'>
                                    <thead>
                                        <tr>
                                            {
                                                headers.map((item, index) => {
                                                    return (
                                                        <th className={`cursor-pointer ${item.value == 'userName' ? 'w-25' : ''}`} key={index}>
                                                            <div className="sort-title my-2" onClick={(event) => HandleSortingClick(event, item.value)}>
                                                                {item.label}
                                                                {
                                                                    fieldNameSorting === item.value
                                                                        ? (
                                                                            <i className={`uil ms-1 ${typeSorting == "asc" ? 'uil-angle-up' : 'uil-angle-down'}`}></i>
                                                                        )
                                                                        : (
                                                                            <i className="uil uil-sort ms-1"></i>
                                                                        )
                                                                }

                                                            </div>
                                                        </th>
                                                    )
                                                })
                                            }
                                            <th className='border-0' width="15%"></th>
                                        </tr>
                                    </thead>
                                    <tbody className="wrap-loading">
                                        {
                                            adminList?.length === 0
                                                ? (
                                                    <tr className="unabled-hover position-relative" style={{ height: '50px' }}>
                                                        <td className="empty-loading">
                                                            There are no item to display
                                                        </td>
                                                    </tr>
                                                )
                                                :
                                                adminList?.map((item, index) => {
                                                    return (
                                                        <tr key={index} onClick={() => handleWatchDetail(item.adminId)}>
                                                            <td>
                                                                <div>
                                                                    {item.adminId}
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div>
                                                                    <div className='d-flex align-items-center gap-1'>
                                                                        <img
                                                                            src={`https://localhost:44324/images/avatars/${item.avatar}`}
                                                                            alt=""
                                                                            className='img-fluid rounded-circle'
                                                                            width="18%"
                                                                        />
                                                                        <div>
                                                                            <p className='m-0 fw-bold'>{item.userName}</p>
                                                                            <p className='m-0'>{item.email}</p>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div>
                                                                    {item.firstName} {item.lastName}
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div>
                                                                    {FormatDateTime(item.registeredDate)}
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div>
                                                                    {item.roleName}
                                                                </div>
                                                            </td>
                                                            <td className="p-0 px-3 mx-5 border-0">
                                                                <button className='btn btn-primary me-2'>
                                                                    <i className='bx bx-edit'></i>
                                                                </button>
                                                                <button className='btn btn-danger'>
                                                                    <i className='bx bx-trash'></i>
                                                                </button>
                                                            </td>
                                                        </tr>
                                                    )
                                                })
                                        }
                                    </tbody>
                                </table>
                                :
                                <React.Fragment>
                                    <div>
                                        <div className="unabled-hover">
                                            <div className="border-0">
                                                <CustomLoader />
                                            </div>
                                        </div>
                                    </div>
                                </React.Fragment>
                        }

                        <ReactPaginate
                            previousLabel={'Previous'}
                            breakLabel={'...'}
                            nextLabelLabel={'Next'}
                            pageCount={lastPage}
                            marginPagesDisplayed={3}
                            pageRangeDisplayed={6}
                            onPageChange={HandlePageClick}
                            containerClassName="pagination justify-content-end"
                            pageClassName={'page-item'}
                            pageLinkClassName={'page-link'}
                            previousClassName={'page-item'}
                            previousLinkClassName={'page-link'}
                            nextClassName={`page-item ${lastPage === 0 ? 'disabled' : ''}`}
                            nextLinkClassName={'page-link'}
                            breakClassName={'page-link'}
                            activeClassName={'active'}
                        />
                    </div>
                </Card.Body>
            </Card>
            <ModalDetail
                IsShow={showModalDetail}
                OnclickCloseModalDetail={HandleCloseModalDetail}
            // Data={productInfo}
            />
        </React.Fragment>
    )
}


export default MainPage
