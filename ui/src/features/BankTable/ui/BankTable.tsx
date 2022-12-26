import React from 'react';
import {BankTableProps} from "./BankTableProps";
import {Button, Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow} from '@mui/material';
import {useNavigate} from "react-router-dom";


const BankTable = ({width = "100%", items}: BankTableProps) => {

    const navigate = useNavigate();

    return (
        <Paper elevation={12} sx={{margin: "0 auto", width: width, padding: "20px"}}>
            <TableContainer>
                <Table>
                    <TableHead>
                        <TableRow>
                            <TableCell>Id</TableCell>
                            <TableCell>Name</TableCell>
                            <TableCell>Code</TableCell>
                            <TableCell/>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {items.map(bank =>
                            <TableRow key={bank.id}>
                                <TableCell>{bank.id}</TableCell>
                                <TableCell>{bank.name}</TableCell>
                                <TableCell>{bank.code}</TableCell>
                                <TableCell>
                                    <Button variant="contained" onClick={() => navigate(`/banks/${bank.code}`)}>
                                        GO TO
                                    </Button>
                                </TableCell>
                            </TableRow>)}
                    </TableBody>
                </Table>
            </TableContainer>
        </Paper>
    );
};

export {BankTable};
