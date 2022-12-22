import React from "react";
import {AgentModel} from "../../../entities/agent/model/types";
import {getAgentByFiscalCode} from "../../../entities/agent/model/api/getAgentByFiscalCode";

export const resetAgentState = async (setStateCallback: React.Dispatch<React.SetStateAction<AgentModel | null>>, agentFiscalCode: number) => {
    try {
        let agent = await getAgentByFiscalCode(agentFiscalCode);
        setStateCallback(agent);
    } catch (e) {
        setStateCallback(null);
    }
}