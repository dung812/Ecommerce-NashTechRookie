import { createSlice, createAsyncThunk, current } from "@reduxjs/toolkit";
import adminApi from "api/adminAPI";
import { toast } from "react-toastify";
import Swal from "sweetalert2";

const admin = createSlice({
    name: "admins",
    initialState: { firstLoading: false, loading: false, loadingSubmit: false, total: 0, page: 1, lastPage: 1, admins: [], admin: {}, firstAdmin: null },
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
                state.firstAdmin = null
                state.firstLoading = true;
            })
            .addCase(fetchAdmins.fulfilled, (state, action) => {
                state.firstLoading = false;
                state.page = action.payload.page;
                state.total = action.payload.totalItem;
                state.lastPage = action.payload.lastPage;
                state.admins = action.payload.admins;
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
                state.page = action.payload.page;
                state.total = action.payload.totalItem;
                state.lastPage = action.payload.lastPage;

                if (state.firstAdmin != null)
                    state.admins = [state.firstAdmin, ...action.payload.admins.filter(x => x.adminId !== state.firstAdmin.adminId)];
                else
                    state.admins = action.payload.admins;
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
                state.page = 1;
                state.total = action.payload.totalItem;
                state.lastPage = action.payload.lastPage;

                if (state.firstAdmin != null)
                    state.admins = [state.firstAdmin, ...action.payload.admins.filter(x => x.adminId !== state.firstAdmin.adminId)];
                else
                    state.admins = action.payload.admins;
            })
            .addCase(filterAdmins.rejected, (state, action) => {
                state.loading = false;
                state.admins = [];
            })



            .addCase(addNewAdmin.pending, (state, action) => {
                state.loadingSubmit = true;
            })
            .addCase(addNewAdmin.fulfilled, (state, action) => {
                const { admins, totalItem, lastPage } = action.payload.list;
                state.loadingSubmit = false;
                state.page = 1;
                state.total = totalItem;
                state.lastPage = lastPage;
                state.firstAdmin = action.payload.admin;
                state.admins = [state.firstAdmin, ...admins.filter(x => x.adminId !== state.firstAdmin.adminId)];

                toast.success('Successfully add new admin', {
                    position: "top-right",
                    autoClose: 5000,
                    hideProgressBar: false,
                    closeOnClick: true,
                    pauseOnHover: true,
                    draggable: true,
                    progress: undefined,
                    theme: "light",
                });
            })
            .addCase(addNewAdmin.rejected, (state, action) => {
                state.loadingSubmit = false;
                    toast.error("Existed username", {
                        position: "top-right",
                        autoClose: 5000,
                        hideProgressBar: false,
                        closeOnClick: true,
                        pauseOnHover: true,
                        draggable: true,
                        progress: undefined,
                        theme: "light",
                    });
            })

            .addCase(handleEditAdmin.pending, (state, action) => {
                state.loadingSubmit = true;
            })
            .addCase(handleEditAdmin.fulfilled, (state, action) => {
                const { admins, totalItem, lastPage } = action.payload.list;
                state.loadingSubmit = false;
                state.page = 1;
                state.total = totalItem;
                state.lastPage = lastPage;
                state.firstAdmin = action.payload.admin;
                state.admins = [state.firstAdmin, ...admins.filter(x => x.adminId !== state.firstAdmin.adminId)];

                toast.success('Successfully edit admin', {
                    position: "top-right",
                    autoClose: 5000,
                    hideProgressBar: false,
                    closeOnClick: true,
                    pauseOnHover: true,
                    draggable: true,
                    progress: undefined,
                    theme: "light",
                });
            })
            .addCase(handleEditAdmin.rejected, (state, action) => {
                state.loadingSubmit = false;
            })

            .addCase(handleDeleteAdmin.pending, (state, action) => {
                state.firstAdmin = null;
                state.loadingSubmit = true;
            })
            .addCase(handleDeleteAdmin.fulfilled, (state, action) => {
                const { admins, totalItem, lastPage } = action.payload;
                state.page = 1;
                state.total = totalItem;
                state.lastPage = lastPage;
                state.admins = admins;
                state.loadingSubmit = false;

                Swal.fire(
                    'Deleted!',
                    'Admin has been deleted.',
                    'success'
                )
            })
            .addCase(handleDeleteAdmin.rejected, (state, action) => {
                state.loadingSubmit = false;
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

export const addNewAdmin = createAsyncThunk('admins/addNewAdmin', async (newAdmin) => {
    var admin = await adminApi.addAdmin(newAdmin);
    const params = {
        page: 1,
        limit: 5,
    }
    const list = await adminApi.getAll(params);
    return { admin, list };
})


export const handleEditAdmin = createAsyncThunk('admins/handleEditAdmin', async ({ id, newInfo }) => {
    var admin = await adminApi.updateAdmin(id, newInfo);
    const params = {
        page: 1,
        limit: 5,
    }
    const list = await adminApi.getAll(params);
    return { admin, list };
})

export const handleDeleteAdmin = createAsyncThunk('admins/handleDeleteAdmin', async (id) => {
    await adminApi.deleteAdmin(id);
    const params = {
        page: 1,
        limit: 5,
    }
    const res = await adminApi.getAll(params);
    return res;
})
