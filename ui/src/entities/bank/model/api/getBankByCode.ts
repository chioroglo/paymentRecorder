import {BankModel} from "../types";
import {axiosHttpClient} from "../../../../shared/api/http";
import {EntityTagHeader, NO_ENTITY_TOKEN_PROVIDED} from "../../../../shared/lib";
import {AxiosError} from "axios";
import {ErrorResponse} from "../../../../shared/api/types";

export const getBankByCode = async (bankCode: string): Promise<BankModel> => {
    try {
        const response = await axiosHttpClient.get<BankModel>(`bank/get-by-bank-code/${bankCode}`);

        return {...response.data, entityTag: response.headers[EntityTagHeader] || NO_ENTITY_TOKEN_PROVIDED};
    } catch (e) {
        const err = e as AxiosError<ErrorResponse>;

        return Promise.reject(err.response?.data.Message);
    }
}