import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";
import productApi from "api/productAPI";

const product = createSlice({
    name: "products",
    initialState: { loading: false, products: [] },
    reducers: {
        searchProduct: (state, action) => {
            state.products = action.payload
        }
    },
    extraReducers: builder => {
        builder
            .addCase(fetchProducts.pending, (state, action) => {
                state.loading = true;
            })
            .addCase(fetchProducts.fulfilled, (state, action) => {
                state.loading = false;
                state.products = action.payload;
            })
            .addCase(fetchProducts.rejected, (state, action) => {
                state.loading = false;
                state.products = [];
            })

            .addCase(addNewProduct.pending, (state, action) => {
                state.loading = true;
            })
            .addCase(addNewProduct.fulfilled, (state, action) => {
                state.loading = false;
                state.products.push(action.payload)
            })

            .addCase(updateProduct.pending, (state, action) => {
                state.loading = true;
            })
            .addCase(updateProduct.fulfilled, (state, action) => {
                state.loading = false;
                state.products = action.payload;
            })


            .addCase(deleteProduct.pending, (state, action) => {
                state.loading = true;
            })
            .addCase(deleteProduct.fulfilled, (state, action) => {
                state.loading = false;
                state.products = action.payload;
            })
    }
})

const { reducer, actions } = product;
export const { searchProduct } = actions;
export default reducer;

// 
/*
    Mỗi một createAsyncThunk sẽ có 3 action:
    * pending: khi vừa gửi request
    * fullfilled: trạng thái thành công
    * rejected: trạng thái thất bại
*/
export const fetchProducts = createAsyncThunk('products/fetchProducts', async () => {
    const res = await productApi.getAll();
    return res
})
export const fetchProduct = createAsyncThunk('products/fetchProduct', async (productId) => {
    const res = await productApi.getProduct(productId);
    return res
})

export const addNewProduct = createAsyncThunk('products/addNewProduct', async (newProduct) => {
    await productApi.addProduct(newProduct);

    // Sau khi add xong, gọi lại product list mới nhất về và update vào state product
    const products = await productApi.getAll();
    return products
})

export const updateProduct = createAsyncThunk('products/updateProduct', async (data) => {
    await productApi.updateProduct(parseInt(data.ProductId), data);

    // Sau khi update xong, gọi lại product list mới nhất về và update vào state product
    const products = await productApi.getAll();
    return products
})

export const deleteProduct = createAsyncThunk('products/deleteProduct', async (productId) => {
    await productApi.deleteProduct(productId);

    // Sau khi xóa xong, gọi lại product list mới nhất về và update vào state product
    const products = await productApi.getAll();
    return products
})

// Dispatch vào thunk action creators trả ra thunk action
/*
    Thay vì dispatch vào action thông thường, nếu cần xử lý side effect thì ta sẽ dispatch vào thunk action creators
    trong thunk action sẽ xử lý bất đồng bộ và sẽ dispatch vào action chính
        - getState: trả ra tất cả các state hiện tại ở state chung
        - dispatch: để dispatch tới action chính
*/

// export function addProducts(product) {
//     return function addProductThunk(dispatch, getState) {
//         console.log('[add product thunk]', getState())
//         console.log({product})
//         product.ProductName = "tét"
//         const action = addProduct(product)
//         dispatch(action)
//         console.log('[add product thunk]', getState())
//     }
// }