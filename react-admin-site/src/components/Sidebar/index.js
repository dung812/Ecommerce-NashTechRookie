import React, { useEffect, useRef } from 'react'
import PropTypes from 'prop-types'
import { NavLink, Link } from 'react-router-dom';

import './Sidebar.scss';
import { useDispatch, useSelector } from 'react-redux';
import { logoutAccount } from 'features/Auth/AuthSlice';
import { useState } from 'react';
import { initPages } from 'routes';

function Sidebar(props) {
    const sidebar = useRef(null);

    let admin = useSelector((state) => state.authAdmin.admin.info);
    let loading = useSelector((state) => state.authAdmin.loading);

    let [pages, setPages] = useState(initPages);

    useEffect(() => {
        setPages(initPages)
        if (admin) {
            if (admin.roleName === 'Employee') {
                setPages([...pages.filter(item => 
                        item.link !== '/admin' && 
                        item.link !== '/activity' &&
                        item.link !== '/product' &&
                        item.link !== '/category' &&
                        item.link !== '/manufacture' &&
                        item.link !== '/restore' 
                    )]
                );
            }
        }
    }, [admin])

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


    function HandleActive(e) {
        const navItems = document.querySelectorAll('.nav-items');
        navItems.forEach(item =>  item.classList.remove('active'))
        e.currentTarget.classList.add('active');

    }
    return (
        <div className="sidebar open" ref={sidebar}>
            <div className="logo-details">
                <i className='bx bx-store-alt icon'></i>
                <div className="logo_name">Footwear</div>
                <i className='bx bx-menu-alt-right' id="btn" onClick={HandleClickMenuSidebar}></i>
            </div>
            <ul className="nav-list">
                <li>
                    <i className='bx bx-search' onClick={HandleClickSearchBtn}></i>
                    <input type="text" placeholder="Search..." />
                    <span className="tooltip">Search</span>
                </li>
                {
                    !loading ?
                        pages.map((item, index) => {
                            return (
                                <li key={index}>
                                    <Link 
                                        to={item.link} 
                                        className={`nav-items ${item.link === '/' ? 'active' : ''}`} 
                                        onClick={(e) => HandleActive(e)}
                                    >
                                        <i className={`bx ${item.icon}`}></i>
                                        <span className="links_name">{item.label}</span>
                                    </Link>
                                    <span className="tooltip">{item.label}</span>
                                </li>
                            )
                        })
                        : ""
                }
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
