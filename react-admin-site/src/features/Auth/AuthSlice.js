import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";
import adminApi from "api/adminAPI"; 

const authAdmin = createSlice({
    name: "authAdmin",
    initialState: { loading: false, isAuth: false, admin: {} },
    reducers: {
		logoutAccount: (state, action) => {
            state.isAuth = false;
            state.admin = {};
		}
	},
    extraReducers: builder => {
        builder
        .addCase(authAdminLogin.pending, (state, action) => {
            state.loading = true;
        })
        .addCase(authAdminLogin.fulfilled, (state, action) => {
            state.loading = false;
            state.isAuth = true;
            state.admin = action.payload;
            localStorage.setItem('authAdmin', action.payload.token)
        })
        .addCase(authAdminLogin.rejected, (state, action) => {
            alert('Invalid username or password')
            state.loading = false;
            state.isAuth = false;
            state.admin = {};
        })
    }
})


const { reducer, actions } = authAdmin;
export const { logoutAccount } = actions;
export default reducer;

export const authAdminLogin = createAsyncThunk('authAdmin/authAdminLogin', async (LoginInfo) => {
    const adminInfo = await adminApi.auth(LoginInfo);
    return adminInfo
})
