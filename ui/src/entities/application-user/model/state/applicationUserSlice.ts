import {createSlice, PayloadAction} from "@reduxjs/toolkit";
import {applicationUserInitialState} from "./applicationUserInitialState";
import {ApplicationUserWithAccessToken} from "../../types/ApplicationUserWithAccessToken";
import {authenticate} from "../thunks/authenticate";
import {actualizeUserUsingRefreshTokenFromCookies} from "../../lib/actualizeUserUsingRefreshTokenFromCookies";

export const applicationUserSlice = createSlice({
    name: "applicationUserSlice",
    initialState: applicationUserInitialState,
    reducers: {},
    extraReducers: {
        [authenticate.fulfilled.type]: (state, action: PayloadAction<ApplicationUserWithAccessToken>) => {
            state.username = action.payload.username;
            state.roles = action.payload.roles;
            state.email = action.payload.email;
            state.accessToken = action.payload.accessToken;
            state.accessTokenExpirationDate = action.payload.accessTokenExpirationDate;
            state.isLoading = false;
            state.isAuthorized = true;
            state.errorMessage = "";
        },
        [authenticate.pending.type]: (state) => {
            state.isLoading = true;
            state.errorMessage = "";
        },
        [authenticate.rejected.type]: (state, action: PayloadAction<string>) => {
            state.errorMessage = action.payload;
            state.isAuthorized = false;
            state.isLoading = false;
        },
        [actualizeUserUsingRefreshTokenFromCookies.fulfilled.type]: (state,action) => {
            state.isLoading = false;
        },
        [actualizeUserUsingRefreshTokenFromCookies.pending.type]: (state,action) => {
            state.isLoading = true;
        },
        [actualizeUserUsingRefreshTokenFromCookies.rejected.type]: (state,action) => {
            state.isLoading = false;
        }

    }
})

export const applicationUserReducer = applicationUserSlice.reducer;