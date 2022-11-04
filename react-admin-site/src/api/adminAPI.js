import axiosClient from "./axiosClient";

const adminApi = {
  auth: (LoginInfo) => {
    const url = "/Auth";
    return axiosClient.post(url, LoginInfo); 
  },
  getMe: (id) => {
    const url = `/Admin/${id}`;
    return axiosClient.get(url); 
  }
  // edit, remove, ...
}
export default adminApi;
