import {ModelConcurrencyTokenInterface} from "../../../types";
import {LegalAgentType} from "../../../../shared/lib/enum";

export interface AgentModel extends ModelConcurrencyTokenInterface {
    id: number,
    name: string,
    typeId: LegalAgentType,
    type: string,
    fiscalCode: number,
    numberOfAccounts: number
}