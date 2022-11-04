import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";
import categoryApi from "api/categoryAPI"; 

const category = createSlice({
    name: "categories",
    initialState: { loading: false, categories: [] },
    reducers: {
		searchCategory: (state, action) => {
            state.categories = action.payload 
		}
	},
    extraReducers: builder => {
        builder
        .addCase(fetchCategories.pending, (state, action) => {
            state.loading = true;
        })
        .addCase(fetchCategories.fulfilled, (state, action) => {
            state.loading = false;
            state.categories = action.payload;
        })
        .addCase(fetchCategories.rejected, (state, action) => {
            state.loading = false;
            state.categories = [];
        })

        .addCase(addNewCategory.pending, (state, action) => {
            state.loading = true;
        })
        .addCase(addNewCategory.fulfilled, (state, action) => {
            state.loading = false;
            state.categories = action.payload
        })

        .addCase(deleteCategory.pending, (state, action) => {
            state.loading = true;
        })
        .addCase(deleteCategory.fulfilled, (state, action) => {
            state.loading = false;
            state.categories = action.payload
        })

        .addCase(updateCategory.pending, (state, action) => {
            state.loading = true;
        })
        .addCase(updateCategory.fulfilled, (state, action) => {
            state.loading = false;
            state.categories = action.payload
        })
    }
})

const { reducer, actions } = category;
export const { searchCategory } = actions;
export default reducer;

export const fetchCategories = createAsyncThunk('categories/fetchCategories', async () => {
    const res = await categoryApi.getAll();
    return res
})

export const addNewCategory = createAsyncThunk('categories/addNewCategory', async (newCategory) => {
    console.log(newCategory)
    await categoryApi.addCategory(newCategory);

    // Lấy list category mới nhất sau khi đã thêm và lưu vào categories state
    const categories = await categoryApi.getAll();
    return categories;
})

export const deleteCategory = createAsyncThunk('categories/deleteCategory', async (categoryId) => {
    console.log(categoryId)
    await categoryApi.deleteCategory(categoryId);

    // Lấy list category mới nhất sau khi đã xóa và lưu vào categories state
    const categories = await categoryApi.getAll();
    return categories;
})

export const updateCategory = createAsyncThunk('categories/updateCategory', async (data) => {
    console.log(data)
    await categoryApi.updateCategory(data.CatalogId, data);

    // Lấy list category mới nhất sau khi đã update và lưu vào categories state
    const categories = await categoryApi.getAll();
    return categories;
})