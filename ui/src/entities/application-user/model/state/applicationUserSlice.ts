import {createSlice, PayloadAction} from "@reduxjs/toolkit";
import {applicationUserInitialState} from "./applicationUserInitialState";
import {ApplicationUserDto} from "../types/ApplicationUserDto";
import {login} from "../thunks/login";

export const applicationUserSlice = createSlice({
    name: "applicationUserSlice",
    initialState: applicationUserInitialState,
    reducers: {},
    extraReducers: {
        [login.fulfilled.type]: (state, action: PayloadAction<ApplicationUserDto>) => {

            //console.log(action.payload.accessTokenExpirationDate);
            state.username = action.payload.username;
            state.roles = action.payload.roles;
            state.email = action.payload.email;
            state.accessToken = action.payload.accessToken;
            state.accessTokenExpirationDate = action.payload.accessTokenExpirationDate;
            state.isLoading = false;
            state.isAuthorized = true;
        },
        [login.pending.type]: (state) => {
            state.isLoading = true;
            state.errorMessage = "";
        },
        [login.rejected.type]: (state, action: PayloadAction<string>) => {
            state.errorMessage = action.payload;
            state.isAuthorized = false;
            state.isLoading = true;
        }

    }
})

export const applicationUserReducer = applicationUserSlice.reducer;