import React from 'react';
import {NavLink, useMatch} from "react-router-dom";
import {AppNavLinkProps} from "./AppNavLinkProps";
import {palette} from 'app/ui';
import "./AppNavLink.css";

export const AppNavLink = ({to, children, color = palette.ZIGGURAT, style}: AppNavLinkProps) => {

    const match = useMatch(to);

    return (
        <NavLink style={{color: color, fontStyle: match ? "italic" : undefined, cursor: "pointer", ...style}}
                 className={'app-nav-link'} end to={to}>
            {children}
        </NavLink>
    );
};
