import {ModelConcurrencyTokenInterface} from "../../../types";

export interface AgentModel extends ModelConcurrencyTokenInterface {
    id: number,
    name: "string",
    typeId: number,
    type: string,
    fiscalCode: number,
    numberOfAccounts: number
}