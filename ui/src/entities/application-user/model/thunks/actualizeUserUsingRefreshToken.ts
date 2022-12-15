import {createAsyncThunk} from "@reduxjs/toolkit";
import {axiosInstance} from "../../../../shared/api/http";
import {AxiosError} from "axios";
import {ErrorResponse} from "../../../../shared/api/types";
import {ApplicationUserWithAccessToken} from "../../types/ApplicationUserWithAccessToken";

export const actualizeUserUsingRefreshToken = createAsyncThunk(
    'actualizeUserUsingRefreshToken',
    async (_,thunkAPI) => {
        try {
            const response = await axiosInstance.get<ApplicationUserWithAccessToken>('/auth/get-access-token/');

            return response.data;
        } catch(err) {
            const errorResponse = err as AxiosError<ErrorResponse>;
            return thunkAPI.rejectWithValue(errorResponse.response?.data.Message);
        }
    });