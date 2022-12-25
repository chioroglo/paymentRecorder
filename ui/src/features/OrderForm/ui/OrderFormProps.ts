import {OrderDto, OrderModel} from "../../../entities/order/model/types";

export interface OrderFormProps {
    formActionCallback: (order: OrderDto) => void,
    initialOrder?: OrderModel
}