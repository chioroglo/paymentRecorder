import {axiosHttpClient} from "../../../../shared/api/http";
import {AxiosError} from "axios";
import {ErrorResponse} from "../../../../shared/api/types";

export const deleteBank = async (concurrencyToken: string,bankId: number): Promise<void> => {
    try {

        await axiosHttpClient.delete(`bank/${bankId}`,{
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