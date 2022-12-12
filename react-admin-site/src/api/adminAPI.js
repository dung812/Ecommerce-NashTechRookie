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
    addAdmin: (data) => {
        const url = `/Admin`;
        return axiosClient.post(url, data);
    },
    deleteAdmin: (id) => {
        const url = `/Admin/${id}`;
        return axiosClient.delete(url);
    },
    updateAdmin: (id, data) => {
        const url = `/Admin/${id}`;
        return axiosClient.put(url, data);
    },
    // edit, remove, ...
}
export default adminApi;
