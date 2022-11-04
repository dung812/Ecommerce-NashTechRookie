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
                <div className="content-body py-5 px-4">
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
                            <Route path="*" element={<NotFound />} />
                        </Route>
                    </Routes>
                </div>

            </Router>
        </div>
    );
}

export default App;
