import React from 'react';
import {Link} from 'react-router-dom';

import {Button} from "@mui/material";
import {ErrorBannerWithMessage} from "../../shared/ui/components";


const NotFoundPage = () => {


    return (
        <ErrorBannerWithMessage message={"This URL was not found on the server.Please return home"}>
            <Link to={"/"}>
                <Button variant="outlined">
                    Go home
                </Button>
            </Link>
        </ErrorBannerWithMessage>
    );
};

export {NotFoundPage};