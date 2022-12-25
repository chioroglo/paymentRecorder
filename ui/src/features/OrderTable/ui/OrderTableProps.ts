import {OrderModel} from "entities/order/model/types";

export interface OrderTableProps {
    items: OrderModel[],
    width?: string
}