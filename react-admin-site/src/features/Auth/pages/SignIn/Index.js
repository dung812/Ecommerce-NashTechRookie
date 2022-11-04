import React, { useEffect } from 'react';
import PropTypes from 'prop-types';
import axios from 'axios';
import './SignIn.scss'
// import { useFormAction } from 'react-router-dom';
import { useForm } from "react-hook-form";
import { loginSchema } from 'validateSchema/LoginShema';
import { yupResolver } from '@hookform/resolvers/yup';
import { useDispatch, useSelector } from 'react-redux';
import { authAdminLogin } from 'features/Auth/AuthSlice';
import { useNavigate } from 'react-router-dom';


const SignIn = () => {
    let navigate = useNavigate();
    const dispatch = useDispatch();
    let loading = useSelector((state) => state.authAdmin.loading);
    let admin = useSelector((state) => state.authAdmin.admin);

    const { register, handleSubmit, formState: { errors }, formState } = useForm({
        resolver: yupResolver(loginSchema),
    });
    const { isSubmitting } = formState;

    const onSubmit = data => {
        navigate("/");
        dispatch(authAdminLogin(data))
    };


    return (
        <div className='login-area'>
            <div className='login-form-wrap'>
                <h4 className='mb-3 text-center'>Sign in your account</h4>
                <form action="#" onSubmit={handleSubmit(onSubmit)}>
                    <div className="mb-3">
                        <label htmlFor='' className="form-label">Username</label>
                        <input type="text" className="form-control" {...register("UserName")} />
                        <small className='text-danger'>{errors.UserName?.message}</small>
                    </div>
                    <div className="mb-3">
                        <label htmlFor='' className="form-label">Password</label>
                        <input type="password" className="form-control" {...register("Password")} />
                        <small className='text-danger'>{errors.Password?.message}</small>
                    </div>
                    <div className="mb-3">
                        <div className="form-check">
                            <input className="form-check-input" type="checkbox" id="basic_checkbox_1" />
                            <label className="form-check-label" htmlFor="basic_checkbox_1">Remember me</label>
                        </div>
                    </div>
                    <button type='submit' className={`btn btn-primary w-100 btn-login btn-custom-loading me-2 ${loading ? "is-loading" : ""}`}>
                        <div className="loader"></div>
                        <span>Sign in</span>
                    </button>
                </form>
            </div>
        </div>
    );
};


export default SignIn;
