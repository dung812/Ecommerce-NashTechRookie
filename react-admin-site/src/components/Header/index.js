import React from 'react'
import PropTypes from 'prop-types'
import './Header.scss'
import { Dropdown } from 'react-bootstrap';
import { useDispatch } from 'react-redux';
import { logoutAccount } from 'features/Auth/AuthSlice';

Header.propTypes = {}

function Header(props) {
    const dispatch = useDispatch();

    function Logout() {
        const action = logoutAccount();
		dispatch(action);
    }

    return (
        <div className='header px-4 py-3'>
            <div className="d-flex justify-content-between align-items-center">
                <div>
                    <form action="" className='d-flex align-items-center position-relative' id='my-form-search'>
                        <button type='submit' className='border-0 bg-transparent fs-4 position-absolute btn-search-custom'>
                            <i className='bx bx-search'></i>
                        </button>
                        <input type="search" className='fs-5 form-control' placeholder='Search...' />
                    </form>
                </div>
                <div className='d-flex align-items-center gap-5'>

                    <Dropdown>
                        <Dropdown.Toggle className='custom-dropdown position-relative'>
                            <i className='bx bx-bell fs-4'></i>   
                            <span className="position-absolute top-0 start-100 translate-middle badge rounded-pill bg-danger">99+</span>
                        </Dropdown.Toggle>

                        <Dropdown.Menu className='p-0'>
                            <Dropdown.Item href="#/action-1" className='notify-item d-flex justify-content-between align-items-center'>
                                <span className='notify-icon user'><i className='bx bx-user'></i></span>
                                <p className='m-0 me-2'><strong>Nguyen Dung</strong> has added a <strong>customer</strong>...</p>
                                <span className="notify-time text-secondary">3:20 am</span>
                            </Dropdown.Item>
                            <Dropdown.Item href="#/action-1" className='notify-item d-flex justify-content-between align-items-center'>
                                <span className='notify-icon order'><i className='bx bx-cart'></i></span>
                                <p className='m-0 me-2'><strong>Nguyen Dung</strong>  purchased new order...</p>
                                <span className="notify-time text-secondary">3:20 am</span>
                            </Dropdown.Item>

                            <Dropdown.Item href="#/action-1" className='text-center py-2'>
                                <p className='m-0'>See all notifications <i className='bx bx-right-arrow-alt'></i></p>
                            </Dropdown.Item>

                        </Dropdown.Menu>
                    </Dropdown>

                    <Dropdown>
                        <Dropdown.Toggle className="d-flex align-items-center gap-1 custom-dropdown">
                            <img src="https://i.bloganchoi.com/bloganchoi.com/wp-content/uploads/2022/02/avatar-trang-y-nghia.jpeg?fit=512%2C20000&quality=95&ssl=1" className='img-fluid avatar' alt="profileImg" />
                            <div className="name_job">
                                <div className="name">Nguyen Dung</div>
                            </div>
                        </Dropdown.Toggle>

                        <Dropdown.Menu>
                            <Dropdown.Item href="/profile"><i className='bx bx-user'></i> Profile</Dropdown.Item>
                            <Dropdown.Item href="/change-password"><i className='bx bx-eraser'></i> Change password</Dropdown.Item>
                            <Dropdown.Item onClick={() => Logout()}><i className='bx bx-dock-left'></i> Logout</Dropdown.Item>
                        </Dropdown.Menu>
                    </Dropdown>

                </div>
            </div>
        </div>
    )
}



export default Header
