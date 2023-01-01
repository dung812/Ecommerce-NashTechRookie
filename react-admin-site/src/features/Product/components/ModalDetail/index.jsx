import React from 'react';
import { Modal } from 'react-bootstrap';
import './ModalDetail.css'

const ModalDetail = (props) => {
    const { IsShow, OnclickCloseModalDetail, Data } = props;

    function FormatDateTime(datetime) {
        if (datetime) {
            let date = `${datetime.split("T")[0].split("-")[2]}-${datetime.split("T")[0].split("-")[1]}-${datetime.split("T")[0].split("-")[0]}`;
            let time = `${datetime.split("T")[1].split(":")[0]}:${datetime.split("T")[1].split(":")[1]}`;
            return `${date} ${time}`;
        }
        return "NaN";
    }
    return (
        <React.Fragment>
            <Modal
                show={IsShow}
                onHide={OnclickCloseModalDetail}
                backdrop="static"
                keyboard={false}
                centered
                className="modal-lg"
            >
                <Modal.Header className="modal__header px-5">
                    <h5 className="m-0 bold text-nash-red">Detailed Product Information ID: {Data?.productId}</h5>
                    <i className="bx bx-x fw-bold fs-3 cursor-pointer" onClick={OnclickCloseModalDetail}></i>
                </Modal.Header>
                <Modal.Body className="modal__body px-5">
                    <div className="row mb-2">
                        <div className="col-12 d-flex justify-content-center">
                            <img width="25%" className="img-fluid" src={"https://localhost:44324/images/products/Image/" + Data?.imageFileName} />
                        </div>
                    </div>
                    <div className="row mb-4 text-center">
                        <div className="col-4">
                            <img className="img-fluid w-70" src={`https://localhost:44324/images/products/ImageList/${Data?.imageName}/1/${Data?.imageNameGallery1}`} />
                        </div>
                        <div className="col-4">
                            <img className="img-fluid w-70" src={`https://localhost:44324/images/products/ImageList/${Data?.imageName}/2/${Data?.imageNameGallery2}`} />
                        </div>
                        <div className="col-4">
                            <img className="img-fluid w-70" src={`https://localhost:44324/images/products/ImageList/${Data?.imageName}/3/${Data?.imageNameGallery3}`} />
                        </div>
                    </div>
                    <div className="row mb-2">
                        <div className="col-3">Product Name</div>
                        <div className="col-9">
                            {Data?.productName}
                        </div>
                    </div>
                    <div className="row mb-2">
                        <div className="col-3">Unit Price</div>
                        <div className="col-9">
                            ${Data?.originalPrice}
                        </div>
                    </div>
                    <div className="row mb-2">
                        <div className="col-3">Promotion Percent</div>
                        <div className="col-9">
                            {Data?.promotionPercent}%
                        </div>
                    </div>
                    <div className="row mb-2">
                        <div className="col-3">Quantity</div>
                        <div className="col-9">
                            {Data?.quantity}
                        </div>
                    </div>
                    <div className="row mb-2">
                        <div className="col-3">Manufacture</div>
                        <div className="col-9">
                            {Data?.manufactureName}
                        </div>
                    </div>
                    <div className="row mb-2">
                        <div className="col-3">Category</div>
                        <div className="col-9">
                            {Data?.catalogName}
                        </div>
                    </div>
                    <div className="row mb-2">
                        <div className="col-3">Admin</div>
                        <div className="col-9">
                            {Data?.adminCreate}
                        </div>
                    </div>
                    <div className="row mb-2">
                        <div className="col-3">Create Date</div>
                        <div className="col-9">
                            {FormatDateTime(Data?.dateCreate)}
                        </div>
                    </div>
                    <div className="row mb-2">
                        <div className="col-3">Update Date</div>
                        <div className="col-9">
                            {FormatDateTime(Data?.updateDate)}
                        </div>
                    </div>
                    <div className="row mb-2">
                        <div className="col-3">Income</div>
                        <div className="col-9 text-danger">
                            ${Data?.income}
                        </div>
                    </div>
                    <div className="row">
                        <div className="col-3 font-weight-bold text-right">Rating:</div>
                        <div className="col-9">
                            <div className="row">
                                <div className="col-12">
                                    <i className='bx bxs-star'></i>
                                    <i className='bx bxs-star'></i>
                                    <i className='bx bxs-star'></i>
                                    <i className='bx bxs-star'></i>
                                    <i className='bx bxs-star'></i>
                                    <span className='link-item-rating ms-2'>(1 rating)</span>
                                </div>
                                <div className="col-12">
                                    <i className='bx bxs-star'></i>
                                    <i className='bx bxs-star'></i>
                                    <i className='bx bxs-star'></i>
                                    <i className='bx bxs-star'></i>
                                    <i className='bx bx-star'></i>
                                    <span className='link-item-rating ms-2'>(0 rating)</span>
                                </div>
                                <div className="col-12">
                                    <i className='bx bxs-star'></i>
                                    <i className='bx bxs-star'></i>
                                    <i className='bx bxs-star'></i>
                                    <i className='bx bx-star'></i>
                                    <i className='bx bx-star'></i>
                                    <span className='link-item-rating ms-2'>(2 rating)</span>
                                </div>
                                <div className="col-12">
                                    <i className='bx bxs-star'></i>
                                    <i className='bx bxs-star'></i>
                                    <i className='bx bx-star'></i>
                                    <i className='bx bx-star'></i>
                                    <i className='bx bx-star'></i>
                                    <span className='link-item-rating ms-2'>(0 rating)</span>
                                </div>
                                <div className="col-12">
                                    <i className='bx bxs-star'></i>
                                    <i className='bx bx-star'></i>
                                    <i className='bx bx-star'></i>
                                    <i className='bx bx-star'></i>
                                    <i className='bx bx-star'></i>
                                    <span className='link-item-rating ms-2'>(0 rating)</span>
                                </div>
                            </div>
                            <div className="row">
                                <div className="col-12"><div className='link-item-rating'>View all</div></div>
                            </div>
                        </div>
                    </div>
                </Modal.Body>
            </Modal>
        </React.Fragment>
    );
};

export default ModalDetail;
