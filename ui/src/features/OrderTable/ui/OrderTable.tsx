import {Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow} from '@mui/material';
import React from 'react';
import {OrderTableProps} from "./OrderTableProps";

const OrderTable = ({width = "100%",items}: OrderTableProps) => {
    return (
        <TableContainer component={Paper}>
            <Table>
                <TableHead>
                    <TableRow>
                        <TableCell>No.</TableCell>
                        <TableCell>Amount</TableCell>
                        <TableCell>Currency</TableCell>
                        <TableCell>Issuer</TableCell>
                        <TableCell>Beneficiary</TableCell>
                        <TableCell>Transaction State</TableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    {items.map((row) => <TableRow>
                        <TableCell>{row.number}</TableCell>
                        <TableCell>{row.amount}</TableCell>
                        <TableCell>{row.currencyName}</TableCell>
                        <TableCell>{row.issuerAgentName}</TableCell>
                        <TableCell>{row.beneficiaryAgentName}</TableCell>
                        <TableCell>{row.transactionState}</TableCell>
                    </TableRow>)}
                </TableBody>
            </Table>
        </TableContainer>
    );
};

export {OrderTable};