import {Button, Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow} from '@mui/material';
import React from 'react';
import {findIana} from 'windows-iana';
import {OrderTableProps} from "./OrderTableProps";
import {changeTimezone, transformUtcStringToDateMonthHoursMinutes} from "../../../shared/lib";
import {Link} from 'react-router-dom';

const OrderTable = ({width = "100%", items}: OrderTableProps) => {

    return (
        <TableContainer sx={{margin: "0 auto", width: width}} component={Paper}>
            <Table>
                <TableHead>
                    <TableRow>
                        <TableCell>No.</TableCell>
                        <TableCell>Amount</TableCell>
                        <TableCell>Currency</TableCell>
                        <TableCell>Issuer</TableCell>
                        <TableCell>Beneficiary</TableCell>
                        <TableCell>Transaction State</TableCell>
                        <TableCell>Issue date</TableCell>
                        <TableCell></TableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    {items.map((row) =>
                        <TableRow key={row.number}>
                            <TableCell>{row.number}</TableCell>
                            <TableCell>{row.amount}</TableCell>
                            <TableCell>{row.currencyName}</TableCell>
                            <TableCell>{row.issuerAgentName}</TableCell>
                            <TableCell>{row.beneficiaryAgentName}</TableCell>
                            <TableCell>{row.transactionState}</TableCell>
                            <TableCell>{transformUtcStringToDateMonthHoursMinutes(changeTimezone(new Date(row.issueDate), findIana(row.timezone)[0]).toISOString())}</TableCell>
                            <TableCell>
                                <Link to={`/orders/${row.number}`}>
                                    <Button variant="outlined">GO TO</Button>
                                </Link>
                            </TableCell>
                        </TableRow>
                    )}
                </TableBody>
            </Table>
        </TableContainer>
    );
};

export {OrderTable};