import React, { useEffect, useRef } from 'react'
import PropTypes from 'prop-types'
import { NavLink } from 'react-router-dom';

import './Sidebar.scss';

function Sidebar(props) {
    const sidebar = useRef(null);

    useEffect(() => {
        const navItems = document.querySelectorAll(".nav-items");

        navItems.forEach(item => item.addEventListener('click', () => {
            navItems.forEach(item => item.classList.remove('active'));
            item.classList.add('active');
        }))
    },[])

    function HandleClickMenuSidebar(event) {
        sidebar.current !== null && sidebar.current.classList.toggle("open");
        menuBtnChange();

        console.log(sidebar.current?.offsetWidth)

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

        if(sidebar?.classList.contains("open")){
            closeBtn?.classList.replace("bx-menu", "bx-menu-alt-right");//replacing the iocns class
        }else {
            closeBtn?.classList.replace("bx-menu-alt-right","bx-menu");//replacing the iocns class
        }
    }


    return (
        <div className="sidebar" ref={sidebar}>
            <div className="logo-details">
                <i className='bx bxl-c-plus-plus icon'></i>
                <div className="logo_name">NguyenDung</div>
                <i className='bx bx-menu' id="btn" onClick={HandleClickMenuSidebar}></i>
            </div>
            <ul className="nav-list">
                <li>
                    <i className='bx bx-search' onClick={HandleClickSearchBtn}></i>
                    <input type="text" placeholder="Search..." />
                    <span className="tooltip">Search</span>
                </li>
                <li>
                    <NavLink to="/" className='nav-items'>
                        <i className='bx bx-bar-chart-square'></i>
                        <span className="links_name">Dashboard</span>
                    </NavLink>
                    <span className="tooltip">Dashboard</span>
                </li>
                <li>
                    <NavLink to="/product" className='nav-items'>
                        <i className='bx bx-store-alt'></i>
                        <span className="links_name">Product</span>
                    </NavLink>
                    <span className="tooltip">Product</span>
                </li>
                <li>
                    <NavLink to="/category" className='nav-items'>
                        <i className='bx bx-category'></i>
                        <span className="links_name">Category</span>
                    </NavLink>
                    <span className="tooltip">Category</span>
                </li>
                <li>
                    <NavLink to="/manufacture" className='nav-items'>
                        <i className='bx bx-library'></i>
                        <span className="links_name">Manufacture</span>
                    </NavLink>
                    <span className="tooltip">Manufacture</span>
                </li>
                <li>
                    <NavLink to="/customer" className='nav-items'>
                        <i className='bx bx-user'></i>
                        <span className="links_name">Customer</span>
                    </NavLink>
                    <span className="tooltip">Customer</span>
                </li>
                <li>
                    <NavLink to="/admin" className='nav-items'>
                        <i className='bx bx-user-pin'></i>
                        <span className="links_name">Admin</span>
                    </NavLink>
                    <span className="tooltip">Admin</span>
                </li>
                <li>
                    <NavLink to="/order" className='nav-items'>
                        <i className='bx bx-cart-alt' ></i>
                        <span className="links_name">Order</span>
                    </NavLink>
                    <span className="tooltip">Order</span>
                </li>
                <li>
                    <NavLink to="/analytics" className='nav-items'>
                        <i className='bx bx-pie-chart-alt-2' ></i>
                        <span className="links_name">Analytics</span>
                    </NavLink>
                    <span className="tooltip">Analytics</span>
                </li>
                <li>
                    <NavLink to="/restore" className='nav-items'>
                        <i className='bx bx-minus-back'></i>
                        <span className="links_name">Restore</span>
                    </NavLink>
                    <span className="tooltip">Restore</span>
                </li>
                <li className="profile">
                    <div className="profile-details">
                        <img src="https://i.bloganchoi.com/bloganchoi.com/wp-content/uploads/2022/02/avatar-trang-y-nghia.jpeg?fit=512%2C20000&quality=95&ssl=1" alt="profileImg" />
                            <div className="name_job">
                                <div className="name">Nguyen Dung</div>
                                <div className="job">Role: Admin</div>
                            </div>
                    </div>
                    <i className='bx bx-log-out' id="log_out" ></i>
                </li>
            </ul>
        </div>

    )
}

Sidebar.propTypes = {}

export default Sidebar
