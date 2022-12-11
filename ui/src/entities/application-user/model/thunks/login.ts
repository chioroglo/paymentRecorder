import {createAsyncThunk} from "@reduxjs/toolkit";
import {axiosInstance} from "../../../../shared/api/http";
import { ApplicationUserDto } from "../types/ApplicationUserDto";

export const login = createAsyncThunk(
    'applicationUser/login',
    async (data: {password: string,emailOrUsername: string},thunkAPI) => {
        try {
            const response = await axiosInstance.post<ApplicationUserDto>('/auth/login/', {
                password: data.password,
                emailOrUsername: data.emailOrUsername
            });
            return response.data;
        } catch(e) {
            console.log(e);
            return thunkAPI.rejectWithValue("Error occured while authorizing");
        }
    })