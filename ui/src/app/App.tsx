import { Layout } from 'pages/layout';
import React from 'react';
import {Routes,Route} from "react-router-dom";

function App() {

    return (
    <div className="App">
        <Routes>
            <Route path="/" element={<Layout/>}>

            </Route>
        </Routes>
    </div>
);
}

export {App};
