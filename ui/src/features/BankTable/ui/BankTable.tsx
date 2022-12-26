import React from 'react';
import {BankTableProps} from "./BankTableProps";
import { Button, Table, TableBody, TableCell, TableContainer, TableHead, TableRow } from '@mui/material';


const BankTable = ({width = "100%",items}:BankTableProps) => {
    return (
            <TableContainer sx={{margin:"0 auto",width:width}}>
                <Table>
                    <TableHead>
                        <TableRow>
                            <TableCell>Id</TableCell>
                            <TableCell>Name</TableCell>
                            <TableCell>Code</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {items.map(bank =>
                        <TableRow>
                            <TableCell>{bank.id}</TableCell>
                            <TableCell>{bank.name}</TableCell>
                            <TableCell>{bank.code}</TableCell>
                        </TableRow>)}

                    </TableBody>
                </Table>
            </TableContainer>
    );
};

export {BankTable};