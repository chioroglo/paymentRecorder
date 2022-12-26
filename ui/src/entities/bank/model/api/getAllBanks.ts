import {BankModel} from "../types";
import {axiosHttpClient} from "../../../../shared/api/http";
import {AxiosError} from "axios";
import {ErrorResponse} from "../../../../shared/api/types";

export const getAllBanks = async (): Promise<BankModel[]> => {
    try {
        const response = await axiosHttpClient.get<BankModel[]>("bank");

        return response.data;
    } catch (e) {
        const err = e as AxiosError<ErrorResponse>;

        return Promise.reject(err.response?.data.Message);
    }
}