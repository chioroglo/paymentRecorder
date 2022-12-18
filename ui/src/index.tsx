import React from 'react';
import ReactDOM from 'react-dom/client';
import {Provider} from 'react-redux';
import {App} from './app/App';
import {setupStore} from "./shared/store/store";
import {BrowserRouter} from "react-router-dom";
import {ThemeProvider} from "@mui/material";
import {theme} from "./app/ui";
import {SnackbarProvider} from "notistack";
import {NOTIFICATION_STACK_CAPACITY, NOTIFICATIONS_AUTO_HIDE_DURATION_MS} from "./shared/config";

const store = setupStore();

const root = ReactDOM.createRoot(
    document.getElementById('root') as HTMLElement
);

root.render(
    <Provider store={store}>
        <BrowserRouter>
            <ThemeProvider theme={theme}>
                <SnackbarProvider autoHideDuration={NOTIFICATIONS_AUTO_HIDE_DURATION_MS}
                                  maxSnack={NOTIFICATION_STACK_CAPACITY}>
                    <App/>
                </SnackbarProvider>
            </ThemeProvider>
        </BrowserRouter>
    </Provider>
);
