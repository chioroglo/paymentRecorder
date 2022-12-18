import React from 'react';
import {Typography} from "@mui/material";

const cellStyle: React.CSSProperties = {
    border: "1px solid black",
    padding: "5px 40px",
    textAlign: "center"
}
export const OrderGridCell = ({children, style}: { children: string | number, style?: React.CSSProperties }) => {
    return (
        <Typography style={{...cellStyle, ...style}}>
            {children}
        </Typography>
    );
};
