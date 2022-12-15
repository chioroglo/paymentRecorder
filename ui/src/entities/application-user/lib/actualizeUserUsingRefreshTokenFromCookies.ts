import {createAsyncThunk} from "@reduxjs/toolkit";
import {axiosInstance} from "../../../shared/api/http";
import {AxiosError} from "axios";
import {ErrorResponse} from "../../../shared/api/types";
import {ApplicationUserDto} from "../types/ApplicationUserDto";

export const actualizeUserUsingRefreshTokenFromCookies = createAsyncThunk(
    'actualizeUserUsingRefreshTokenFromCookies',
    async (_,thunkAPI) => {
        try {
            const response = await axiosInstance.get<ApplicationUserDto>('/auth/get-current-user/');

            return response.data;
            //console.log(response);
        } catch(err) {
            const errorResponse = err as AxiosError<ErrorResponse>;
            return thunkAPI.rejectWithValue(errorResponse.response?.data.Message);
        }
    });