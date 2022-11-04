import React from 'react'
import PropTypes from 'prop-types'
import { Routes, Route, Link } from "react-router-dom";
import MainPage from './pages/Main/Index';
import NotFound from 'components/NotFound/Index';

function Dashboard(props) {
    return (
        <div>
            <Routes>
                <Route path="/" element={<MainPage />} />
                <Route path="*" element={<NotFound />} />
            </Routes>
        </div>
    )
}

Dashboard.propTypes = {}

export default Dashboard
