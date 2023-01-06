import { configureStore, ThunkAction, Action } from '@reduxjs/toolkit';
import productReducer from '../features/Product/ProductSlice'
import customerReducer from '../features/Customer/CustomerSlice'
import categoryReducer from '../features/Category/CategorySlice'
import manufactureReducer from '../features/Manufacture/ManufactureSlice'
import orderReducer from '../features/Order/OrderSlice'
import authReducer from '../features/Auth/AuthSlice'
import adminReducer from '../features/Admin/AdminSlice'
import activityReducer from '../features/Activity/ActivitySlice'
import restoreReducer from '../features/Restore/RestoreSlice'
import modalReducer from '../components/CustomModal/CustomModalSlice'

const rootReducer = {
	products: productReducer,
    customers: customerReducer,
    categories: categoryReducer,
    manufactures: manufactureReducer,
    orders: orderReducer,
    authAdmin: authReducer,
    admins: adminReducer,
    activity: activityReducer,
    restore: restoreReducer,
    modal: modalReducer
	// user: userReducer
}

const store = configureStore({
	reducer: rootReducer
});

export default store;