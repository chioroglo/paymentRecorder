import { Layout } from 'pages/layout';
import { LoginPage } from 'pages/login';
import React, { useEffect } from 'react';
import {Routes,Route} from "react-router-dom";
import "./App.css";
import {RouteGuardAuthRequired} from "./lib/routeGuards";
import RouteGuardUnauthorizedOnly from "./lib/routeGuards/RouteGuardUnauthorizedOnly";
import {actualizeUserUsingRefreshToken} from "../entities/application-user/model/thunks";
import { useDispatchTyped } from 'shared/store/hooks/useDispatchTyped';

function App() {

    const dispatch = useDispatchTyped();

    useEffect(() => {
        dispatch(actualizeUserUsingRefreshToken());
    },[]);

    return (
    <div className="App">
        <Routes>
            <Route path="/" element={<Layout/>}>
                <Route path="/login" element={<RouteGuardUnauthorizedOnly children={<LoginPage/>}/>}></Route>
                <Route path="/orders" element={<RouteGuardAuthRequired children={<div>aaa</div>}/>}></Route>
            </Route>
        </Routes>
    </div>
);
}

export {App};
