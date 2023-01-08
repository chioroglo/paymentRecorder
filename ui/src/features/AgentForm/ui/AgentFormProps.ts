import {AgentDto} from "entities/agent/model/types";

export interface AgentFormProps {
    initialValue?: AgentDto,
    width?: string,
    caption: string,
    formActionCallback: (dto: AgentDto) => Promise<void>
}