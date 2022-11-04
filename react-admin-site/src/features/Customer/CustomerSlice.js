import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";
import customerApi from "api/customerAPI"; 

const customer = createSlice({
    name: "customers",
    initialState: { loading: false, customers: [] },
    reducers: {
		searchCustomer: (state, action) => {
            state.customers = action.payload 
		}
	},
    extraReducers: builder => {
        builder
        .addCase(fetchCustomers.pending, (state, action) => {
            state.loading = true;
        })
        .addCase(fetchCustomers.fulfilled, (state, action) => {
            state.loading = false;
            state.customers = action.payload;
        })
        .addCase(fetchCustomers.rejected, (state, action) => {
            state.loading = false;
            state.customers = [];
        })
    }
})

const { reducer, actions } = customer;
export const { searchCustomer } = actions;
export default reducer;

export const fetchCustomers = createAsyncThunk('customers/fetchProducts', async () => {
    const res = await customerApi.getAll();
    return res
})