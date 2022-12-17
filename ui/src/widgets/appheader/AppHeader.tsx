import {Box, Button, IconButton, Toolbar, Typography } from '@mui/material';
import {Link} from "react-router-dom";
import React from 'react';
import { AppNavLink } from 'shared/ui/components/AppNavLink';
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

                <Box sx={{flexGrow: 1}} style={{display: "flex", justifyContent: "flex-start",alignItems:"center"}}>

                    <img width="64px" height="64px" src={"logo1024.png"}/>
                    <AppNavLink to={"/"}>
                        <Typography color={"#FFF"}>PaymentRecorder</Typography>
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