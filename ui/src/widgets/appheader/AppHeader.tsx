import {Box, Button, IconButton, Toolbar, Typography } from '@mui/material';
import {Link, NavLink} from "react-router-dom";
import React from 'react';
import { AppNavLink } from 'shared/ui/components/AppNavLink';
import {useSelectorTyped} from "../../shared/store/hooks/useSelectorTyped";
import { AppNavBar } from "../../shared/ui";
import {useDispatchTyped} from "../../shared/store/hooks/useDispatchTyped";
import {logout} from "../../entities/application-user/model/thunks";
import LogoutIcon from '@mui/icons-material/Logout';
import { HeaderNavLink } from 'shared/ui/components/HeaderNavLink';

const AppHeader = () => {

    const user = useSelectorTyped(state => state.applicationUserReducer);

    const dispatch = useDispatchTyped();

    const handleLogout = () => dispatch(logout());


    return (
        <AppNavBar color="primary">
            <Toolbar>
                {
                    user.isAuthorized ?
                        <Box justifyContent="space-around" minWidth="fit-content" display={"flex"} alignItems="center">

                            <IconButton style={{"border":"1px solid black"}} onClick={handleLogout}><LogoutIcon/></IconButton>
                            <Typography display={"inline"}>Welcome, {user.username}!</Typography>
                        </Box>
                        :
                        <Button color="primary" variant="contained" component={Link} to="/login">Login</Button>
                }

                <Box sx={{flexGrow: 1}} minWidth="100px" display="flex" justifyContent="flex-start">

                    <HeaderNavLink to="/orders">ORDERS</HeaderNavLink>

                    <HeaderNavLink to="/banks">BANKS</HeaderNavLink>

                    <HeaderNavLink to="/economic-agents">AGENTS</HeaderNavLink>
                </Box>

                <Box style={{display: "flex", justifyContent: "space-between",alignItems:"center",}}>

                    <img width="64px" height="64px" src={"logo1024.png"}/>
                    <NavLink to={"/"}>
                        <Typography color={"#FFF"}>PaymentRecorder</Typography>
                    </NavLink>

                </Box>

            </Toolbar>
        </AppNavBar>
    );
};

export default AppHeader;