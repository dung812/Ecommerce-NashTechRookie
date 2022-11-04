import axiosClient from "./axiosClient";

const categoryApi = {
  getAll: () => {
    const url = "/Catalog";
    return axiosClient.get(url); 
  },
  addCategory: (data) => {
    const url = `/Catalog`;
    return axiosClient.post(url, data);
  },
  deleteCategory: (id) => {
    const url = `/Catalog/${id}`;
    return axiosClient.delete(url);
  },
  updateCategory: (id, data) => {
    const url = `/Catalog/${id}`;
    return axiosClient.put(url, data);
  },
  // edit, remove, ...
}
export default categoryApi;
