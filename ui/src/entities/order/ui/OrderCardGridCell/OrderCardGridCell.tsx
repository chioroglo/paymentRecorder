import React from 'react';
import {Box} from "@mui/material";
import {OrderCardGridCellProps} from "./OrderCardGridCellProps";
import "./styles/OrderCardGridCell.css";

const orderCardGridCellClassName = "order-card-grid-cell";

export const OrderCardGridCell = ({children, style}: OrderCardGridCellProps) => {
    return (
        <Box alignContent="self" className={orderCardGridCellClassName} style={{...style}}>
            {children}
        </Box>
    );
};
