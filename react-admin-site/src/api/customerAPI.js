import axiosClient from "./axiosClient";

const customerApi = {
  getAll: () => {
    const url = "/Customer";
    return axiosClient.get(url); 
  },
  deleteCustomer: (id) => {
    const url = `/Customer/${id}`;
    return axiosClient.delete(url);
},
  // edit, remove, ...
}
export default customerApi;
