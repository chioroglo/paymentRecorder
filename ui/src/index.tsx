import React from 'react';
import ReactDOM from 'react-dom/client';
import {Provider} from 'react-redux';
import {App} from './app/App';
import {setupStore} from "./shared/store/store";
import {BrowserRouter} from "react-router-dom";
import {ThemeProvider} from "@mui/material";
import {theme} from "./app/ui";

const store = setupStore();

const root = ReactDOM.createRoot(
    document.getElementById('root') as HTMLElement
);

root.render(
    <Provider store={store}>
        <BrowserRouter>
            <ThemeProvider theme={theme}>
                <App/>
            </ThemeProvider>
        </BrowserRouter>
    </Provider>
);
