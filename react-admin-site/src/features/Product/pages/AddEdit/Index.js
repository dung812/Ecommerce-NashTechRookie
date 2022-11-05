import React, { useCallback, useEffect, useState } from 'react'
import { useNavigate, useParams } from "react-router-dom";
import { Card } from 'react-bootstrap';
import axios from 'axios';

import "./AddEdit.scss"
import ProductForm from 'features/Product/components/ProductForm/Index';
import { useDispatch, useSelector } from 'react-redux';
import { addNewProduct, updateProduct } from 'features/Product/ProductSlice';
import Swal from 'sweetalert2';

const AddEditPage = () => {
    let navigate = useNavigate(); // after submit form -> navigate("/product"); to redirect product page
    const { productId } = useParams();

    const dispatch = useDispatch();

    let admin = useSelector((state) => state.authAdmin.admin.info); // Get admin if to Admin info state

    let editedProduct = useSelector((state) => state.products.products.find(product => product.productId === parseInt(productId)));
    const isAddMode = !productId;
    const initialValues = isAddMode
        ? {
            productName: "",
            image: "",
            imageList: "",
            originalPrice: 1,
            promotionPercent: 1,
            description: "",
            quantity: 1,
            gender: "Men",
            adminId: 1,
            manufactureId: 1,
            catalogId: 1,
        }
        : editedProduct;


    const onSubmit = data => {

        // Get value
        const image = document.getElementById("input-image");
        const gender = document.querySelector("select[name='genderCategory']");
        const catalogId = document.querySelector("select[name='catalogId']");
        const manufactureId = document.querySelector("select[name='manufactureId']");

        if (isAddMode) {
            // Case add
            if (typeof image.files[0] === "undefined") {
                alert("Image field is blank")
                return;
            }

            const newProduct = {
                ProductName: data.productName,
                Image: image.files[0].name,
                ImageList: image.files[0].name.split(".")[0],
                OriginalPrice: data.originalPrice,
                PromotionPercent: data.promotionPercent,
                Description: data.description,
                Quantity: data.quantity,
                Gender: gender.value,
                AdminId: admin.adminId, // get token infor
                ManufactureId: parseInt(manufactureId.value),
                CatalogId: parseInt(catalogId.value)
            }
            axios.all([
                HandleSaveProductImage(),
                HandleSaveProductImageGallery("#input-image-gallery-1", image.files[0].name.split(".")[0], "1.jpg"),
                HandleSaveProductImageGallery("#input-image-gallery-2", image.files[0].name.split(".")[0], "2.jpg"),
                HandleSaveProductImageGallery("#input-image-gallery-3", image.files[0].name.split(".")[0], "3.jpg"),
                dispatch(addNewProduct(newProduct))
            ])
            .then(axios.spread((firstResponse, secondResponse, thirdResponse, fourResponse) => {

                navigate("/product");
            }))
            .catch(error => console.log(error));
        }
        else {
            // Handle update image
            const inputImage = document.querySelector("#input-image");
            const inputImageGallery1 = document.querySelector("#input-image-gallery-1");
            const inputImageGallery2 = document.querySelector("#input-image-gallery-2");
            const inputImageGallery3 = document.querySelector("#input-image-gallery-3");
 
            const updateInfoProduct = {
                ProductName: data.productName,
                Image: inputImage.value.length > 0 ? image.files[0].name : initialValues.image,
                ImageList: inputImage.value.length > 0 ? image.files[0].name.split(".")[0] : initialValues.imageList,
                OriginalPrice: data.originalPrice,
                PromotionPercent: data.promotionPercent,
                Description: data.description,
                Quantity: data.quantity,
                Gender: gender.value,
                AdminId: admin.adminId, // get token infor
                ManufactureId: parseInt(manufactureId.value),
                CatalogId: parseInt(catalogId.value)
            }
            updateInfoProduct.ProductId = productId;


            const HandleListAPI = [];
            HandleListAPI.push(dispatch(updateProduct(updateInfoProduct)))
            if (inputImage.value.length > 0)
                HandleListAPI.push(HandleUpdateProductImage(initialValues.image))
            if (inputImageGallery1.value.length > 0)
                HandleListAPI.push(HandleSaveProductImageGallery("#input-image-gallery-1", initialValues.imageList, "1.jpg"))
            if (inputImageGallery2.value.length > 0)
                HandleListAPI.push(HandleSaveProductImageGallery("#input-image-gallery-2", initialValues.imageList, "2.jpg"))
            if (inputImageGallery3.value.length > 0)
                HandleListAPI.push(HandleSaveProductImageGallery("#input-image-gallery-3", initialValues.imageList, "3.jpg"))

            Swal.fire({
                title: 'Do you want to save the changes?',
                showDenyButton: true,
                showCancelButton: true,
                confirmButtonText: 'Save',
                denyButtonText: `Don't save`,
            }).then((result) => {
                if (result.isConfirmed) {
                    axios.all(HandleListAPI).then(res => {
                        navigate("/product");
                    })
                } else if (result.isDenied) {
                    Swal.fire('Changes are not saved', '', 'info')
                }
            })


            // console.log(inputImageGallery1.value.length)    


            // dispatch(updateProduct(updateInfoProduct))

            // navigate("/product");

        }
    }

    function HandleSaveProductImage() {
        var formData = new FormData();
        var imagefile = document.querySelector('#input-image');
        formData.append("objFile", imagefile?.files[0]);

        axios.post('https://localhost:44324/api/Product/PostImage', formData, {
            headers: {
                'Content-Type': 'multipart/form-data'
            }
        })
    }
    function HandleUpdateProductImage(imageName) {
        var formData = new FormData();
        var imagefile = document.querySelector('#input-image');
        formData.append("objFile", imagefile?.files[0]);

        axios.post(`https://localhost:44324/api/Product/UpdatePostImage?imageName=${imageName}`, formData, {
            headers: {
                'Content-Type': 'multipart/form-data'
            }
        })
    }

    function HandleSaveProductImageGallery(seletorGallery, imageName, imageGalleryName) {
        var formData = new FormData();
        var imagefile = document.querySelector(seletorGallery);
        formData.append("objFile", imagefile?.files[0]);

        // Add gallery
        axios.post(`https://localhost:44324/api/Product/PostImageGallery?imageName=${imageName}&imageGalleryName=${imageGalleryName}`, formData, {
            headers: {
                'Content-Type': 'multipart/form-data'
            }
        })
    }

    function HandleRedirect(redirectUrl) {
        navigate(redirectUrl);
    }
    return (
        <React.Fragment>
            <Card className='mb-3 border-0'>
                <Card.Body>
                    <div className="d-flex justify-content-between align-items-center">
                        <div>
                            <h4 className="font-weight-medium mb-1">Product manage</h4>
                            <nav aria-label="breadcrumb">
                                <ol className="breadcrumb m-0">
                                    <li className="breadcrumb-item breadcrumb-item-custom"><a onClick={() => HandleRedirect("/")}>Home</a></li>
                                    <li className="breadcrumb-item breadcrumb-item-custom"><a onClick={() => HandleRedirect("/product")}>Product</a></li>
                                    <li className="breadcrumb-item active" aria-current="page">Create</li>
                                </ol>
                            </nav>
                        </div>
                        <div>Aug 19</div>
                    </div>

                </Card.Body>
            </Card>
            <Card className='border-0'>
                <Card.Body>
                    <h5 className="card-title mb-3">Create a new product</h5>
                    <ProductForm
                        initialValues={initialValues}
                        isAddMode={isAddMode}
                        onSubmit={onSubmit}
                    />
                </Card.Body>
            </Card>
        </React.Fragment>
    );
};



export default AddEditPage;
