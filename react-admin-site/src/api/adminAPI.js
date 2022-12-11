import axiosClient from "./axiosClient";

const adminApi = {
    auth: (LoginInfo) => {
        const url = "/Auth";
        return axiosClient.post(url, LoginInfo);
    },
    getMe: (id) => {
        const url = `/Admin/${id}`;
        return axiosClient.get(url);
    },
    getAll: (params) => {
        const url = '/Admin/GetAdminsPaging';
        return axiosClient.get(url, { params });
    },
    // edit, remove, ...
}
export default adminApi;
