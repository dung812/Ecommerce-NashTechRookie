import React from 'react'
import PropTypes from 'prop-types'
import { useNavigate } from 'react-router-dom';
import { yupResolver } from '@hookform/resolvers/yup';
import { useForm } from 'react-hook-form';
import { categorySchema } from 'validateSchema/CategorySchema';

CategoryForm.propTypes = {}

function CategoryForm(props) {
    const { initialValues, isAddMode } = props;
    let navigate = useNavigate();

    const { register, handleSubmit, formState: { errors }, formState } = useForm({
        resolver: yupResolver(categorySchema),
    });

    const { isSubmitting } = formState;

    function HandleRedirect(redirectUrl) {
        navigate(redirectUrl);
    }

    return (
        <form onSubmit={handleSubmit(props.onSubmit)}>
            <div className="row">
                <div className="form-group col-md-12">
                    <label htmlFor='name' className='mb-2'>Category Name <span className="text-danger">*</span></label>
                    <input type="text" className="form-control" {...register("name")} defaultValue={initialValues.name} />
                    <small className='text-danger'>{errors.name?.message}</small>
                </div>
            </div>

            <div className='text-end mt-3'>
                <button type='submit' className={`btn btn-primary btn-custom-loading me-2 ${isSubmitting ? "is-loading" : ""}`}>
                    <div className="loader"></div>
                    <span>Submit</span>
                </button>
            </div>
        </form>
    )
}



export default CategoryForm
