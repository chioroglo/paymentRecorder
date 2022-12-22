import {Box, Paper} from '@mui/material';
import React from 'react';
import {OrderCardProps} from "./OrderCardProps";
import {transformUtcStringToDateMonthHoursMinutes} from "../../../../shared/lib";
import {OrderCardGridCell} from "../OrderCardGridCell/OrderCardGridCell";


const OrderCard = ({order}: OrderCardProps) => {
    return (
        <Paper variant="outlined" square>
            <Box display="flex">
                <OrderCardGridCell>PAYMENT ORDER NO.</OrderCardGridCell>
                <OrderCardGridCell>{'№' + order.number}</OrderCardGridCell>
                <OrderCardGridCell
                    style={{flexGrow: 1}}>{`EMISSION DATE:${transformUtcStringToDateMonthHoursMinutes(order.emissionDate)}`}</OrderCardGridCell>
            </Box>

            <Box display="flex">
                <OrderCardGridCell>{`SUM: ${order.amount}`}</OrderCardGridCell>
                <OrderCardGridCell style={{flexGrow: 1}}>{`${order.amount} ${order.currencyName}`}</OrderCardGridCell>

            </Box>

            <Box display="flex">
                <OrderCardGridCell style={{flexGrow: 1}}>{`ISSUER: ${order.issuerAgentName}`}</OrderCardGridCell>
                <Box>
                    <OrderCardGridCell>{`ISSUER ACCOUNT: ${order.issuerAccountCode}`}</OrderCardGridCell>
                    <OrderCardGridCell>{`ISSUER IDNP: ${order.issuerFiscalCode}`}</OrderCardGridCell>
                </Box>
            </Box>

            <Box display="flex">
                <OrderCardGridCell
                    style={{flexGrow: 1}}>{`ISSUER REPRESENTER (BANK) ${order.issuerBankName}`}</OrderCardGridCell>
                <OrderCardGridCell>{`BANK CODE ${order.issuerBankCode}`}</OrderCardGridCell>
            </Box>

            <Box display="flex">
                <OrderCardGridCell
                    style={{flexGrow: 1}}>{`BENEFICIARY: ${order.beneficiaryAgentName}`}</OrderCardGridCell>
                <Box>
                    <OrderCardGridCell>{`BENEFICIARY ACCOUNT: ${order.beneficiaryAccountCode}`}</OrderCardGridCell>
                    <OrderCardGridCell>{`BENEFICIARY IDNP: ${order.beneficiaryAccountCode}`}</OrderCardGridCell>
                </Box>
            </Box>

            <Box display="flex">
                <OrderCardGridCell
                    style={{flexGrow: 1}}>{`BENEFICIARY REPRESENTER (BANK) ${order.beneficiaryBankName}`}</OrderCardGridCell>
                <OrderCardGridCell>{`BANK CODE ${order.beneficiaryBankCode}`}</OrderCardGridCell>
            </Box>

            <Box display="flex">
                <OrderCardGridCell style={{flexGrow: 1}}>{`DESTINATION: ${order.destination}`}</OrderCardGridCell>
                <OrderCardGridCell>{`TRANSACTION TYPE: ${order.transactionType}`}</OrderCardGridCell>
            </Box>


            <Box display="flex">
                <OrderCardGridCell style={{flexGrow: 1}}>
                    {`ISSUE DATE: ${transformUtcStringToDateMonthHoursMinutes(order.issueDate)   } (${order.timezone}) / EXECUTION DATE: ${order.executionDate ? `${transformUtcStringToDateMonthHoursMinutes(order.executionDate)} (${order.timezone})` : '-'}`}
                </OrderCardGridCell>
                <OrderCardGridCell>
                    {`TRANSACTION STATE : ${order.transactionState}`}
                </OrderCardGridCell>
            </Box>

        </Paper>
    );
};

export default OrderCard;