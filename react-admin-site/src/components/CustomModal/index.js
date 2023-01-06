import React, { useEffect } from 'react'
import './CustomModal.css'
import { useDispatch, useSelector } from 'react-redux';
import { closeModal } from './CustomModalSlice';
import { useNavigate } from 'react-router-dom';
import { fetchProducts } from 'features/Product/ProductSlice';

function CustomModal(props) {
    const navigate = useNavigate();
    const dispatch = useDispatch();
    let isShowModal = useSelector((state) => state.modal.isShowModal);
    let title = useSelector((state) => state.modal.title);
    let content = useSelector((state) => state.modal.content);
    let orderList = useSelector((state) => state.modal.orderList);

    useEffect(() => {
        if (orderList)
            dispatch(fetchProducts());
    }, [orderList])

    const HandleCloseModal = () => {
        dispatch(closeModal())
    }

    const handleRedirectProductDetails = (productId) => { 
        navigate(`/product/${productId}`)
        dispatch(closeModal())
    }
    return (
        <React.Fragment>
            <div className={`overlay ${isShowModal ? "show" : ""}`}></div>
            <div className={`custom-modal ${isShowModal ? "show" : ""}`}>
                <div className="modal-header">
                    <h5 className="modal-header__title p-0 m-0">{title}</h5>
                </div>
                <div className="modal-content">
                    <div className="modal-content-wrap" dangerouslySetInnerHTML={{ __html: content }} ></div>
                    <ul>
                        {
                            orderList && orderList.map((item, index) => {
                                return (
                                    <li key={index}>
                                        Product ID <span className='text-primary fw-bold cursor-pointer me-1' onClick={() => handleRedirectProductDetails(item.productId)}>#{item.productId}</span> 
                                        in order is <span className='text-danger fw-bold'>{item.quantityOfOrder}</span> | <span className='text-danger fw-bold'>{item.quantityOfStock}</span> in stock
                                    </li>
                                )
                            })
                        }
                    </ul>

                    <div className="text-end mt-3">
                        <button className="btn btn-outline-secondary" onClick={HandleCloseModal}>Close</button>
                    </div>
                </div>
            </div>
        </React.Fragment>
    )
}



export default CustomModal
