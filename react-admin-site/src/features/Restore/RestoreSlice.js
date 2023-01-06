import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";
import productApi from "api/productAPI";

const restore = createSlice({
    name: "restore",
    initialState: { loading: false, disabledProducts: [] },
    extraReducers: builder => {
        builder
            .addCase(fetchDisabledProducts.fulfilled, (state, action) => {
                state.disabledProducts = action.payload;
            })
            .addCase(fetchDisabledProducts.rejected, (state, action) => {
                state.disabledProducts = [];
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