import {Layout} from 'pages/layout';
import {LoginPage} from 'pages/login';
import React from 'react';
import {Route, Routes} from "react-router-dom";
import "./App.css";
import {RouteGuardAuthRequired, RouteGuardUnauthorizedOnly} from "./lib/routeGuards";
import {AllOrdersPage, CreateOrderPage, OrderPage} from "../pages/orders";
import MainPage from "../pages/main/MainPage";
import {NotFoundPage} from "../pages/not-found";
import {BankMenuPage} from 'pages/banks/BankMenu';
import {BankPage} from "../pages/banks/BankPage";
import {AgentMenuPage} from "../pages/agents/AgentMenu/ui";
import {AgentPage} from "../pages/agents/AgentPage/ui";

function App() {


    return (
        <div className="App">
            <Routes>
                <Route path="/" element={<Layout/>}>
                    <Route path="login" element={<RouteGuardUnauthorizedOnly children={<LoginPage/>}/>}/>

                    <Route path="orders">
                        <Route path="list/" element={<RouteGuardAuthRequired children={<AllOrdersPage/>}/>}/>
                        <Route path=":orderNumber" element={<RouteGuardAuthRequired children={<OrderPage/>}/>}/>
                        <Route path="create-new" element={<RouteGuardAuthRequired children={<CreateOrderPage/>}/>}/>
                    </Route>

                    <Route path="banks">
                        <Route path="menu" element={<RouteGuardAuthRequired children={<BankMenuPage/>}/>}/>
                        <Route path=":bankCode" element={<RouteGuardAuthRequired children={<BankPage/>}/>}/>
                    </Route>

                    <Route path="economic-agents">
                        <Route path="menu" element={<RouteGuardAuthRequired children={<AgentMenuPage/>}/>}/>
                        <Route path=":agentFiscalCode" element={<RouteGuardAuthRequired children={<AgentPage/>}/>}/>
                    </Route>

                    <Route path="accounts">
                        <Route path="menu" element={<RouteGuardAuthRequired children={<></>}/>}/>
                        <Route path=":accountId" element={<RouteGuardAuthRequired children={<></>}/>}/>
                    </Route>


                    <Route path="/" element={<RouteGuardAuthRequired children={<MainPage/>}/>}/>

                    <Route path="*" element={<NotFoundPage/>}/>
                </Route>
            </Routes>
        </div>
    );
}

export {App};
