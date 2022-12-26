import {Box, Button, IconButton, Toolbar, Typography} from '@mui/material';
import {Link, NavLink} from "react-router-dom";
import React from 'react';
import {useSelectorTyped} from "../../shared/store/hooks/useSelectorTyped";
import {AppNavBar} from "../../shared/ui/components";
import {useDispatchTyped} from "../../shared/store/hooks/useDispatchTyped";
import {logout} from "../../entities/application-user/model/thunks";
import LogoutIcon from '@mui/icons-material/Logout';
import {HeaderNavLink} from 'shared/ui/components/HeaderNavLink';

const logo = require("../../shared/media/logo1024.png");

const AppHeader = () => {

    const user = useSelectorTyped(state => state.applicationUserReducer);

    const dispatch = useDispatchTyped();

    const handleLogout = () => dispatch(logout());


    return (
        <AppNavBar color="primary">
            <Toolbar style={{justifyContent: "space-between"}}>
                {
                    user.isAuthorized ?
                        <Box justifyContent="space-between" minWidth="200px" display={"flex"} alignItems="center">

                            <IconButton style={{"border": "1px solid black", margin: "0 5px 0 0"}}
                                        onClick={handleLogout}><LogoutIcon/></IconButton>
                            <Typography display={"inline"}>Welcome, {user.username}!</Typography>
                        </Box>
                        :
                        <Button color="primary" variant="contained" component={Link} to="/login">Login</Button>
                }

                {
                    user.isAuthorized &&
                    <Box sx={{flexGrow: 1}} minWidth="100px" display="flex" justifyContent="flex-start">

                        <HeaderNavLink to="/orders">ORDERS</HeaderNavLink>

                        <HeaderNavLink to="/banks">BANKS</HeaderNavLink>

                        <HeaderNavLink to="/economic-agents">AGENTS</HeaderNavLink>

                        <HeaderNavLink to={"/accounts"}>ACCOUNTS</HeaderNavLink>
                    </Box>
                }

                <Box style={{display: "flex", justifyContent: "space-between", alignItems: "center",}}>

                    <img alt="logo" width="64px" height="64px" src={logo}/>
                    <NavLink to={"/"}>
                        <Typography color={"#FFF"}>PaymentRecorder</Typography>
                    </NavLink>

                </Box>

            </Toolbar>
        </AppNavBar>
    );
};

export default AppHeader;