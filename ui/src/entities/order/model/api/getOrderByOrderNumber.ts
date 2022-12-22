import {OrderModel} from "../types";
import {axiosHttpClient} from "../../../../shared/api/http";
import {EntityTagHeader} from "../../../../shared/lib";
import {ErrorResponse} from "../../../../shared/api/types";
import {AxiosError} from "axios";

export const getOrderByOrderNumber = async (orderId: number): Promise<OrderModel> => {
    try {
        const response = await axiosHttpClient.get(`/order/${orderId}`);

        return {...response.data, entityTag: response.headers[EntityTagHeader]};
    } catch (e) {
        const err = e as AxiosError<ErrorResponse>;

        return Promise.reject(err.response?.data.Message);
    }
}