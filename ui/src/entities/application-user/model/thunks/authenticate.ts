import {createAsyncThunk} from "@reduxjs/toolkit";
import {axiosInstance} from "../../../../shared/api/http";
import {ApplicationUserWithAccessToken} from "../../types";
import {
    AccessTokenStorageClaim,
    AccessTokenExpirationDateStorageClaim,
    EmailStorageClaim, RolesStorageClaim,
    UsernameStorageClaim
} from "../../lib"
import {LoginDto} from "../../types";
import { AxiosError } from "axios";
import {ErrorResponse} from "../../../../shared/api/types";

export const authenticate = createAsyncThunk(
    'applicationUser/authenticate',
    async (data: LoginDto, thunkAPI) => {
        try {

            let response = await axiosInstance.post<ApplicationUserWithAccessToken>('/auth/login/', {
                password: data.password,
                emailOrUsername: data.emailOrUsername
            });

            const storage: Storage = data.rememberMe ? localStorage : sessionStorage;
            storage.setItem(AccessTokenStorageClaim, response.data.accessToken);
            storage.setItem(AccessTokenExpirationDateStorageClaim, response.data.accessTokenExpirationDate);
            storage.setItem(UsernameStorageClaim, response.data.username);
            storage.setItem(EmailStorageClaim,response.data.email);
            storage.setItem(RolesStorageClaim,response.data.roles.toString());

            return response.data;
        } catch(err) {
            const errorResponse = err as AxiosError<ErrorResponse>;

            return thunkAPI.rejectWithValue(errorResponse.response?.data.Message);
        }

    })