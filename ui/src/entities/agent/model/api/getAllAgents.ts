import {axiosHttpClient} from "../../../../shared/api/http";
import {AxiosError} from "axios";
import {ErrorResponse} from "../../../../shared/api/types";
import {AgentModel} from "../types";

export const getAllAgents = async (): Promise<AgentModel[]> => {
    try {
        const response = await axiosHttpClient.get<AgentModel[]>("agent");

        return response.data;
    } catch (e) {
        const err = e as AxiosError<ErrorResponse>;

        return Promise.reject(err.response?.data.Message);
    }
}