import {axiosHttpClient} from "../../../../shared/api/http";
import {AxiosError} from "axios";
import {ErrorResponse} from "../../../../shared/api/types";

export const deleteAgent = async (concurrencyToken: string, agentId: number): Promise<void> => {
    try {

        await axiosHttpClient.delete(`bank/${agentId}`, {
            headers: {
                "If-Match": concurrencyToken
            }
        });

        return Promise.resolve();
    } catch (e) {
        const err = e as AxiosError<ErrorResponse>;

        return Promise.reject(err.response?.data.Message);
    }
}