import { AppBar } from '@mui/material';
import React from 'react';
import {AppNavBarProps} from "./AppNavBarProps";

export const AppNavBar = ({children}: AppNavBarProps) => {
    return (
        <AppBar position="absolute">
            {children}
        </AppBar>
    );
};
