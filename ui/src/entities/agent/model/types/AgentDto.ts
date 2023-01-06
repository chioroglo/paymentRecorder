import {DtoConcurrencyTokenInterface} from "../../../types";
import {LegalAgentType} from "../../../../shared/lib/enum";

export interface AgentDto extends DtoConcurrencyTokenInterface {
    id: number,
    name: string,
    fiscalCode: number,
    type: LegalAgentType
}