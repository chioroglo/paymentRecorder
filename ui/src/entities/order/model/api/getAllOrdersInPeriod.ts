import {OrderModel} from "../types";
import {axiosHttpClient} from "../../../../shared/api/http";
import {ErrorResponse} from "../../../../shared/api/types";
import {AxiosError} from "axios";

export const getAllOrdersInPeriod = async (periodStart: Date, periodEnd: Date, limit: number = 10): Promise<OrderModel[]> => {
    try {
        const response = await axiosHttpClient.get<OrderModel[]>(`/order/get-by-period/?periodStart=${periodStart.toISOString()}&periodEnd=${periodEnd.toISOString()}&limit=${limit}`);

        return response.data;
    } catch (e) {
        const err = e as AxiosError<ErrorResponse>;

        return Promise.reject(err.response?.data.Message);
    }
}