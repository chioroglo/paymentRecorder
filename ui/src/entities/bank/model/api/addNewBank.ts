import {BankDto, BankModel} from "../types";
import {axiosHttpClient} from "../../../../shared/api/http";
import {AxiosError} from "axios";
import {ErrorResponse} from "../../../../shared/api/types";
import {EntityTagHeader} from "shared/lib";

export const addNewBank = async (dto: BankDto): Promise<BankModel> => {
    try {
        const response = await axiosHttpClient.post<BankModel>("bank", dto);

        return {...response.data, entityTag: response.headers[EntityTagHeader] || ""};
    } catch (e) {
        const err = e as AxiosError<ErrorResponse>;

        return Promise.reject(err.response?.data.Message);
    }
}