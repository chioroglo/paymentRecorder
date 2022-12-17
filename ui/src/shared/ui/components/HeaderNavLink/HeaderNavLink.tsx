import React from 'react';
import {AppNavLink} from "../AppNavLink";
import { AppNavLinkProps } from '../AppNavLink/AppNavLinkProps';

const HeaderNavLink = (props:AppNavLinkProps) => {
    return <AppNavLink style={{padding:"0 20px"}} {...props}/>;
};

export {HeaderNavLink};