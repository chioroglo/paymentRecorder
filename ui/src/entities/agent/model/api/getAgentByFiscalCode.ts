import {axiosHttpClient} from "../../../../shared/api/http";
import {AxiosError} from "axios";
import {ErrorResponse} from "../../../../shared/api/types";
import {AgentModel} from "../types";
import {EntityTagHeader, NO_ENTITY_TOKEN_PROVIDED} from "../../../../shared/lib";

export const getAgentByFiscalCode = async (fiscalCode: number): Promise<AgentModel> => {
    try {
        const response = await axiosHttpClient.get<AgentModel>(`agent/get-by-fiscal-code/${fiscalCode}`);

        return {...response.data, entityTag: response.headers[EntityTagHeader] || NO_ENTITY_TOKEN_PROVIDED};
    } catch (e) {
        const err = e as AxiosError<ErrorResponse>;

        return Promise.reject(err.response?.data.Message);
    }
}