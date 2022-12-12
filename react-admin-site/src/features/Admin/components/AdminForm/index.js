import React, { useEffect } from 'react'
import { useNavigate } from 'react-router-dom';
import { Controller, useForm } from "react-hook-form";
import DatePicker from "react-datepicker";
import { yupResolver } from '@hookform/resolvers/yup';
import { adminAddSchema, adminEditSchema } from 'validateSchema/AdminSchema';
import { useSelector } from 'react-redux';

function AdminForm(props) {
    const { initialValues, isAddMode, onSubmit } = props;

    let loadingSubmit = useSelector((state) => state.admins.loadingSubmit);

    const role = ['Admin', 'Employee'];
    const gender = ['Men', 'Women'];

    let navigate = useNavigate();
    const { register, handleSubmit, formState: { errors }, formState, control, reset } = useForm({
        resolver: yupResolver(isAddMode ? adminAddSchema : adminEditSchema),
    });


    useEffect(() => {
        if (!isAddMode) {
            reset({
                birthday: new Date(initialValues?.birthday?.split("T")[0],),
                registeredDate: new Date(initialValues?.registeredDate?.split("T")[0],),
            });
        }

    }, [initialValues])


    return (
        <React.Fragment>
            <form onSubmit={handleSubmit(onSubmit)}>
                <div className="row">
                    {
                        isAddMode
                            ? (
                                <div className="form-group col-md-4">
                                    <label htmlFor='userName'>Username <span className="text-danger">*</span></label>
                                    <input type="text" className="form-control" {...register("userName")} defaultValue={initialValues.userName} />
                                    <small className='text-danger'>{errors.userName?.message}</small>
                                </div>
                            )
                            : (
                                <div className="form-group col-md-4">
                                    <label htmlFor='userName'>Username <span className="text-danger">*</span></label>
                                    <input type="text" className="form-control" defaultValue={initialValues.userName} disabled />
                                </div>
                            )
                    }

                    <div className="form-group col-md-4">
                        <label htmlFor='firstName'>First name <span className="text-danger">*</span></label>
                        <input type="text" className="form-control" {...register("firstName")} defaultValue={initialValues.firstName} />
                        <small className='text-danger'>{errors.firstName?.message}</small>
                    </div>
                    <div className="form-group col-md-4">
                        <label htmlFor='lastName'>Last name <span className="text-danger">*</span></label>
                        <input type="text" className="form-control" {...register("lastName")} defaultValue={initialValues.lastName} />
                        <small className='text-danger'>{errors.lastName?.message}</small>
                    </div>
                </div>
                {
                    isAddMode ?
                        (
                            <div className="row">
                                <div className="form-group col-md-6">
                                    <label htmlFor='password'>Password <span className="text-danger">*</span></label>
                                    <input type="password" className="form-control" {...register("password")} defaultValue={initialValues.password} />
                                    <small className='text-danger'>{errors.password?.message}</small>
                                </div>
                                <div className="form-group col-md-6">
                                    <label htmlFor='confirmPassword'>Confirm password <span className="text-danger">*</span></label>
                                    <input type="password" className="form-control" {...register("confirmPassword")} />
                                    <small className='text-danger'>{errors.confirmPassword?.message}</small>
                                </div>
                            </div>
                        )
                        : ""
                }

                <div className="row">
                    <div className="form-group col-md-12">
                        <label htmlFor='phone'>Phone <span className="text-danger">*</span></label>
                        <input type="text" className="form-control" {...register("phone")} defaultValue={initialValues.phone} />
                        <small className='text-danger'>{errors.phone?.message}</small>
                    </div>
                </div>
                <div className="row">
                    <div className="form-group col-md-4">
                        <label htmlFor='email'>Email <span className="text-danger">*</span></label>
                        <input type="email" className="form-control" {...register("email")} defaultValue={initialValues.email} />
                        <small className='text-danger'>{errors.email?.message}</small>
                    </div>
                    <div className="form-group col-md-4">
                        <label htmlFor='birthday'>Birthday <span className="text-danger">*</span></label>
                        <Controller
                            control={control}
                            name="birthday"
                            render={({ field }) => (
                                <DatePicker
                                    id="birthday"
                                    className="form-control date-picker"
                                    dateFormat="dd/MM/yyyy"
                                    showMonthDropdown
                                    showYearDropdown
                                    dropdownMode="select"
                                    autoComplete="off"
                                    onChange={(e) => field.onChange(e)}
                                    selected={field.value}
                                />
                            )}
                            rules={{
                                required: "Birthday date is required!",
                            }}
                        />
                        <small className='text-danger'>{errors.birthday?.message}</small>
                    </div>
                    <div className="form-group col-md-4">
                        <label htmlFor='registeredDate'>Join date <span className="text-danger">*</span></label>
                        <Controller
                            control={control}
                            name="registeredDate"
                            render={({ field }) => (
                                <DatePicker
                                    id="registeredDate"
                                    className="form-control date-picker"
                                    dateFormat="dd/MM/yyyy"
                                    showMonthDropdown
                                    showYearDropdown
                                    dropdownMode="select"
                                    autoComplete="off"
                                    onChange={(e) => field.onChange(e)}
                                    selected={field.value}
                                />
                            )}
                            rules={{
                                required: "Join date is required!",
                            }}
                        />
                        <small className='text-danger'>{errors.registeredDate?.message}</small>
                    </div>
                </div>

                <div className="row">
                    <div className="form-group col-md-6">
                        <label htmlFor='role'>Role <span className="text-danger">*</span></label>
                        <select className="form-select" id="role" {...register("role")}>
                            {
                                role.map((item, index) => {
                                    return (<option value={item} key={index}>{item}</option>)
                                })
                            }
                        </select>
                    </div>
                    <div className="form-group col-md-6">
                        <label htmlFor='gender'>Gender <span className="text-danger">*</span></label>
                        <select className="form-select" id="gender" {...register("gender")}>
                            {
                                gender.map((item, index) => {
                                    return (<option value={item} key={index}>{item}</option>)
                                })
                            }
                        </select>
                    </div>
                </div>
                <div className='text-start mt-3'>
                    <button type='submit' className={`btn btn-primary btn-custom-loading me-2 ${loadingSubmit ? "is-loading" : ""}`}>
                        <div className="loader"></div>
                        <span>Submit</span>
                    </button>
                    <button type='button' className='btn btn-outline-dark' onClick={() => navigate('/admin')} >Cancel</button>
                </div>
            </form>
        </React.Fragment>
    )
}


export default AdminForm
