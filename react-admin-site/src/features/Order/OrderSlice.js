import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";
import orderApi from "api/orderAPI"; 

const order = createSlice({
    name: "orders",
    initialState: { loading: false, orders: [] },
    reducers: {
		searchOrder: (state, action) => {
            state.orders = action.payload 
		}
	},
    extraReducers: builder => {
        builder
        .addCase(fetchOrders.pending, (state, action) => {
            state.loading = true;
        })
        .addCase(fetchOrders.fulfilled, (state, action) => {
            state.loading = false;
            state.orders = action.payload;
        })
        .addCase(fetchOrders.rejected, (state, action) => {
            state.loading = false;
            state.orders = [];
        })
    }
})


const { reducer, actions } = order;
export const { searchOrder } = actions;
export default reducer;


export const fetchOrders = createAsyncThunk('orders/fetchOrders', async (status) => {
    const res = await orderApi.getAll(status);
    return res
})