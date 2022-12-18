import {OrderModel} from "../types";
import {axiosHttpClient} from "../../../../shared/api/http";
import {EntityTagHeader} from "../../../../shared/lib";

export const getOrderByOrderNumber = async (orderId: number): Promise<OrderModel> => {
    try {
        const response = await axiosHttpClient.get(`/order/${orderId}`);

        return {...response.data, entityTag: response.headers[EntityTagHeader]};
    } catch (e) {
        return Promise.reject("Error");
    }
}