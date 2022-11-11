import axiosClient from "./axiosClient";

const manufactureApi = {
  getAll: () => {
    const url = "/Manufacture";
    return axiosClient.get(url); 
  },
  addManufacture: (data) => {
    const url = `/Manufacture`;
    return axiosClient.post(url, data);
  },
  deleteManufacture: (id) => {
    const url = `/Manufacture/${id}`;
    return axiosClient.delete(url);
  },
  updateManufacture: (id, data) => {
    const url = `/Manufacture/${id}`;
    return axiosClient.put(url, data);
  },
  // edit, remove, ...
}
export default manufactureApi;