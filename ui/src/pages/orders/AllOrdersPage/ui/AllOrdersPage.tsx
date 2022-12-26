import {OrderModel} from 'entities/order/model/types';
import {OrderTable} from 'features/OrderTable';
import React, {useEffect, useState} from 'react';
import {CenteredLoader, ErrorBannerWithMessage} from "../../../../shared/ui/components";
import {getAllOrdersInPeriod} from "../../../../entities/order/model/api";
import {CLIENT_ERROR_OCCURRED} from "../../../../shared/lib";
import {Box, Button, Paper, TextField, Typography} from "@mui/material";
import {Moment} from "moment/moment";
import {DateTimePicker} from "@mui/x-date-pickers";
import "./styles/AllOrdersPage.css";
import {useNavigate} from 'react-router-dom';

const pageWidth = "90%"

export const AllOrdersPage = () => {

    const navigate = useNavigate();

    const [isLoading, setLoading] = useState<boolean>(true);
    const [orders, setOrders] = useState<OrderModel[]>([]);
    const [error, setError] = useState<boolean>(false);
    const [errorMessage, setErrorMessage] = useState<string>(CLIENT_ERROR_OCCURRED);

    const [startDate, setStartDate] = useState<Date>(() => {
        const date = new Date();
        date.setMonth(0);
        return date;
    });

    const [endDate, setEndDate] = useState<Date>(() => {
        const date = new Date();
        date.setMonth(11);
        return date;
    });

    useEffect(() => {

        setLoading(true);
        setError(false);

        // validation
        if (startDate <= endDate) {
            getAllOrdersInPeriod(startDate, endDate).then((result) => {
                setOrders(result);
            }).catch((errorMessage) => {
                if (errorMessage) {
                    setErrorMessage(errorMessage);
                }
                setError(true);
            }).finally(() => setLoading(false));
        } else {
            setError(true);
            setErrorMessage("Error has occurred. Please introduce correct time period");
        }

    }, [startDate, endDate]);


    return (
        <div>
            <Paper elevation={12} className="order-list-timespan-selector" sx={{width: pageWidth}}>
                <Typography className="timespan-selector-headline">Order selection menu</Typography>
                <Box className="timespan-inputs-container">
                    <DateTimePicker label="Start date" renderInput={(params) =>
                        <TextField {...params} error={startDate >= endDate}/>}
                                    value={startDate}
                                    onChange={(value: Moment | null) => {
                                        if (value?.isValid()) {
                                            setStartDate(value?.toDate());
                                        }
                                    }}/>


                    <DateTimePicker label="End date" renderInput={(params) =>
                        <TextField {...params} error={startDate >= endDate}/>}
                                    value={endDate}
                                    onChange={(value: Moment | null) => {
                                        if (value?.isValid()) {
                                            setEndDate(value?.toDate());
                                        }
                                    }}/>
                </Box>

                <Box display="flex" justifyContent="space-around">
                    <Button variant="contained" fullWidth={false}
                            onClick={() => navigate("/orders/create-new")}>
                        ADD NEW PAYMENT ORDER
                    </Button>
                </Box>
            </Paper>
            {isLoading ?
                <CenteredLoader/>
                : <div>
                    {error ? <ErrorBannerWithMessage message={errorMessage}/> :
                        <OrderTable width={pageWidth} items={orders}/>}
                </div>}
        </div>
    );
};
