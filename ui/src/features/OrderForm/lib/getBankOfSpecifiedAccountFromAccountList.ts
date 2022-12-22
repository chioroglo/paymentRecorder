import {BankDto} from "entities/bank/model/types";
import {AccountModel} from "../../../entities/account/model/types";
// AccountDto is being used because on OrderForm there is no need of bank id and we may handle form using BankDto
export const getBankOfSpecifiedAccountFromAccountList = (accounts: AccountModel[], accountCode: string): BankDto | null => {
    const account = accounts.find(acc => acc.accountCode === accountCode);

    return account ? {name: account.bankName, code: account.bankCode} : null
}