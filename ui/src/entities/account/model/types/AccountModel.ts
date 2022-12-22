import {ModelConcurrencyTokenInterface} from "../../../types";

export interface AccountModel extends ModelConcurrencyTokenInterface {
    id: number,
    accountCode: string,
    "ownerName": string,
    agentFiscalCode: number,
    bankName: string,
    bankCode: string,
    amountOfOutcomingOrders: number,
    amountOfIncomingOrders: number
}