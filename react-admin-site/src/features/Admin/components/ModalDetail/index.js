import { setAdminActivity } from 'features/Activity/ActivitySlice';
import React from 'react';
import { Modal } from 'react-bootstrap';
import { useDispatch, useSelector } from 'react-redux';
import { useNavigate } from 'react-router-dom';
import { FormatDate, FormatDateTime } from 'utils';

const ModalDetail = (props) => {
    const dispatch = useDispatch();
    const navigate = useNavigate();
    const { IsShow, OnclickCloseModalDetail } = props;
    const Data = useSelector((state) => state.admins.admin);

    function handleWatchActivity(adminId) {
        navigate(`/activity`)
    }

    return (
        <React.Fragment>
            <Modal
                show={IsShow}
                onHide={OnclickCloseModalDetail}
                backdrop="static"
                keyboard={false}
                centered
                className="modal-md"
            >
                <Modal.Header className="modal__header px-5">
                    <h5 className="m-0 bold text-nash-red">Detailed Admin Information</h5>
                    <i className="bx bx-x fw-bold fs-3 cursor-pointer" onClick={OnclickCloseModalDetail}></i>
                </Modal.Header>
                <Modal.Body className="modal__body px-5">
                    <div className="row mb-2">
                        <div className="col-12 d-flex justify-content-center">
                            <img width="30%" className="img-fluid" src={`http://ntdung812-001-site1.btempurl.com/images/avatars/${Data?.avatar}`} />
                        </div>
                    </div>
                    <div className="row mb-2">
                        <div className="col-3">ID</div>
                        <div className="col-9">
                            {Data?.adminId}
                        </div>
                    </div>
                    <div className="row mb-2">
                        <div className="col-3">Username</div>
                        <div className="col-9">
                            {Data?.userName}
                        </div>
                    </div>
                    <div className="row mb-2">
                        <div className="col-3">Full name</div>
                        <div className="col-9">
                            {Data?.firstName} {Data?.lastName}
                        </div>
                    </div>
                    <div className="row mb-2">
                        <div className="col-3">Gender</div>
                        <div className="col-9">
                            {Data?.gender}
                        </div>
                    </div>
                    <div className="row mb-2">
                        <div className="col-3">Email</div>
                        <div className="col-9">
                            {Data?.email}
                        </div>
                    </div>
                    <div className="row mb-2">
                        <div className="col-3">Phone</div>
                        <div className="col-9">
                            {Data?.phone}
                        </div>
                    </div>
                    <div className="row mb-2">
                        <div className="col-3">Birthday</div>
                        <div className="col-9">
                            {FormatDate(Data?.birthday)}
                        </div>
                    </div>
                    <div className="row mb-2">
                        <div className="col-3">Join Date</div>
                        <div className="col-9">
                            {FormatDate(Data?.registeredDate)}
                        </div>
                    </div>
                    <div className="row mb-2">
                        <div className="col-3">Role</div>
                        <div className="col-9">
                            {Data?.roleName}
                        </div>
                    </div>
                    <div className="row mt-3">
                        <div className="col-4">
                            <div className="radio">
                                <label><input type="radio" className="list_role" value="1" /> Admin</label>
                            </div>
                            <div className="radio">
                                <label><input type="radio" className="list_role" value="2" /> Employee</label>
                            </div>
                        </div>
                        <div className="col-8">
                            <button className='btn btn-primary btn-custom-loading'><div className="loader"></div><span>Change Role</span></button> 
                        </div>
                    </div>
            </Modal.Body>
            <Modal.Footer>
                <div className="row mb-2">
                    <div className="col-12">
                        <u className='cursor-pointer' onClick={() => handleWatchActivity(Data?.adminId)}>Watch activities of {Data?.firstName} {Data?.lastName}</u>
                    </div>
                </div>
            </Modal.Footer>
        </Modal>
        </React.Fragment >
    );
};

export default ModalDetail;
