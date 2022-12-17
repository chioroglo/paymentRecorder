import React from 'react';
import {NavLink} from "react-router-dom";
import {AppNavLinkProps} from "./AppNavLinkProps";
import {useMatch} from "react-router-dom";
import { palette } from 'app/ui';
import "./AppNavLink.css";

export const AppNavLink = ({to,children,color=palette.ZIGGURAT,style}: AppNavLinkProps) => {

    const match = useMatch(to);

    return (
        <NavLink style={{color: color, cursor: "pointer",textDecoration: match ? "underline" : undefined ,...style}} className={'app-nav-link'} end to={to}>
            {children}
        </NavLink>
    );
};
