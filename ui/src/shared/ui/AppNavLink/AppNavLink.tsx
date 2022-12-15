import React from 'react';
import {NavLink} from "react-router-dom";
import {AppNavLinkProps} from "./AppNavLinkProps";
import {useMatch} from "react-router-dom";

export const AppNavLink = (props: AppNavLinkProps) => {

    const match = useMatch(props.to);

    return (
        <NavLink end style={{color: match ? props.highlightColor : props.color, cursor: "pointer" }} to={props.to}>
            {props.children}
        </NavLink>
    );
};
