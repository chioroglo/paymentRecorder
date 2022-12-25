import React from "react";
import {AccountModel} from "../../../entities/account/model/types";
import {getAccountsByAgentFiscalCode} from "../../../entities/account/model/api/getAccountsByAgentFiscalCode";

export const refetchAccountListState = async (setStateCallback: React.Dispatch<React.SetStateAction<AccountModel[]>>, agentId: number) => {
    try {
        let accounts = await getAccountsByAgentFiscalCode(agentId);
        setStateCallback(accounts);
    } catch (e) {
        setStateCallback([]);
    }
}