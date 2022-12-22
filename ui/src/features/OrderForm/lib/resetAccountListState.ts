import React from "react";
import {AccountModel} from "../../../entities/account/model/types";
import {getAccountsByAgentFiscalCode} from "../../../entities/account/model/api/getAccountsByAgentFiscalCode";

export const resetAccountListState = async (setStateCallback: React.Dispatch<React.SetStateAction<AccountModel[]>>, agentId: number) => {
    let accounts = await getAccountsByAgentFiscalCode(agentId);
    setStateCallback(accounts);
}