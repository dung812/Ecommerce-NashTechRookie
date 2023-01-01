import React, { useEffect, useReducer, useState } from 'react'
import PropTypes from 'prop-types'
import { useForm } from "react-hook-form";
import { yupResolver } from '@hookform/resolvers/yup';
import { productSchema } from 'validateSchema/ProductSchema';
import { useNavigate } from 'react-router-dom';
import axios from 'axios';
import { useDispatch, useSelector } from 'react-redux';
import { fetchCategories } from 'features/Category/CategorySlice';
import { fetchManufactures } from 'features/Manufacture/ManufactureSlice';
import ReactQuill from 'react-quill';
import { setDescription } from 'features/Product/ProductSlice';


ProductForm.propTypes = {}

function ProductForm(props) {
    const { initialValues, isAddMode } = props;
    let navigate = useNavigate();

    const dispatch = useDispatch();
    let catalogs = useSelector((state) => state.categories.categories);
    let manufactures = useSelector((state) => state.manufactures.manufactures);

    let loadingProduct = useSelector((state) => state.products.loading);
    let loadingCategory = useSelector((state) => state.categories.loading);
    let loadingManufacture = useSelector((state) => state.manufactures.loading);

    const [valueEditor, setValueEditor] = useState(initialValues.description);

    useEffect(() => {
        dispatch(setDescription(valueEditor))
    }, [valueEditor])


    useEffect(() => {
        dispatch(fetchCategories());
        dispatch(fetchManufactures());
    }, [])

    useEffect(() => {
        const catalogOptions = document.querySelectorAll("select[name='catalogId'] option")
        catalogOptions.forEach(item => {
            if (parseInt(item.value) === initialValues.catalogId)
                item.setAttribute("selected", "selected")
        })
    }, [loadingCategory])

    useEffect(() => {
        const manufactureOptions = document.querySelectorAll("select[name='manufactureId'] option")
        manufactureOptions.forEach(item => {
            if (parseInt(item.value) === initialValues.manufactureId)
                item.setAttribute("selected", "selected")
        })
    }, [loadingManufacture])


    useEffect(() => {
        // Preview main product image
        const inputImage = document.getElementById("input-image");
        inputImage && inputImage.addEventListener("change", function (e) {
            readURL(inputImage, "#product-image_main");
        })

        // Preview gallery product image
        const inputImageGallerys = document.querySelectorAll(".input-image-gallery");
        inputImageGallerys.forEach(item => item.addEventListener("change", function (e) {
            switch (Number(e.target.dataset.gallery)) {
                case 1:
                    readURL(e.target, "#product-image_gallery1");
                    break;
                case 2:
                    readURL(e.target, "#product-image_gallery2");
                    break;
                case 3:
                    readURL(e.target, "#product-image_gallery3");
                    break;
            }
        }))

        // Fill value in selects gender  category
        const genderOptions = document.querySelectorAll("select[name='genderCategory'] option")
        genderOptions.forEach(item => {
            if (item.value === initialValues.gender)
                item.setAttribute("selected", "selected")
        })

        dispatch(fetchCategories());

    }, []);

    function readURL(input, imageSeletor) {
        if (input.files && input.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                const image = document.querySelector(imageSeletor);
                image && image.setAttribute('src', e.target.result);
            }
            reader.readAsDataURL(input.files[0]);
        }
    };

    const { register, handleSubmit, formState: { errors }, formState } = useForm({
        resolver: yupResolver(productSchema),
    });


    function HandleRedirect(redirectUrl) {
        navigate(redirectUrl);
    }

    return (
        <form onSubmit={handleSubmit(props.onSubmit)}>
            <div className="row">
                <div className="form-group col-md-12">
                    <label htmlFor='name'>Product Name <span className="text-danger">*</span></label>
                    <input type="text" className="form-control" {...register("productName")} defaultValue={initialValues.productName} />
                    <small className='text-danger'>{errors.productName?.message}</small>
                </div>
                <div className="form-group col-md-4">
                    <label htmlFor='price'>Price <span className="text-danger">*</span></label>
                    <input type="number" className="form-control" {...register("originalPrice")} defaultValue={initialValues.originalPrice} />
                    <small className='text-danger'>{errors.originalPrice?.message}</small>
                </div>
                <div className="form-group col-md-4">
                    <label htmlFor=''>Promotion percent <span className="text-danger">*</span></label>
                    <input type="number" className="form-control" {...register("promotionPercent")} defaultValue={initialValues.promotionPercent} />
                    <small className='text-danger'>{errors.promotionPercent?.message}</small>
                </div>
                <div className="form-group col-md-4">
                    <label htmlFor=''>Quantity <span className="text-danger">*</span></label>
                    <input type="number" className="form-control" {...register("quantity")} defaultValue={initialValues.quantity} />
                    <small className='text-danger'>{errors.quantity?.message}</small>
                </div>
                <div className="form-group col-md-4">
                    <label htmlFor=''>Gender Category <span className="text-danger">*</span></label>
                    <select className="form-control" id="GenderCategory" name='genderCategory'>
                        <option value="Men">Men Shoes</option>
                        <option value="Women">Women Shoes</option>
                    </select>
                </div>
                <div className="form-group col-md-4">
                    <label htmlFor=''>Shoes Category <span className="text-danger">*</span></label>
                    <select className="form-control" id="CatalogId" name='catalogId'>
                        {
                            catalogs.map(item => {
                                return (<option value={item.catalogId} key={item.catalogId}>{item.name}</option>)
                            })
                        }
                    </select>
                </div>
                <div className="form-group col-md-4">
                    <label htmlFor=''>Manufacture <span className="text-danger">*</span></label>
                    <select className="form-control" id="ManufactureId" name='manufactureId'>
                        {
                            manufactures.map(item => {
                                return (<option value={item.manufactureId} key={item.manufactureId}>{item.name}</option>)
                            })
                        }
                    </select>
                </div>
                {/* <div className="form-group col-md-12">
                    <label htmlFor=''>Description <span className="text-danger">*</span></label>
                    <textarea className="form-control" rows={5} {...register("description")} defaultValue={initialValues.description}></textarea>
                    <small className='text-danger'>{errors.description?.message}</small>
                </div> */}
                <div className="form-group col-md-12">
                    <label htmlFor=''>Description <span className="text-danger">*</span></label>
                    <ReactQuill theme="snow" value={valueEditor} {...register("description")}  onChange={setValueEditor} />
                </div>
                <div className="form-group col-md-12">
                    <label htmlFor=''>Image <span className="text-danger">*</span></label>
                    <img src={!isAddMode ? `https://localhost:44324/images/products/Image/${initialValues.imageFileName}` : ""} width="30%" className='img-fluid d-block mx-auto mb-2' id='product-image_main' />
                    <input type="file" className="form-control" id='input-image' accept="image/*" name="image" />
                </div>
                <div className="form-group col-md-4">
                    <label htmlFor=''>Image gallery 1 <span className="text-danger">*</span></label>
                    <img src={!isAddMode ? `https://localhost:44324/images/products/ImageList/${initialValues.imageName}/1/${initialValues.imageNameGallery1}` : ""} width="80%" className='img-fluid d-block mx-auto mb-2' id='product-image_gallery1' />
                    <input type="file" className="form-control input-image-gallery" id='input-image-gallery-1' data-gallery="1" />

                </div>
                <div className="form-group col-md-4">
                    <label htmlFor=''>Image gallery 2 <span className="text-danger">*</span></label>
                    <img src={!isAddMode ? `https://localhost:44324/images/products/ImageList/${initialValues.imageName}/2/${initialValues.imageNameGallery2}` : ""} width="80%" className='img-fluid d-block mx-auto mb-2' id='product-image_gallery2' />
                    <input type="file" className="form-control input-image-gallery" id='input-image-gallery-2' data-gallery="2" />
                </div>
                <div className="form-group col-md-4">
                    <label htmlFor=''>Image gallery 3 <span className="text-danger">*</span></label>
                    <img src={!isAddMode ? `https://localhost:44324/images/products/ImageList/${initialValues.imageName}/3/${initialValues.imageNameGallery3}` : ""} width="80%" className='img-fluid d-block mx-auto mb-2' id='product-image_gallery3' />
                    <input type="file" className="form-control input-image-gallery" id='input-image-gallery-3' data-gallery="3" />
                </div>
            </div>
            <div className='text-start mt-3'>
                <button type='submit' className={`btn btn-primary btn-custom-loading me-2 ${loadingProduct ? "is-loading" : ""}`}>
                    <div className="loader"></div>
                    <span>Submit</span>
                </button>
                <button type='button' className='btn btn-outline-dark' onClick={() => HandleRedirect("/product")} >Cancel</button>
            </div>
        </form>
    )
}


export default ProductForm
