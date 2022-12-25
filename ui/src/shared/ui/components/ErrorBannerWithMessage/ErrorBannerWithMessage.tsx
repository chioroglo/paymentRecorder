import {Box, Typography} from '@mui/material';
import React from 'react';
import ErrorIcon from '@mui/icons-material/Error';
import {ErrorBannerWithMessageProps} from "./ErrorBannerWithMessageProps";

const ErrorBannerWithMessage = ({message, children}: ErrorBannerWithMessageProps) => {
    return (
        <Box style={{margin: "15% auto"}}>
            <Typography variant={"h2"} style={{textAlign: "center"}}>
                {message}
            </Typography>

            <ErrorIcon
                style={{margin: "0 auto", display: "block", width: "100px", height: "100px"}}/>

            <Box display="flex" justifyContent="center">
                {children}
            </Box>
        </Box>
    );
};

export {ErrorBannerWithMessage};