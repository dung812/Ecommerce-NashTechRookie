import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";
import manufactureApi from "api/manufactureAPI"; 

const manufacture = createSlice({
    name: "manufactures",
    initialState: { loading: false, manufactures: [] },
    reducers: {
		searchManufacture: (state, action) => {
            state.manufactures = action.payload 
		}
	},
    extraReducers: builder => {
        builder
        .addCase(fetchManufactures.pending, (state, action) => {
            state.loading = true;
        })
        .addCase(fetchManufactures.fulfilled, (state, action) => {
            state.loading = false;
            state.manufactures = action.payload;
        })
        .addCase(fetchManufactures.rejected, (state, action) => {
            state.loading = false;
            state.manufactures = [];
        })
    }
})


const { reducer, actions } = manufacture;
export const { searchManufacture } = actions;
export default reducer;

export const fetchManufactures = createAsyncThunk('manufactures/fetchManufactures', async () => {
    const res = await manufactureApi.getAll();
    return res
})