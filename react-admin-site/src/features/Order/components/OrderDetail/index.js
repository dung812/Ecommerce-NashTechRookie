import React from 'react';
import './OrderDetail.scss'

const OrderDetail = React.forwardRef((props, ref) => {
    const { order, productOfOrder } = props;
    return (
        <div className='mt-2'  ref={ref}>
            <h4 className="mb-3 text-dark text-uppercase">Information Shipping</h4>
            <hr />
            <table className="table">
                <thead className="thead-light">
                    <tr>
                        <th scope="col" className="text-center" width="20%">Name</th>
                        <th scope="col" className="text-center" width="20%">Phone</th>
                        <th scope="col" className="text-center" width="30%">Address</th>
                        <th scope="col" className="text-center" width="30%">Note</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td className="text-center">
                            {order.orderName}
                        </td>
                        <td className="text-center">
                            {order.phone}
                        </td>
                        <td className="text-center">
                            {order.address}
                        </td>
                        <td className="text-center">
                            {order.note}
                        </td>
                    </tr>

                </tbody>
            </table>

            <h4 className="mb-3 text-dark text-uppercase mt-4">Products</h4>
            <hr />
            <table className="table">
                <thead className="thead-light">
                    <tr>
                        <th scope="col" className="text-center" width="5%">STT</th>
                        <th scope="col" className="text-center" width="35%">Product</th>
                        <th scope="col" className="text-center" width="10%">Quality</th>
                        <th scope="col" className="text-center" width="10%">Unit price</th>
                        <th scope="col" className="text-center" width="10%">Discount</th>
                        <th scope="col" className="text-center" width="15%">Total Discounted</th>
                        <th scope="col" className="text-center" width="15%">Total money</th>
                    </tr>
                </thead>
                <tbody>
                    {
                        productOfOrder.map((item, index) => {
                            return (
                                <tr key={index}>
                                    <td className="text-center font-weight-medium">1</td>
                                    <td>
                                        <div className="row align-items-center">
                                            <div className="col-md-4">
                                                <img
                                                    src={`https://localhost:44324/images/products/Image/${item.productImage}`}
                                                    width="80%"
                                                    className='img-fluid'
                                                />
                                            </div>
                                            <div className="col-md-8">
                                                <p className="m-0">{item.productName}</p>
                                                <p className="m-0 text-muted">Size: {item.attributeName}</p>
                                            </div>
                                        </div>
                                    </td>
                                    <td className="text-center">{item.quantity}</td>
                                    <td className="text-center">${item.unitPrice}</td>
                                    <td className="text-center">{item.promotionPercent}%</td>
                                    <td className="text-center">-${item.totalDiscounted}</td>
                                    <td className="text-center">${item.totalMoney}</td>
                                </tr>
                            )
                        })
                    }
                </tbody>
            </table>

            <h4 className="mb-3 text-dark text-uppercase mt-4">Payment</h4>
            <hr />
            <table className="table">
                <thead className="thead-light">
                    <tr>
                        <th scope="col" className="text-center">Total product money</th>
                        <th scope="col" className="text-center">Shipping</th>
                        <th scope="col" className="text-center">Total money discounted</th>
                        <th scope="col" className="text-center">Total payment</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td className="text-center">${order.totalMoney}</td>
                        <td className="text-center">$0</td>
                        <td className="text-center">-${order.totalDiscounted}</td>
                        <td className="text-center">${order.totalMoney}</td>
                    </tr>
                </tbody>
            </table>
        </div>
    );
});




export default OrderDetail;
