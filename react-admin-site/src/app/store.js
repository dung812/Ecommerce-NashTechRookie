import { configureStore, ThunkAction, Action } from '@reduxjs/toolkit';
import productReducer from '../features/Product/ProductSlice'
import customerReducer from '../features/Customer/CustomerSlice'
import categoryReducer from '../features/Category/CategorySlice'
import authReducer from '../features/Auth/AuthSlice'

const rootReducer = {
	products: productReducer,
    customers: customerReducer,
    categories: categoryReducer,
    authAdmin: authReducer,
	// user: userReducer
}

const store = configureStore({
	reducer: rootReducer
});

export default store;