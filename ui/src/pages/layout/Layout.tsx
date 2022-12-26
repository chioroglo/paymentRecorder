import React from 'react';
import {Outlet} from "react-router-dom";
import AppHeader from 'widgets/appheader/AppHeader';

const Layout = () => {
    return (
        <>
            <div style={{height: "5vh", minHeight: "70px"}}/>

            <AppHeader/>

            <main>
                <Outlet/>
            </main>
        </>
    );
};

export {Layout};