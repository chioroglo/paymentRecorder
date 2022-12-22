import {DtoConcurrencyTokenInterface} from "../../../types";

export interface AccountDto extends DtoConcurrencyTokenInterface {
    accountCode: string,
    agentId: number,
    bankId: number
}