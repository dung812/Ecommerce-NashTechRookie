import axiosClient from "./axiosClient";

const customerApi = {
  getAll: () => {
    const url = "/Customer";
    return axiosClient.get(url); 
  }
  // edit, remove, ...
}
export default customerApi;
