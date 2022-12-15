import {Box, Button, Toolbar, Typography } from '@mui/material';
import {Link} from "react-router-dom";
import React from 'react';
import { AppNavLink } from 'shared/ui/AppNavLink';
import {useSelectorTyped} from "../../shared/store/hooks/useSelectorTyped";
import { AppNavBar } from "../../shared/ui";

const AppHeader = () => {

    const user = useSelectorTyped(state => state.applicationUserReducer);


    return (
        <AppNavBar>
            <Toolbar>

                <Box sx={{flexGrow: 1}} style={{display: "flex", justifyContent: "flex-start"}}>

                    <AppNavLink to={"/"}>
                        <Typography color={"#FFF"}>Home Page</Typography>
                    </AppNavLink>

                </Box>

                {
                    user.isAuthorized ?
                        <div>
                            <Typography display={"inline"}>Welcome, {user.username}!</Typography>
                        </div>
                        :
                        <Button variant="contained" component={Link} to="/login">Login</Button>
                }
            </Toolbar>
        </AppNavBar>
    );
};

export default AppHeader;