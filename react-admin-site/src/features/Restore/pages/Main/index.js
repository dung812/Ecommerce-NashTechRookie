import React, { useEffect, useState } from 'react';
import { Card } from 'react-bootstrap';
import { MultiSelect } from "react-multi-select-component";
import DatePicker from 'react-datepicker';
import ReactPaginate from 'react-paginate';
import ModalDetail from 'features/Admin/components/ModalDetail';


function MainPage(props) {
    const [showModalDetail, setShowModalDetail] = useState(false);
    const [selected, setSelected] = useState([]);
    const [joinDate, setJoinDate] = useState(null);
    const [searchKeyword, setSearchKeyword] = useState('');
    useEffect(() => {
        console.log(searchKeyword)
    }, [searchKeyword])

    const options = [
        { label: "Grapes 🍇", value: "grapes" },
        { label: "Mango 🥭", value: "mango" },
        // { label: "Strawberry 🍓", value: "strawberry", disabled: true },
    ];

    const HandlePageClick = (data) => {
        let currentPage = data.selected + 1;
    }
    const HandleSortingClick = (event, sortType) => {

    }
    const handleSubmitFilters = () => {

    }

    const handleChangeKeyword = (event) => setSearchKeyword(event.target.value);

    const HandleCloseModalDetail = (event) => {
        setShowModalDetail(false);
    };

    const HandleWatchDetail = (productId) => {
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
                        <div className="col-3">
                            <MultiSelect
                                placeholder="Select columns"
                                options={options}
                                value={selected}
                                onChange={setSelected}
                                labelledBy="Test"
                            />
                        </div>
                        <div className="col-3">
                            <DatePicker
                                autoComplete="on"
                                dateFormat="dd/MM/yyyy"
                                showMonthDropdown
                                showYearDropdown
                                dropdownMode="select"
                                placeholderText="Assigned Date"
                                selected={joinDate}
                                onChange={(date) => setJoinDate(date)}
                                className="form-control"
                            ></DatePicker>
                        </div>
                        <div className="col-3"></div>
                        <div className="col-3">
                            <div className="input-group">
                                <input
                                    type="text"
                                    className="form-control"
                                    name="search"
                                    value={searchKeyword}
                                    onChange={(event) => handleChangeKeyword(event)}
                                />
                                <button
                                    className="btn btn-outline-secondary"
                                    type="button"
                                    onClick={handleSubmitFilters}
                                    style={{ border: "#c7c5c5 1px solid" }}
                                >
                                    <i className="bi bi-search"></i>
                                </button>
                            </div>

                        </div>


                    </div>

                    <div className="table-responsive">
                        <table className='table table-spacing'>
                            <thead>
                                <tr>
                                    <th className="cursor-pointer">
                                        <div className="sort-title my-2 desc" onClick={(event) => HandleSortingClick(event, "adminId")}>
                                            ID
                                            <i className="uil uil-sort ms-1"></i>
                                        </div>
                                    </th>
                                    <th className="cursor-pointer"  width="30%">
                                        <div className="sort-title my-2 desc" onClick={(event) => HandleSortingClick(event, "userName")}>
                                            Account
                                            <i className="uil uil-sort ms-1"></i>
                                        </div>
                                    </th>
                                    <th className="cursor-pointer">
                                        <div className="sort-title my-2 desc" onClick={(event) => HandleSortingClick(event, "lastName")}>
                                            Full name
                                            <i className="uil uil-sort ms-1"></i>
                                        </div>
                                    </th>
                                    <th className="cursor-pointer">
                                        <div className="sort-title my-2 desc" onClick={(event) => HandleSortingClick(event, "gender")}>
                                            Gender
                                            <i className="uil uil-sort ms-1"></i>
                                        </div>
                                    </th>
                                    <th className="cursor-pointer">
                                        <div className="sort-title my-2 desc" onClick={(event) => HandleSortingClick(event, "role")}>
                                            Role
                                            <i className="uil uil-sort ms-1"></i>
                                        </div>
                                    </th>
                                    <th className='border-0' width="15%"></th>
                                </tr>
                            </thead>
                            <tbody className="wrap-loading">
                                <tr>
                                    <td>
                                        <div>
                                            tesst
                                        </div>
                                    </td>
                                    <td>
                                        <div>
                                            <div className='d-flex align-items-center gap-1'>
                                                <img
                                                    src={`https://localhost:44324/images/avatars/avatar.jpg`}
                                                    alt=""
                                                    className='img-fluid rounded-circle'
                                                    width="18%"
                                                />
                                                <div>
                                                    <p className='m-0 fw-bold'>Nguyen Dung</p>
                                                    <p className='m-0'>dung@gmail.com</p>
                                                </div>
                                            </div>
                                        </div>
                                    </td>
                                    <td>
                                        <div>
                                            tesst
                                        </div>
                                    </td>
                                    <td>
                                        <div>
                                            test
                                        </div>
                                    </td>
                                    <td>
                                        <div>
                                            tesst
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
                            </tbody>

                        </table>

                        <ReactPaginate
                            previousLabel={'Previous'}
                            breakLabel={'...'}
                            nextLabelLabel={'Next'}
                            pageCount={10}
                            marginPagesDisplayed={3}
                            pageRangeDisplayed={6}
                            onPageChange={HandlePageClick}
                            containerClassName="pagination justify-content-end"
                            pageClassName={'page-item'}
                            pageLinkClassName={'page-link'}
                            previousClassName={'page-item'}
                            previousLinkClassName={'page-link'}
                            nextClassName="page-item"
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
