// api/productApi.js
import axiosClient from "./axiosClient";

const productApi = {
  getAll: () => {
    const url = "/Product";
    return axiosClient.get(url); 
  },
  getProduct: (id) => {
    const url = `/Product/${id}`;
    return axiosClient.get(url);
  },
  addProduct: (data) => {
    const url = `/Product`;
    return axiosClient.post(url, data);
  },
  deleteProduct: (id) => {
    const url = `/Product/${id}`;
    return axiosClient.delete(url);
  },
  updateProduct: (id, data) => {
    const url = `/Product/${id}`;
    return axiosClient.put(url, data);
  },
  // edit, remove, ...
}
export default productApi;
