import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";
import manufactureApi from "api/manufactureAPI"; 
import { toast } from "react-toastify";

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

        .addCase(addNewManufacture.pending, (state, action) => {
            state.loading = true;
        })
        .addCase(addNewManufacture.fulfilled, (state, action) => {
            state.loading = false;
            state.manufactures = action.payload;

            toast.success('Successfully add new manufacture', {
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
        .addCase(addNewManufacture.rejected, (state, action) => {
            state.loading = false;
            state.manufactures = [];
        })

        .addCase(updateManufacture.pending, (state, action) => {
            state.loading = true;
        })
        .addCase(updateManufacture.fulfilled, (state, action) => {
            state.loading = false;
            state.manufactures = action.payload;

            toast.success('Successfully update manufacture', {
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
        .addCase(updateManufacture.rejected, (state, action) => {
            state.loading = false;
            state.manufactures = [];
        })

        .addCase(deleteManufacture.pending, (state, action) => {
            state.loading = true;
        })
        .addCase(deleteManufacture.fulfilled, (state, action) => {
            state.loading = false;
            state.manufactures = action.payload;
        })
        .addCase(deleteManufacture.rejected, (state, action) => {
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

export const addNewManufacture = createAsyncThunk('manufactures/addNewManufacture', async (newManufacture) => {
    await manufactureApi.addManufacture(newManufacture);

    const res = await manufactureApi.getAll();
    return res
})

export const updateManufacture = createAsyncThunk('manufactures/updateManufacture', async (data) => {
    await manufactureApi.updateManufacture(parseInt(data.ManufactureId), data);

    const res = await manufactureApi.getAll();
    return res
})

export const deleteManufacture = createAsyncThunk('manufactures/deleteManufacture', async (manufactureId) => {
    await manufactureApi.deleteManufacture(manufactureId);

    // Sau khi xóa xong, gọi lại product list mới nhất về và update vào state product
    const res = await manufactureApi.getAll();
    return res
})