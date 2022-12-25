import {ModelConcurrencyTokenInterface} from "entities/types";
import {TransactionState, TransactionType} from "../../../../shared/lib/enum";

export interface OrderModel extends ModelConcurrencyTokenInterface {
    "id": number,
    "number": number,
    "emissionDate": string,
    "amount": number,
    "currencyCode": number,
    "currencyName": string,
    "issuerAccountCode": string,
    "issuerFiscalCode": number,
    "issuerAgentName": string,
    "issuerBankName": string,
    "issuerBankCode": string,
    "beneficiaryAccountCode": string,
    "beneficiaryFiscalCode": number,
    "beneficiaryAgentName": string,
    "beneficiaryBankName": string,
    "beneficiaryBankCode": string,
    "destination": string,
    "transactionType": TransactionType,
    "issueDate": string,
    "executionDate"?: string,
    "timezone": string,
    "transactionState": TransactionState
}