import {ModelConcurrencyTokenInterface} from "../../../types";

export interface BankModel extends ModelConcurrencyTokenInterface {
    id: number,
    name: string,
    code: string
}