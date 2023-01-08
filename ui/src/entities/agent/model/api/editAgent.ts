import {AgentDto, AgentModel} from "../types";
import {axiosHttpClient} from "../../../../shared/api/http";
import {AxiosError} from "axios";
import {ErrorResponse} from "../../../../shared/api/types";
import {EntityTagHeader} from "shared/lib";

export const editAgent = async (dto: AgentDto): Promise<AgentModel> => {
    try {

        const response = await axiosHttpClient.put<AgentModel>(`agent/${dto.id}`, dto, {
            headers: {
                "If-Match": dto.ifMatch
            }
        });

        return {...response.data, entityTag: response.headers[EntityTagHeader] || ""};
    } catch (e) {
        const err = e as AxiosError<ErrorResponse>;

        return Promise.reject(err.response?.data.Message);
    }
}