import { Layout } from 'pages/layout';
import { LoginPage } from 'pages/login';
import React from 'react';
import {Routes,Route} from "react-router-dom";
import "./App.css";
import {RouteGuardAuthRequired} from "./lib/routeGuards";
import RouteGuardUnauthorizedOnly from "./lib/routeGuards/RouteGuardUnauthorizedOnly";

function App() {

    return (
    <div className="App">
        <Routes>
            <Route path="/" element={<Layout/>}>
                <Route path="/login" element={<RouteGuardUnauthorizedOnly children={<LoginPage/>}/>}></Route>
            </Route>
        </Routes>
    </div>
);
}

export {App};
