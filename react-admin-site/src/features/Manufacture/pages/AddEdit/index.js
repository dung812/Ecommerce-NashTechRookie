import React from 'react';
import PropTypes from 'prop-types';
import { useNavigate, useParams } from 'react-router-dom';
import { useDispatch, useSelector } from 'react-redux';
import { Card } from 'react-bootstrap';
import ManufactureForm from 'features/Manufacture/components/ManufactureForm';
import { addNewManufacture, updateManufacture } from 'features/Manufacture/ManufactureSlice';
import axios from 'axios';
import Swal from 'sweetalert2';


const AddEditPage = () => {
    let navigate = useNavigate(); // after submit form -> navigate("/product"); to redirect product page
    const { manufactureId } = useParams();
    const dispatch = useDispatch();

    let editedManufacture = useSelector((state) => state.manufactures.manufactures.find(manufacture => manufacture.manufactureId === parseInt(manufactureId)));


    const isAddMode = !manufactureId;
    const initialValues = isAddMode
        ? {
            name: "",
            logo: ""
        }
        : editedManufacture;

    function HandleRedirect(redirectUrl) {
        navigate(redirectUrl);
    }

    const onSubmit = data => {
        // Get value
        const image = document.getElementById("input-image");
        if (isAddMode) {
            // Case add
            const newManufacture = {
                Name: data.name,
                Logo: image.files[0].name
            }
            axios.all([
                HandleSaveManufactureImage(),
                dispatch(addNewManufacture(newManufacture))
            ])
            .then(axios.spread((firstResponse, secondResponse, thirdResponse, fourResponse) => {
                navigate("/manufacture");
            }))
            .catch(error => console.log(error));

        }
        else {
            const editManufacture = {
                Name: data.name,
                Logo: image.value.length > 0 ? image.files[0].name : initialValues.logo
            }
            editManufacture.ManufactureId = manufactureId;
            Swal.fire({
                title: 'Do you want to save the changes?',
                showDenyButton: true,
                showCancelButton: true,
                confirmButtonText: 'Save',
                denyButtonText: `Don't save`,
            }).then((result) => {
                if (result.isConfirmed) {
                    if (image.value.length > 0)
                        HandleSaveManufactureImage();

                    navigate("/manufacture");
                    dispatch(updateManufacture(editManufacture))
                    
                } else if (result.isDenied) {
                    Swal.fire('Changes are not saved', '', 'info')
                }
            })
        }
    }

    function HandleSaveManufactureImage() {
        var formData = new FormData();
        var imagefile = document.querySelector('#input-image');
        formData.append("objFile", imagefile?.files[0]);

        axios.post('https://localhost:44324/api/SaveImage/SaveImageManufacture', formData, {
            headers: {
                'Content-Type': 'multipart/form-data'
            }
        })
    }

    return (
        <React.Fragment>
            <Card className='mb-3 border-0'>
                <Card.Body>
                    <div className="d-flex justify-content-between align-items-center">
                        <div>
                            <h4 className="font-weight-medium mb-1">Manufacture manage</h4>
                            <nav aria-label="breadcrumb">
                                <ol className="breadcrumb m-0">
                                    <li className="breadcrumb-item breadcrumb-item-custom"><a onClick={() => HandleRedirect("/")}>Home</a></li>
                                    <li className="breadcrumb-item breadcrumb-item-custom"><a onClick={() => HandleRedirect("/manufacture")}>Manufacture</a></li>
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
                            <h5 className="card-title mb-3">Edit manufacture ID: {manufactureId}</h5>
                    }

                    <ManufactureForm
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
