import {Box, Button, Toolbar, Typography } from '@mui/material';
import {Link} from "react-router-dom";
import React from 'react';
import { AppNavLink } from 'shared/ui/AppNavLink';
import {useSelectorTyped} from "../../shared/store/hooks/useSelectorTyped";
import { AppNavBar } from "../../shared/ui";
import {useDispatchTyped} from "../../shared/store/hooks/useDispatchTyped";
import {logout} from "../../entities/application-user/model/thunks";
import {palette} from "../../shared/lib";

const AppHeader = () => {

    const user = useSelectorTyped(state => state.applicationUserReducer);

    const dispatch = useDispatchTyped();

    const handleLogout = () => {
        dispatch(logout());
    }


    return (
        <AppNavBar color={palette.KASHMIR_BLUE}>
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
                            <Button variant="contained" action={handleLogout}>Log out</Button>
                        </div>
                        :
                        <Button variant="contained" component={Link} to="/login">Login</Button>
                }
            </Toolbar>
        </AppNavBar>
    );
};

export default AppHeader;