import {addOrder} from 'entities/order/model/api';
import {OrderDto} from 'features/OrderForm/types';
import React from 'react';
import {OrderForm} from "../../features/OrderForm/ui";
import {useSnackbar, VariantType} from "notistack";
import {CLIENT_ERROR_OCCURRED} from "../../shared/lib";
import {IconButton} from "@mui/material";
import CloseIcon from "@mui/icons-material/Close";

export const AllOrdersPage = () => {

    const {enqueueSnackbar, closeSnackbar} = useSnackbar();

    const sendOrder = async (order: OrderDto) => {
        let message: string = CLIENT_ERROR_OCCURRED;
        let variant: VariantType = "info";

        try {
            await addOrder(order);
            message = "Order added successfully!";
            variant = "success";
        } catch (e) {
            console.log(e);
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
        <div>
            <OrderForm formActionCallback={sendOrder}/>
        </div>
    );
};
