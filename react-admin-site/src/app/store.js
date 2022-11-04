import { configureStore, ThunkAction, Action } from '@reduxjs/toolkit';
import productReducer from '../features/Product/ProductSlice'
import customerReducer from '../features/Customer/CustomerSlice'
import categoryReducer from '../features/Category/CategorySlice'

const rootReducer = {
	products: productReducer,
    customers: customerReducer,
    categories: categoryReducer
	// user: userReducer
}

const store = configureStore({
	reducer: rootReducer
});

export default store;