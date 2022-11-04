import React from 'react'
import PropTypes from 'prop-types'
import { Routes, Route } from "react-router-dom";
import NotFound from 'components/NotFound/Index';
import MainPage from './pages/Main/Index';

Category.propTypes = {}

function Category(props) {
    return (
        <div>
            <Routes>
                <Route path="/" element={<MainPage />} />
                <Route path="*" element={<NotFound />} />
            </Routes>
        </div>
    )
}



export default Category
