import { addNewAdmin, handleEditAdmin } from 'features/Admin/AdminSlice';
import AdminForm from 'features/Admin/components/AdminForm';
import React from 'react'
import { Card } from 'react-bootstrap';
import { useDispatch, useSelector } from 'react-redux';
import { useNavigate, useParams } from "react-router-dom";
import { UTCWithoutHour } from 'utils';
import './AddEditPage.scss'

function AddEditPage(props) {
    let navigate = useNavigate();
    const dispatch = useDispatch();
    const { adminId } = useParams();
    const isAddMode = !adminId;
    let loadingSubmit = useSelector((state) => state.admins.loadingSubmit);
    let editedAdmin = useSelector((state) => state.admins.admins.find(admin => admin.adminId === parseInt(adminId)));

    const initialValues = isAddMode
        ? {
            userName: "",
            password: "",
            firstName: "",
            lastName: "",
            email: "",
            phone: "",
            birthday: "",
            gender: "",
            registeredDate: "",
            roleId: 1
        }
        : editedAdmin;

    const onSubmit = data => {
        if (isAddMode) {
            // Case add
            const newAdmin = {
                UserName: data.userName,
                Password: data.userName,
                FirstName: data.firstName,
                LastName: data.lastName,
                Email: data.email,
                Phone: data.phone,
                Birthday: UTCWithoutHour(data.birthday),
                RegisteredDate: UTCWithoutHour(data.registeredDate),
                Gender: data.gender,
                RoleName: data.role,
            }
            dispatch(addNewAdmin(newAdmin));
            if(!loadingSubmit){
                navigate('/admin')
            }

        }
        else {
            // Case edit
            const newInfo = {
                FirstName: data.firstName,
                LastName: data.lastName,
                Email: data.email,
                Phone: data.phone,
                Birthday: UTCWithoutHour(data.birthday),
                RegisteredDate: UTCWithoutHour(data.registeredDate),
                Gender: data.gender,
                RoleName: data.role,
            }
            dispatch(handleEditAdmin({id:parseInt(adminId), newInfo}));
            if(!loadingSubmit){
                navigate('/admin')
            }
        }
    }


    const HandleRedirect = (url) => {
        navigate(url)
    }




    return (
        <React.Fragment>
            <Card className='mb-3 border-0'>
                <Card.Body>
                    <div className="d-flex justify-content-between align-items-center">
                        <div>
                            <h4 className="font-weight-medium mb-1">Admin manage</h4>
                            <nav aria-label="breadcrumb">
                                <ol className="breadcrumb m-0">
                                    <li className="breadcrumb-item breadcrumb-item-custom"><a onClick={() => HandleRedirect("/")}>Home</a></li>
                                    <li className="breadcrumb-item breadcrumb-item-custom"><a onClick={() => HandleRedirect("/admin")}>Admin</a></li>
                                    <li className="breadcrumb-item active" aria-current="page">{isAddMode ? "Create" : "Edit"}</li>
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
                            <h5 className="card-title mb-3">Create a new admin</h5> :
                            <h5 className="card-title mb-3">Edit product ID: {initialValues.productId}</h5>
                    }

                    <AdminForm
                        initialValues={initialValues}
                        isAddMode={isAddMode}
                        onSubmit={onSubmit}
                    />
                </Card.Body>
            </Card>
        </React.Fragment>
    )
}


export default AddEditPage
