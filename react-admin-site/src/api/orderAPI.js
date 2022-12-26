import axiosClient from "./axiosClient";

const orderApi = {
  getAll: (params) => {
    const url = `/Order/GetOrdersFilter`;
    return axiosClient.get(url, { params }); 
  },
  checkedOrder: (orderId) => {
    const url = `/Order/CheckedOrder/${orderId}`;
    return axiosClient.get(url); 
  },
  successOrder: (orderId) => {
    const url = `/Order/SuccessDeliveryOrder/${orderId}`;
    return axiosClient.get(url); 
  },
  cancelledOrder: (orderId) => {
    const url = `/Order/CancellationOrder/${orderId}`;
    return axiosClient.get(url); 
  },
  deleteOrder: (orderId) => {
    const url = `/Order/${orderId}`;
    return axiosClient.delete(url); 
  },

  getRecentOrders: () => {
    const url = `/Order/GetRecentOrders`;
    return axiosClient.get(url); 
  },

}
export default orderApi;
