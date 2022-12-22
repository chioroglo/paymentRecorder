import {TransactionState, TransactionType} from "../../../shared/lib/enum";
import {DtoConcurrencyTokenInterface} from "../../../entities/types";

export interface OrderDto extends DtoConcurrencyTokenInterface{
    number: number,
    emissionDate: string,
    amount: number,
    currencyCode: number,
    issuerAccountCode: string,
    issuerFiscalCode: number,
    beneficiaryAccountCode: string,
    beneficiaryFiscalCode: number,
    destination: string,
    transactionType: TransactionType,
    transactionState: TransactionState,
    issueDate: string,
    executionDate?: string,
    timezone: string,
}