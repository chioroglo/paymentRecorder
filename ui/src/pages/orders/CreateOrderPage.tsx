import React from 'react';
import {useSnackbar, VariantType} from "notistack";
import {OrderDto} from "../../features/OrderForm/types";
import {CLIENT_ERROR_OCCURRED} from "../../shared/lib";
import {addOrder} from "../../entities/order/model/api";
import {Box, Icon, IconButton, Paper, Typography} from "@mui/material";
import CloseIcon from "@mui/icons-material/Close";
import {OrderForm} from "../../features/OrderForm/ui";
import DocumentScannerIcon from '@mui/icons-material/DocumentScanner';

const CreateOrderPage = () => {

    const {enqueueSnackbar, closeSnackbar} = useSnackbar();

    const sendOrder = async (order: OrderDto) => {
        let message: string = CLIENT_ERROR_OCCURRED;
        let variant: VariantType = "info";

        try {
            await addOrder(order);
            message = "Order added successfully!";
            variant = "success";
        } catch (e) {
            variant = "error";
            if (typeof (e) === "string") {
                message = e;
            }
        } finally {
            enqueueSnackbar(message, {
                variant: variant,
                action: (key) =>
                    <IconButton onClick={() => closeSnackbar(key)}>
                        <CloseIcon/>
                    </IconButton>
            });
        }
    }

    return (
        <>
            <Paper elevation={12} style={{display:"flex",justifyContent:"space-between",margin:"20px auto",width:"fit-content",padding:"20px",alignItems:"center"}}>
                <DocumentScannerIcon fontSize={"large"} />
                <Box>
                    <Typography textAlign="center" variant="h3">WRITE NEW PAYMENT ORDER</Typography>
                    <Typography textAlign="center" variant="h4">Please, take care about data you input!</Typography>
                </Box>
            </Paper>

            <OrderForm formActionCallback={sendOrder}/>
        </>
    );
};

export {CreateOrderPage};