import { BankModel } from "entities/bank/model/types";

export interface BankTableProps {
    width?: string,
    height?: string,
    items: BankModel[],
    pageSize?: number
}