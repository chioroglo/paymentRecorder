import {BankDto, BankModel} from "../types";
import {axiosHttpClient} from "../../../../shared/api/http";
import {AxiosError} from "axios";
import {ErrorResponse} from "../../../../shared/api/types";
import {EntityTagHeader} from "shared/lib";

export const editBank = async (dto: BankDto): Promise<BankModel> => {
    try {

        const response = await axiosHttpClient.put<BankModel>(`bank/${dto.id}`, dto, {
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