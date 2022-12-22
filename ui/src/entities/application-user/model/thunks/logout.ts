import {createAsyncThunk} from "@reduxjs/toolkit";
import {axiosHttpClient} from "../../../../shared/api/http";
import {AxiosError} from "axios";
import {ErrorResponse} from "../../../../shared/api/types";

export const logout = createAsyncThunk(
    'applicationUser/logout',
    async (_, thunkAPI) => {

        try {
            let response = await axiosHttpClient.post('/auth/logout');


            return response.data;
        } catch (err) {
            const errorResponse = err as AxiosError<ErrorResponse>;

            return thunkAPI.rejectWithValue(errorResponse.response?.data.Message);
        } finally {
            localStorage.clear();
            sessionStorage.clear();
        }
    }
)