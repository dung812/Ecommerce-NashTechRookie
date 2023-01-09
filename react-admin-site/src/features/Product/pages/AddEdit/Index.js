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
    let description = useSelector((state) => state.products.description);
    const isAddMode = !productId;
    const initialValues = isAddMode
        ? {
            productName: "",
            imageFileName: "",
            imageName: "",
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
        const imageGallery1 = document.getElementById("input-image-gallery-1");
        const imageGallery2 = document.getElementById("input-image-gallery-2");
        const imageGallery3 = document.getElementById("input-image-gallery-3");

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
                ImageFileName: image.files[0].name,
                ImageName: image.files[0].name.split(".")[0],
                OriginalPrice: data.originalPrice,
                PromotionPercent: data.promotionPercent,
                Description: description,
                Quantity: data.quantity,
                Gender: gender.value,
                AdminId: admin.adminId, // get token infor
                ManufactureId: parseInt(manufactureId.value),
                CatalogId: parseInt(catalogId.value),
                ImageNameGallery1: imageGallery1.files[0].name,
                ImageNameGallery2: imageGallery2.files[0].name,
                ImageNameGallery3: imageGallery3.files[0].name,
            }
            axios.all([
                HandleSaveProductImage(),
                HandleSaveProductImageGallery("#input-image-gallery-1", image.files[0].name.split(".")[0], "1"),
                HandleSaveProductImageGallery("#input-image-gallery-2", image.files[0].name.split(".")[0], "2"),
                HandleSaveProductImageGallery("#input-image-gallery-3", image.files[0].name.split(".")[0], "3"),
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
                ImageFileName: image.value.length > 0 ? image.files[0].name : initialValues.imageFileName,
                ImageName: image.value.length > 0 ? image.files[0].name.split(".")[0] : initialValues.imageName,
                OriginalPrice: data.originalPrice,
                PromotionPercent: data.promotionPercent,
                Description: description,
                Quantity: data.quantity,
                Gender: gender.value,
                AdminId: admin.adminId, // get token infor
                ManufactureId: parseInt(manufactureId.value),
                CatalogId: parseInt(catalogId.value),
                ImageNameGallery1: imageGallery1.value.length > 0 ? imageGallery1.files[0].name : initialValues.imageNameGallery1,
                ImageNameGallery2: imageGallery2.value.length > 0 ? imageGallery2.files[0].name : initialValues.imageNameGallery2,
                ImageNameGallery3: imageGallery3.value.length > 0 ? imageGallery3.files[0].name : initialValues.imageNameGallery3,
            }
            updateInfoProduct.ProductId = productId;

            Swal.fire({
                title: 'Do you want to save the changes?',
                showDenyButton: true,
                showCancelButton: true,
                confirmButtonText: 'Save',
                denyButtonText: `Don't save`,
            }).then((result) => {
                if (result.isConfirmed) {

                    if (inputImage.value.length > 0)
                        HandleUpdateProductImage(initialValues.imageFileName, inputImageGallery1, inputImageGallery2, inputImageGallery3, updateInfoProduct);
                    else {
                        if (inputImageGallery1.value.length > 0)
                            HandleUpdateProductImageGallery("#input-image-gallery-1", updateInfoProduct.ImageName, "1", initialValues.imageNameGallery1)

                        if (inputImageGallery2.value.length > 0)
                            HandleUpdateProductImageGallery("#input-image-gallery-2", updateInfoProduct.ImageName, "2", initialValues.imageNameGallery1)

                        if (inputImageGallery3.value.length > 0)
                            HandleUpdateProductImageGallery("#input-image-gallery-3", updateInfoProduct.ImageName, "3", initialValues.imageNameGallery1)

                        navigate("/product");
                    }

                    dispatch(updateProduct(updateInfoProduct))
                    

                } else if (result.isDenied) {
                    Swal.fire('Changes are not saved', '', 'info')
                }
            })

        }
    }
    

    function HandleSaveProductImage() {
        var formData = new FormData();
        var imagefile = document.querySelector('#input-image');
        formData.append("objFile", imagefile?.files[0]);

        axios.post(`http://ntdung812-001-site1.btempurl.com/api/SaveImage/SaveImageProduct`, formData, {
            headers: {
                'Content-Type': 'multipart/form-data'
            }
        })
    }
    function HandleSaveProductImageGallery(seletorGallery, imageName, indexImageGallery) {
        var formData = new FormData();
        var imagefile = document.querySelector(seletorGallery);
        formData.append("objFile", imagefile?.files[0]);

        // Add gallery
        axios.post(`http://ntdung812-001-site1.btempurl.com/api/SaveImage/SaveImageProductGallery?imageName=${imageName}&indexImageGallery=${indexImageGallery}`, formData, {
            headers: {
                'Content-Type': 'multipart/form-data'
            }
        })
    }


    function HandleUpdateProductImage(imageFileName, inputImageGallery1, inputImageGallery2, inputImageGallery3, updateInfoProduct) {
        var formData = new FormData();
        var imagefile = document.querySelector('#input-image');
        formData.append("objFile", imagefile?.files[0]);

        axios.post(`http://ntdung812-001-site1.btempurl.com/api/SaveImage/UpdateImageProduct?imageFileName=${imageFileName}`, formData, {
            headers: {
                'Content-Type': 'multipart/form-data'
            }
        })
            .then(res => {
                if (inputImageGallery1.value.length > 0)
                    HandleUpdateProductImageGallery("#input-image-gallery-1", updateInfoProduct.ImageName, "1", initialValues.imageNameGallery1)

                if (inputImageGallery2.value.length > 0)
                    HandleUpdateProductImageGallery("#input-image-gallery-2", updateInfoProduct.ImageName, "2", initialValues.imageNameGallery1)

                if (inputImageGallery3.value.length > 0)
                    HandleUpdateProductImageGallery("#input-image-gallery-3", updateInfoProduct.ImageName, "3", initialValues.imageNameGallery1)

                navigate("/product");
            })
    }

    function HandleUpdateProductImageGallery(seletorGallery, imageName, indexImageGallery, oldImageGalleryFileName) {
        var formDataGallery = new FormData();
        var imagefile = document.querySelector(seletorGallery);

        formDataGallery.append("objGalleryFile", imagefile?.files[0]);

        // Add gallery
        axios.post(`http://ntdung812-001-site1.btempurl.com/api/SaveImage/UpdateImageProductGallery?imageName=${imageName}&indexImageGallery=${indexImageGallery}&oldImageGalleryFileName=${oldImageGalleryFileName}`,
            formDataGallery,
            {
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
                    {
                        isAddMode ? 
                        <h5 className="card-title mb-3">Create a new product</h5> :
                        <h5 className="card-title mb-3">Edit product ID: {initialValues.productId}</h5> 
                    }
                    
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
