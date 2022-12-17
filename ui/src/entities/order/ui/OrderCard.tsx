import {Box, Paper, Typography } from '@mui/material';
import React from 'react';
import {OrderCardProps} from "./OrderCardProps";
import {transformUtcStringToDateMonthHoursMinutes} from "../../../shared/lib";
import {OrderGridCell} from "./OrderGridCell";


const OrderCard = ({order}:OrderCardProps) => {
    return (
        <Paper variant="outlined" style={{padding:"5px"}} square>
            <Box display="flex">
                <OrderGridCell>PAYMENT ORDER NO.</OrderGridCell>
                <OrderGridCell>{order.number}</OrderGridCell>
                <OrderGridCell style={{flexGrow:1}}>{`EMISSION DATE:${transformUtcStringToDateMonthHoursMinutes(order.emissionDate)}`}</OrderGridCell>
            </Box>

            <Box display="flex">
                <OrderGridCell>{`SUM: ${order.amount}`}</OrderGridCell>
                <OrderGridCell style={{flexGrow:1}}>{`${order.currencyName} ${order.amount}`}</OrderGridCell>

            </Box>

            <Box display="flex">
                <OrderGridCell style={{flexGrow:1}}>{`ISSUER: ${order.issuerAgentName}`}</OrderGridCell>
                <Box>
                    <OrderGridCell>{`ISSUER ACCOUNT: ${order.issuerAccountCode}`}</OrderGridCell>
                    <OrderGridCell>{`ISSUER IDNP: ${order.issuerFiscalCode}`}</OrderGridCell>
                </Box>
            </Box>

            <Box display="flex">
                <OrderGridCell style={{flexGrow:1}}>{`ISSUER REPRESENTER (BANK) ${order.issuerBankName}`}</OrderGridCell>
                <OrderGridCell>{`BANK CODE ${order.issuerBankCode}`}</OrderGridCell>
            </Box>

            <Box display="flex">
                <OrderGridCell style={{flexGrow:1}}>{`BENEFICIARY: ${order.beneficiaryAgentName}`}</OrderGridCell>
                <Box>
                    <OrderGridCell>{`BENEFICIARY ACCOUNT: ${order.beneficiaryAccountCode}`}</OrderGridCell>
                    <OrderGridCell>{`BENEFICIARY IDNP: ${order.beneficiaryAccountCode}`}</OrderGridCell>
                </Box>
            </Box>

            <Box display="flex">
                <OrderGridCell style={{flexGrow:1}}>{`BENEFICIARY REPRESENTER (BANK) ${order.beneficiaryBankName}`}</OrderGridCell>
                <OrderGridCell>{`BANK CODE ${order.beneficiaryBankCode}`}</OrderGridCell>
            </Box>

            <Box display="flex">
                <OrderGridCell style={{flexGrow:1}}>{`DESTINATION: ${order.destination}`}</OrderGridCell>
                <OrderGridCell>{`TRANSACTION TYPE: ${order.transactionType}`}</OrderGridCell>
            </Box>


            <Box display="flex">
                <OrderGridCell style={{flexGrow:1}}>
                    {`ISSUE DATE ${order.issueDate} / EXECUTION DATE ${order.executionDate}`}
                </OrderGridCell>
                <OrderGridCell>
                    {`TRANSACTION STATE : ${order.transactionState}`}
                </OrderGridCell>
            </Box>

        </Paper>
    );
};

export default OrderCard;