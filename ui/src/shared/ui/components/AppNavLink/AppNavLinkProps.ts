import React from "react";

export interface AppNavLinkProps {
    to: string,
    children: JSX.Element | string,
    color?: string,
    style?: React.CSSProperties
}