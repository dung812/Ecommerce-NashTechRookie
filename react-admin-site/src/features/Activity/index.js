import React from 'react'
import { Routes, Route } from "react-router-dom";
import NotFound from 'components/NotFound/Index';
import MainPage from './pages/Main';

function Activity(props) {
    return (
        <React.Fragment>
            <Routes>
                <Route path="/" element={<MainPage />} />
                <Route path="*" element={<NotFound />} />
            </Routes>
        </React.Fragment>
    )
}

export default Activity
