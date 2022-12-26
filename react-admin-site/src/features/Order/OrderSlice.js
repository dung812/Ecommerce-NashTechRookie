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

        .addCase(checkedOrder.pending, (state, action) => {
            state.loading = true;
        })
        .addCase(checkedOrder.fulfilled, (state, action) => {
            state.loading = false;
            state.orders = action.payload;
        })
        .addCase(checkedOrder.rejected, (state, action) => {
            state.loading = false;
            state.orders = [];
        })

        .addCase(successOrder.pending, (state, action) => {
            state.loading = true;
        })
        .addCase(successOrder.fulfilled, (state, action) => {
            state.loading = false;
            state.orders = action.payload;
        })
        .addCase(successOrder.rejected, (state, action) => {
            state.loading = false;
            state.orders = [];
        })

        .addCase(cancelledOrder.pending, (state, action) => {
            state.loading = true;
        })
        .addCase(cancelledOrder.fulfilled, (state, action) => {
            state.loading = false;
            state.orders = action.payload;
        })
        .addCase(cancelledOrder.rejected, (state, action) => {
            state.loading = false;
            state.orders = [];
        })

        .addCase(deleteOrder.pending, (state, action) => {
            state.loading = true;
        })
        .addCase(deleteOrder.fulfilled, (state, action) => {
            state.loading = false;
            state.orders = action.payload;
        })
        .addCase(deleteOrder.rejected, (state, action) => {
            state.loading = false;
            state.orders = [];
        })

    }
})


const { reducer, actions } = order;
export const { searchOrder } = actions;
export default reducer;


export const fetchOrders = createAsyncThunk('orders/fetchOrders', async (params) => {
    const res = await orderApi.getAll(params);
    return res
})

export const checkedOrder = createAsyncThunk('orders/checkedOrder', async ({statusOrder, orderId}) => {
    await orderApi.checkedOrder(orderId);

    const res = await orderApi.getAll(statusOrder);
    return res
})

export const successOrder = createAsyncThunk('orders/successOrder', async ({statusOrder, orderId}) => {
    await orderApi.successOrder(orderId);

    const res = await orderApi.getAll(statusOrder);
    return res
})

export const cancelledOrder = createAsyncThunk('orders/cancelledOrder', async ({statusOrder, orderId}) => {
    await orderApi.cancelledOrder(orderId);

    const res = await orderApi.getAll(statusOrder);
    return res
})

export const deleteOrder = createAsyncThunk('orders/deleteOrder', async ({statusOrder, orderId}) => {
    await orderApi.deleteOrder(orderId);

    const res = await orderApi.getAll(statusOrder);
    return res
})