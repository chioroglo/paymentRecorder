import {Layout} from 'pages/layout';
import {LoginPage} from 'pages/login';
import React from 'react';
import {Route, Routes} from "react-router-dom";
import "./App.css";
import {RouteGuardAuthRequired, RouteGuardUnauthorizedOnly} from "./lib/routeGuards";
import {AllOrdersPage, OrderPage} from "../pages/orders";

function App() {


    return (
        <div className="App">
            <Routes>
                <Route path="/" element={<Layout/>}>
                    <Route path="/login" element={<RouteGuardUnauthorizedOnly children={<LoginPage/>}/>}/>
                    <Route path="/orders" element={<RouteGuardAuthRequired children={<AllOrdersPage/>}/>}/>
                    <Route path="/orders/:orderNumber" element={<RouteGuardAuthRequired children={<OrderPage/>}/>}/>
                </Route>
            </Routes>
        </div>
    );
}

export {App};
