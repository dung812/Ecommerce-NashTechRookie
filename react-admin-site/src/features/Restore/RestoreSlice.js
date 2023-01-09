import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";
import customerApi from "api/customerAPI";
import productApi from "api/productAPI";

const restore = createSlice({
    name: "restore",
    initialState: { loading: false, disabledProducts: [], disabledCustomers: [] },
    extraReducers: builder => {
        builder
            .addCase(fetchDisabledProducts.pending, (state, action) => {
                state.loading = true;
            })
            .addCase(fetchDisabledProducts.fulfilled, (state, action) => {
                state.disabledProducts = action.payload;
                state.loading = false;
            })
            .addCase(fetchDisabledProducts.rejected, (state, action) => {
                state.disabledProducts = [];
                state.loading = false;
            })

            .addCase(restoreDisabledProduct.pending, (state, action) => {
                state.loading = true;
            })
            .addCase(restoreDisabledProduct.fulfilled, (state, action) => {
                state.loading = false;
                state.disabledProducts = action.payload;
            })
            .addCase(restoreDisabledProduct.rejected, (state, action) => {
                state.loading = false;
                state.disabledProducts = [];
            })

            .addCase(deleteDisabledProduct.pending, (state, action) => {
                state.loading = true;
            })
            .addCase(deleteDisabledProduct.fulfilled, (state, action) => {
                state.loading = false;
                state.disabledProducts = action.payload;
            })
            .addCase(deleteDisabledProduct.rejected, (state, action) => {
                state.loading = false;
                state.disabledProducts = [];
            })


            .addCase(fetchDisabledCustomers.pending, (state, action) => {
                state.loading = true;
            })
            .addCase(fetchDisabledCustomers.fulfilled, (state, action) => {
                state.disabledCustomers = action.payload;
                state.loading = false;
            })
            .addCase(fetchDisabledCustomers.rejected, (state, action) => {
                state.disabledCustomers = [];
                state.loading = false;
            })
            .addCase(restoreDisabledCustomer.pending, (state, action) => {
                state.loading = true;
            })
            .addCase(restoreDisabledCustomer.fulfilled, (state, action) => {
                state.loading = false;
                state.disabledCustomers = action.payload;
            })
            .addCase(restoreDisabledCustomer.rejected, (state, action) => {
                state.loading = false;
                state.disabledCustomers = [];
            })
    }
})

const { reducer, actions } = restore;
export const { } = actions;
export default reducer;

export const fetchDisabledProducts = createAsyncThunk('restore/fetchDisabledProducts', async () => {
    const products = await productApi.getAllDisabled();
    return products
})
export const restoreDisabledProduct = createAsyncThunk('restore/restoreDisabledProduct', async (productId) => {
    await productApi.restoreProduct(productId);

    const products = await productApi.getAllDisabled();
    return products
})
export const deleteDisabledProduct = createAsyncThunk('restore/deleteDisabledProduct', async (productId) => {
    await productApi.hardDeleteProduct(productId);

    const products = await productApi.getAllDisabled();
    return products
})

//Customer
export const fetchDisabledCustomers = createAsyncThunk('restore/fetchDisabledCustomers', async () => {
    const customers = await customerApi.getAllDisabled();
    return customers
})

export const restoreDisabledCustomer = createAsyncThunk('restore/restoreDisabledCustomer', async (customerId) => {
    await customerApi.restoreCustomer(customerId);

    const customers = await customerApi.getAllDisabled();
    return customers
})