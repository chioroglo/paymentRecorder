import { ModelConcurrencyTokenInterface } from "entities/types";

export interface OrderModel extends ModelConcurrencyTokenInterface{
    "id": number,
    "number": number,
    "emissionDate": string,
    "amount": number,
    "currencyCode": number,
    "currencyName": string,
    "issuerAccountCode": string,
    "issuerFiscalCode": string,
    "issuerAgentName": string,
    "issuerBankName": string,
    "issuerBankCode": string,
    "beneficiaryAccountCode": string,
    "beneficiaryFiscalCode": string,
    "beneficiaryAgentName": string,
    "beneficiaryBankName": string,
    "beneficiaryBankCode": string,
    "destination": string,
    "transactionType": string,
    "issueDate": string,
    "executionDate": string,
    "timezone": string,
    "transactionState": string
}