import React from 'react';
import { Modal, ModalFooter } from 'react-bootstrap';
import { useSelector } from 'react-redux';
import { useNavigate } from 'react-router-dom';
import { FormatDateTime } from 'utils';

const ModalDetail = (props) => {
    let navigate = useNavigate();
    const { IsShow, OnclickCloseModalDetail, CustomerId } = props;
    const Data = useSelector((state) => state.customers.customers.find(item => item.customerId === CustomerId));

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
                    <h5 className="m-0 bold text-nash-red">Detailed Admin Information ID: {Data?.customerId}</h5>
                    <i className="bx bx-x fw-bold fs-3 cursor-pointer" onClick={OnclickCloseModalDetail}></i>
                </Modal.Header>
                <Modal.Body className="modal__body px-5">
                    <div className="row mb-2">
                        <div className="col-12 d-flex justify-content-center">
                            <img width="30%" className="img-fluid" src={`http://ntdung812-001-site1.btempurl.com/images/avatars/${Data?.avatar}`} />
                        </div>
                    </div>
                    <div className="row mb-2">
                        <div className="col-5">Full name</div>
                        <div className="col-7">
                            {Data?.firstName} {Data?.lastName}
                        </div>
                    </div>
                    <div className="row mb-2">
                        <div className="col-5">Email</div>
                        <div className="col-7">
                            {Data?.email}
                        </div>
                    </div>
                    <div className="row mb-2">
                        <div className="col-5">Register Date</div>
                        <div className="col-7">
                            {FormatDateTime(Data?.registerDate)}
                        </div>
                    </div>
                    <div className="row mb-2">
                        <div className="col-5">Total  New Order</div>
                        <div className="col-7">
                            9
                        </div>
                    </div>
                    <div className="row mb-2">
                        <div className="col-5">Total Order Success</div>
                        <div className="col-7">
                            {/* {Data?.totalOrderSuccess} */}
                            0
                        </div>
                    </div>
                    <div className="row mb-2">
                        <div className="col-5">Total Order Cancel</div>
                        <div className="col-7">
                            {/* {Data?.totalOrderCancel} */}
                            0
                        </div>
                    </div>
                    <div className="row mb-2">
                        <div className="col-5">Money Puschased</div>
                        <div className="col-7 text-danger">
                            ${Data?.totalMoneyPuschased}
                        </div>
                    </div>
                </Modal.Body>
                <ModalFooter>
                    <div className="row mb-2">
                        <div className="col-12">
                            <u className='cursor-pointer' onClick={() => navigate(`/order/${Data?.customerId}`)}>Watch order list of {Data?.firstName} {Data?.lastName}</u>
                        </div>
                    </div>
                </ModalFooter>
            </Modal>
        </React.Fragment>
    );
};

export default ModalDetail;
