import React from 'react'
import PropTypes from 'prop-types'
import { Routes, Route } from "react-router-dom";
import NotFound from 'components/NotFound/Index';
import MainPage from './pages/Main/Index';
import AddEditPage from './pages/AddEdit';

Manufacture.propTypes = {}

function Manufacture(props) {
    return (
        <div>
            <Routes>
                <Route path="/" element={<MainPage />} />
                <Route path="add" element={<AddEditPage />} />
                <Route path=":manufactureId" element={<AddEditPage />} />
                <Route path="*" element={<NotFound />} />
                
            </Routes>
        </div>
    )
}



export default Manufacture
