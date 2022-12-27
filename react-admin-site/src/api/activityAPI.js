import axiosClient from "./axiosClient";

const activityApi = {
    getActivityOfAdmin: (params) => {
        const url = `/Admin/GetActivitiesOfAdmin`;
        return axiosClient.get(url, {params});
    },
}
export default activityApi;
