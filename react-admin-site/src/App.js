import productApi from 'api/productAPI';
import Header from 'components/Header';
import NotFound from 'components/NotFound/Index';
import Sidebar from 'components/Sidebar';
import Dashboard from 'features/Dashboard/Index';
import Product from 'features/Product/Index';
import Customer from 'features/Customer/Index';
import React, { useEffect, useState } from 'react';
import { BrowserRouter as Router, Route, Routes } from "react-router-dom";

import { useDispatch, useSelector } from 'react-redux';

import './App.scss';
import { fetchProducts } from 'features/Product/ProductSlice';
import axios from 'axios';
import Category from 'features/Category/Index';
import Order from 'features/Order/Index';
import Manufacture from 'features/Manufacture/Index';
import Admin from 'features/Admin/Index';
import SignIn from 'features/Auth/pages/SignIn/Index';
import ProtectedRoutes from 'ProtectedRoutes';
import Activity from 'features/Activity';
import Restore from 'features/Restore';
import Analytic from 'features/Analytic';
import { ToastContainer } from 'react-toastify';

const useAuth = () => {
    const user = { loggedIn: false }
    return user && user.loggedIn;
}

function App() {
    // useEffect(() => {
    //     window.addEventListener('beforeunload', (e) => {
    //         e.returnValue = "Changes you made may not be saved."
    //     })
    // }, []);

    const isAuth = useSelector((state) => state.authAdmin.isAuth);
    return (
        <div className="App">

            <Router>
                <Sidebar />
                <Header />
                <div className="content-body py-5 px-4 active">
                    <Routes>
                        {
                            isAuth ? <Route path="/" element={<Dashboard />} /> : <Route path="/" element={<SignIn />} />
                        }
                        <Route element={<ProtectedRoutes isAuth={isAuth} />}>
                            <Route path="/product/*" element={<Product />} />
                            <Route path="/customer/*" element={<Customer />} />
                            <Route path="/category/*" element={<Category />} />
                            <Route path="/order/*" element={<Order />} />
                            <Route path="/manufacture/*" element={<Manufacture />} />
                            <Route path="/admin/*" element={<Admin />} />
                            <Route path="/activity/*" element={<Activity />} />
                            <Route path="/restore/*" element={<Restore />} />
                            <Route path="/analytic/*" element={<Analytic />} />
                            <Route path="*" element={<NotFound />} />
                        </Route>
                    </Routes>
                </div>

            </Router>

            <ToastContainer
                position="top-right"
                autoClose={5000}
                hideProgressBar={false}
                newestOnTop={false}
                closeOnClick
                rtl={false}
                pauseOnFocusLoss
                draggable
                pauseOnHover
                theme="light"
            />
            {/* Same as */}
            <ToastContainer />
        </div>
    );
}

export default App;
