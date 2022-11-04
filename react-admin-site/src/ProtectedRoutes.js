import React from 'react'
import { Navigate, Outlet } from 'react-router-dom';
import SignIn from 'features/Auth/pages/SignIn/Index';


const ProtectedRoutes = props => {
    return props.isAuth ? <Outlet /> : <SignIn /> 
}



export default ProtectedRoutes