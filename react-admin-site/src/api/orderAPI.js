import axiosClient from "./axiosClient";

const orderApi = {
  getAll: (status) => {
    const url = `/Order/${status}`;
    return axiosClient.get(url); 
  }
  // edit, remove, ...
}
export default orderApi;
