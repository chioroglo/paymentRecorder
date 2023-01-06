import {Button, Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow} from '@mui/material';
import React from 'react';
import {Link} from 'react-router-dom';
import {AgentTableProps} from "./AgentTableProps";

const AgentTable = ({width = "100%", items}: AgentTableProps) => {
    return (
        <Paper elevation={12} sx={{margin: "0 auto", width: width, padding: "20px"}}>
            <TableContainer>
                <Table>
                    <TableHead>
                        <TableRow>
                            <TableCell>Name</TableCell>
                            <TableCell>Agent type</TableCell>
                            <TableCell>Fiscal Code</TableCell>
                            <TableCell></TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {items.map((row) =>
                            <TableRow key={row.id}>
                                <TableCell>{row.name}</TableCell>
                                <TableCell>{row.type}</TableCell>
                                <TableCell>{row.fiscalCode}</TableCell>
                                <TableCell>
                                    <Link to={`/economic-agents/${row.fiscalCode}`}>
                                        <Button variant="outlined">GO TO</Button>
                                    </Link>
                                </TableCell>
                            </TableRow>)}
                    </TableBody>
                </Table>
            </TableContainer>
        </Paper>
    );
};

export {AgentTable};