import {axiosHttpClient} from "../../../../shared/api/http";
import {EntityTagHeader} from "../../../../shared/lib";
import {AxiosError} from "axios";
import {ErrorResponse} from "../../../../shared/api/types";
import { AgentDto, AgentModel } from "../types";

export const addNewAgent = async (dto: AgentDto): Promise<AgentModel> => {
    try {
        const response = await axiosHttpClient.post<AgentModel>("agent", dto);

        return {...response.data, entityTag: response.headers[EntityTagHeader] || ""};
    } catch (e) {
        const err = e as AxiosError<ErrorResponse>;

        return Promise.reject(err.response?.data.Message);
    }
}