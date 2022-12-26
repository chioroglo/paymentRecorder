import {BankDto, BankModel} from "../types";
import {axiosHttpClient} from "../../../../shared/api/http";
import {AxiosError} from "axios";
import {ErrorResponse} from "../../../../shared/api/types";
import { EntityTagHeader } from "shared/lib";

export const editBank = async (dto: BankDto,concurrencyToken: string): Promise<BankModel> => {
    try {

        const response = await axiosHttpClient.put<BankModel>("bank",dto,{
            headers: {
                "If-Match": concurrencyToken
            }
        });

        return {...response.data, entityTag: response.headers[EntityTagHeader] || ""};
    } catch (e) {
        const err = e as AxiosError<ErrorResponse>;

        return Promise.reject(err.response?.data.Message);
    }
}