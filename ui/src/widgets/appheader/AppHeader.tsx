import {Box, Button, IconButton, Toolbar, Typography } from '@mui/material';
import {Link} from "react-router-dom";
import React from 'react';
import { AppNavLink } from 'shared/ui/AppNavLink';
import {useSelectorTyped} from "../../shared/store/hooks/useSelectorTyped";
import { AppNavBar } from "../../shared/ui";
import {useDispatchTyped} from "../../shared/store/hooks/useDispatchTyped";
import {logout} from "../../entities/application-user/model/thunks";
import LogoutIcon from '@mui/icons-material/Logout';

const AppHeader = () => {

    const user = useSelectorTyped(state => state.applicationUserReducer);

    const dispatch = useDispatchTyped();

    const handleLogout = () => {
        dispatch(logout());
    }


    return (
        <AppNavBar color="primary">
            <Toolbar>

                <Box sx={{flexGrow: 1}} style={{display: "flex", justifyContent: "flex-start"}}>

                    <AppNavLink to={"/"}>
                        <Typography color={"#FFF"}>Home Page</Typography>
                    </AppNavLink>

                </Box>

                {
                    user.isAuthorized ?
                        <Box justifyContent="space-around" minWidth="300px" display={"flex"} alignItems="center">
                            <Typography display={"inline"}>Welcome, {user.username}!</Typography>
                            <IconButton style={{"border":"1px solid black"}} onClick={handleLogout}><LogoutIcon/></IconButton>

                        </Box>
                        :
                        <Button variant="contained" component={Link} to="/login">Login</Button>
                }
            </Toolbar>
        </AppNavBar>
    );
};

export default AppHeader;