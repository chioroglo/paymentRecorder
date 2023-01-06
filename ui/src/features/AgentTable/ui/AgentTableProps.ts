import {AgentModel} from "entities/agent/model/types";

export interface AgentTableProps {
    items: AgentModel[],
    width?: string
}