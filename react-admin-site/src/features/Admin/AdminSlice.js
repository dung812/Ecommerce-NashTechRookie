import { createSlice, createAsyncThunk, current  } from "@reduxjs/toolkit";
import adminApi from "api/adminAPI";

const initAdmin = {
    adminId: 0,
    userName: "",
    firstName: "",
    lastName: "",
    email: "",
    phone: "",
    birthday: "",
    gender: "",
    registeredDate: "",
    roleName: "",
}

const admin = createSlice({
    name: "admins",
    initialState: { firstLoading: false, loading: false, total: 0, page: 1, lastPage: 1, admins: [], admin: {} },
    reducers: {
        getAdmin: (state, action) => {
            const id = action.payload;
            const data = current(state.admins);
            state.admin = data.find(admin => admin.adminId === id);
        }
    },
    extraReducers: builder => {
        builder
            .addCase(fetchAdmins.pending, (state, action) => {
                state.firstLoading = true;
            })
            .addCase(fetchAdmins.fulfilled, (state, action) => {
                state.firstLoading = false;
                state.admins = action.payload.admins;
                state.page = action.payload.page;
                state.total = action.payload.totalItem;
                state.lastPage = action.payload.lastPage;
            })
            .addCase(fetchAdmins.rejected, (state, action) => {
                state.firstLoading = false;
                state.admins = [];
            })

            .addCase(fetchAdminsOnclick.pending, (state, action) => {
                state.loading = true;
            })
            .addCase(fetchAdminsOnclick.fulfilled, (state, action) => {
                state.loading = false;
                state.admins = action.payload.admins;
                state.page = action.payload.page;
                state.total = action.payload.totalItem;
                state.lastPage = action.payload.lastPage;
            })
            .addCase(fetchAdminsOnclick.rejected, (state, action) => {
                state.loading = false;
                state.admins = [];
            })

            .addCase(filterAdmins.pending, (state, action) => {
                state.loading = true;
            })
            .addCase(filterAdmins.fulfilled, (state, action) => {
                state.loading = false;
                state.admins = action.payload.admins;
                state.page = 1;
                state.total = action.payload.totalItem;
                state.lastPage = action.payload.lastPage;
            })
            .addCase(filterAdmins.rejected, (state, action) => {
                state.loading = false;
                state.admins = [];
            })

    }
})

const { reducer, actions } = admin;
export const { getAdmin } = actions;
export default reducer;

export const fetchAdmins = createAsyncThunk('admins/fetchAdmins', async (params) => {
    const res = await adminApi.getAll(params);
    return res
})
export const fetchAdminsOnclick = createAsyncThunk('admins/fetchAdminsOnclick', async (params) => {
    const res = await adminApi.getAll(params);
    return res
})
export const filterAdmins = createAsyncThunk('admins/filterAdmins', async (params) => {
    const res = await adminApi.getAll(params);
    return res
})