import React, { useEffect, useRef } from 'react'
import PropTypes from 'prop-types'
import { NavLink, Link } from 'react-router-dom';

import './Sidebar.scss';
import { useDispatch, useSelector } from 'react-redux';
import { logoutAccount } from 'features/Auth/AuthSlice';

function Sidebar(props) {
    const sidebar = useRef(null);

    let admin = useSelector((state) => state.authAdmin.admin.info);

    const dispatch = useDispatch();
    function Logout() {
        const action = logoutAccount();
        dispatch(action);
    }

    useEffect(() => {

        const navItems = document.querySelectorAll(".nav-items");

        navItems.forEach(item => item.addEventListener('click', () => {
            navItems.forEach(item => item.classList.remove('active'));
            item.classList.add('active');
        }))
    }, [])

    function HandleClickMenuSidebar(event) {
        sidebar.current !== null && sidebar.current.classList.toggle("open");
        menuBtnChange();

        // console.log(sidebar.current?.offsetWidth)

        const header = document.querySelector(".header");
        header.classList.toggle("active");
        const content = document.querySelector(".content-body");
        content.classList.toggle("active");

    }

    function HandleClickSearchBtn(event) {
        sidebar.current !== null && sidebar.current.classList.toggle("open");
        menuBtnChange();

        const header = document.querySelector(".header");
        header.classList.toggle("active");
        const content = document.querySelector(".content-body");
        content.classList.toggle("active");
    }

    function menuBtnChange() {
        let sidebar = document.querySelector(".sidebar");
        let closeBtn = document.querySelector("#btn");

        if (sidebar?.classList.contains("open")) {
            closeBtn?.classList.replace("bx-menu", "bx-menu-alt-right");//replacing the iocns class
        } else {
            closeBtn?.classList.replace("bx-menu-alt-right", "bx-menu");//replacing the iocns class
        }
    }


    return (
        <div className="sidebar open" ref={sidebar}>
            <div className="logo-details">
                <i className='bx bxl-c-plus-plus icon'></i>
                <div className="logo_name">NguyenDung</div>
                <i className='bx bx-menu-alt-right' id="btn" onClick={HandleClickMenuSidebar}></i>
            </div>
            <ul className="nav-list">
                <li>
                    <i className='bx bx-search' onClick={HandleClickSearchBtn}></i>
                    <input type="text" placeholder="Search..." />
                    <span className="tooltip">Search</span>
                </li>
                <li>
                    <Link to="/" className='nav-items'>
                        <i className='bx bx-bar-chart-square'></i>
                        <span className="links_name">Dashboard</span>
                    </Link>
                    <span className="tooltip">Dashboard</span>
                </li>
                {
                    admin?.roleName === "Admin" &&
                    <li>
                        <Link to="/product" className='nav-items'>
                            <i className='bx bx-store-alt'></i>
                            <span className="links_name">Product</span>
                        </Link>
                        <span className="tooltip">Product</span>
                    </li>
                }
                {/* <li>
                    <NavLink to="/product" className='nav-items'>
                        <i className='bx bx-store-alt'></i>
                        <span className="links_name">Product</span>
                    </NavLink>
                    <span className="tooltip">Product</span>
                </li> */}
                <li>
                    <Link to="/category" className='nav-items'>
                        <i className='bx bx-category'></i>
                        <span className="links_name">Category</span>
                    </Link>
                    <span className="tooltip">Category</span>
                </li>
                <li>
                    <Link to="/manufacture" className='nav-items'>
                        <i className='bx bx-library'></i>
                        <span className="links_name">Manufacture</span>
                    </Link>
                    <span className="tooltip">Manufacture</span>
                </li>
                <li>
                    <Link to="/customer" className='nav-items'>
                        <i className='bx bx-user'></i>
                        <span className="links_name">Customer</span>
                    </Link>
                    <span className="tooltip">Customer</span>
                </li>
                <li>
                    <Link to="/admin" className='nav-items'>
                        <i className='bx bx-user-circle'></i>
                        <span className="links_name">Admin</span>
                    </Link>
                    <span className="tooltip">Admin</span>
                </li>
                <li>
                    <Link to="/activity" className='nav-items'>
                        <i className='bx bx-street-view'></i>
                        <span className="links_name">Activities admin</span>
                    </Link>
                    <span className="tooltip">Activities admin</span>
                </li>
                <li>
                    <Link to="/order" className='nav-items'>
                        <i className='bx bx-receipt'></i>
                        <span className="links_name">Order</span>
                    </Link>
                    <span className="tooltip">Order</span>
                </li>
                <li>
                    <Link to="/analytic" className='nav-items'>
                        <i className='bx bx-line-chart' ></i>
                        <span className="links_name">Analytics</span>
                    </Link>
                    <span className="tooltip">Analytics</span>
                </li>
                <li>
                    <Link to="/report" className='nav-items'>
                        <i className='bx bx-line-chart' ></i>
                        <span className="links_name">Report</span>
                    </Link>
                    <span className="tooltip">Report</span>
                </li>
                <li>
                    <Link to="/restore" className='nav-items'>
                        <i className='bx bx-minus-back'></i>
                        <span className="links_name">Restore</span>
                    </Link>
                    <span className="tooltip">Restore</span>
                </li>
                <li className="profile">
                    <div className="profile-details">
                        <img src={`https://localhost:44324/images/avatars/${admin?.avatar ? admin?.avatar : "avatar.jpg"}`} alt="profileImg" />
                        <div className="name_job">
                            <div className="name">{`${admin?.firstName} ${admin?.lastName}`}</div>
                            <div className="job">Role: {admin?.roleName}</div>
                        </div>
                    </div>
                    <i className='bx bx-log-out' id="log_out" onClick={() => Logout()}></i>
                </li>
            </ul>
        </div>

    )
}

Sidebar.propTypes = {}

export default Sidebar
