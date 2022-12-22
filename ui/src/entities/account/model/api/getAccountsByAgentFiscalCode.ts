import {axiosHttpClient} from "../../../../shared/api/http";
import {AxiosError} from "axios";
import {ErrorResponse} from "../../../../shared/api/types";
import {AccountModel} from "../types";

export const getAccountsByAgentId = async (agentId: number): Promise<AccountModel[]> => {
    try {
        const response = await axiosHttpClient.get<AccountModel[]>(`agent/get-by-agent-id/${agentId}`);

        return response.data;
        //return {...response.data, entityTag: response.headers[EntityTagHeader]};
    } catch (e) {
        const err = e as AxiosError<ErrorResponse>;

        return Promise.reject(err.response?.data.Message);
    }
}