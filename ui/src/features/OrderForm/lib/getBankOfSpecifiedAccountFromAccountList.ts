import {BankDto} from "entities/bank/model/types";
import {AccountModel} from "../../../entities/account/model/types";
// AccountDto is being used because on OrderForm there is no need of bank id and we may handle form using BankDto
export const getBankOfSpecifiedAccountFromAccountList = (accounts: AccountModel[], accountCode: string): BankDto | null => {
    const account = accounts.find(acc => acc.accountCode === accountCode);

    // ifMatch and id field may be omitted because it only used as model
    return account ? {id:0,name: account.bankName, code: account.bankCode,ifMatch:""} : null
}