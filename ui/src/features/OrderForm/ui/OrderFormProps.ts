import {OrderModel} from "../../../entities/order/model/types";
import {OrderDto} from "../types";

export interface OrderFormProps {
    formActionCallback: (order: OrderDto) => void,
    initialOrder?: OrderModel
}