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

    getAllDisabled: () => {
        const url = '/Customer/GetCustomerDisabled';
        return axiosClient.get(url); 
    },
    restoreCustomer: (id) => {
        const url = `/Customer/RestoreCustomer/${id}`;
        return axiosClient.get(url);
    }
}
export default customerApi;
