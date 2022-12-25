import {OrderDto, OrderModel} from "../types";
import {axiosHttpClient} from "../../../../shared/api/http";
import {EntityTagHeader, NO_ENTITY_TOKEN_PROVIDED} from "../../../../shared/lib";
import {ErrorResponse} from "../../../../shared/api/types";
import {AxiosError} from "axios";
import {TransactionState} from "../../../../shared/lib/enum";

export const addOrder = async (order: OrderDto): Promise<OrderModel> => {
    try {

        if (order.transactionState !== TransactionState.Completed) {
            order.executionDate = undefined;
        }

        const response = await axiosHttpClient.post<OrderModel>(`/order/`, order);

        return {...response.data, entityTag: response.headers[EntityTagHeader] || NO_ENTITY_TOKEN_PROVIDED};
    } catch (e) {
        const err = e as AxiosError<ErrorResponse>;

        return Promise.reject(err.response?.data.Message);
    }
}