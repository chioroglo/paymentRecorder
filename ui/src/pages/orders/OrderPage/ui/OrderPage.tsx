import Button from '@mui/material/Button';
import {OrderModel} from 'entities/order/model/types';
import React, {useCallback, useEffect, useState} from 'react';
import {useNavigate, useParams} from 'react-router-dom';
import {getOrderByOrderNumber} from "../../../../entities/order/model/api";
import OrderCard from "../../../../entities/order/ui/OrderCard/OrderCard";
import {CenteredLoader, ErrorBannerWithMessage} from "../../../../shared/ui/components";

export const OrderPage = () => {

    const {orderNumber} = useParams();
    const navigate = useNavigate();
    const [order, setOrder] = useState<OrderModel>();
    const [error, setError] = useState<boolean>(false);
    const [isLoading, setLoading] = useState<boolean>(true);

    const fetchAndSetOrder = useCallback(async () => {
        if (orderNumber) {
            const response = await getOrderByOrderNumber(parseInt(orderNumber));
            setOrder(response);
        }
    },[orderNumber]);

    useEffect(() => {
        fetchAndSetOrder().catch(() => {
            setError(true);
        }).finally(() => {
            setLoading(false);
        });
    }, [fetchAndSetOrder]);

    return (
        <div>
            {
                isLoading ? <CenteredLoader/> :
                    ((!error && order) ? <OrderCard order={order}/> :
                        <ErrorBannerWithMessage message={`Order with number №'${orderNumber}' was not found`}>
                            <Button variant="outlined" onClick={() => navigate(-1)}>
                                Go back
                            </Button>
                        </ErrorBannerWithMessage>)
            }
        </div>
    );
};
