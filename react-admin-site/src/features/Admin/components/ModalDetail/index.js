import React from 'react';
import { Modal } from 'react-bootstrap';
import { useSelector } from 'react-redux';
import { FormatDateTime } from 'utils';

const ModalDetail = (props) => {
    const { IsShow, OnclickCloseModalDetail } = props;
    const Data = useSelector((state) => state.admins.admin);

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
                            <img width="30%" className="img-fluid" src={`https://localhost:44324/images/avatars/${Data?.avatar}`} />
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
                            {FormatDateTime(Data?.birthday)}
                        </div>
                    </div>
                    <div className="row mb-2">
                        <div className="col-3">Join Date</div>
                        <div className="col-9">
                            {FormatDateTime(Data?.registeredDate)}
                        </div>
                    </div>
                    <div className="row mb-2">
                        <div className="col-3">Role</div>
                        <div className="col-9">
                            {Data?.roleName}
                        </div>
                    </div>

                </Modal.Body>
            </Modal>
        </React.Fragment>
    );
};

export default ModalDetail;
