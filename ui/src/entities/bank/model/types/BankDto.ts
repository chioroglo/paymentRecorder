import {DtoConcurrencyTokenInterface} from "../../../types";

export interface BankDto extends DtoConcurrencyTokenInterface {
    id: number
    name: string
    code: string
}