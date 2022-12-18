import {OrderModel} from 'entities/order/model/types';
import React, {useEffect, useState} from 'react';
import {useParams} from 'react-router-dom';
import {getOrderByOrderNumber} from "../../entities/order/model/api";
import OrderCard from "../../entities/order/ui/OrderCard";
import {CenteredLoader} from "../../shared/ui/components/CenteredLoader";

export const OrderPage = () => {

    const {orderNumber} = useParams();
    const [order, setOrder] = useState<OrderModel>();
    const [error, setError] = useState<boolean>(false);
    const [isLoading, setLoading] = useState<boolean>(true);

    const fetchAndSetOrder = async () => {
        const response = await getOrderByOrderNumber(parseInt(orderNumber || "0"));
        console.log("response received");
        setOrder(response);
    }

    useEffect(() => {
        fetchAndSetOrder().catch((err) => {
            setError(true);
        }).finally(() => {
            setLoading(false);
        });
    }, []);

    return (
        <div>
            {
                isLoading ? <CenteredLoader/> :
                    ((!error && order) ? <OrderCard order={order}/> : <span>error</span>)
            }
        </div>
    );
};
