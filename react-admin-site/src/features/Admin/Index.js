import React from 'react'
import { Routes, Route } from "react-router-dom";
import NotFound from 'components/NotFound/Index';
import MainPage from './pages/Main/Index';
import AddEditPage from './pages/AddEditPage';

function Admin(props) {
    return (
        <div>
            <Routes>
                <Route path="/" element={<MainPage />} />
                <Route path="add" element={<AddEditPage />} />
                <Route path=":adminId" element={<AddEditPage />} />
                <Route path="*" element={<NotFound />} />
            </Routes>
        </div>
    )
}



export default Admin