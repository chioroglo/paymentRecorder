import {createSlice, PayloadAction} from "@reduxjs/toolkit";
import {applicationUserInitialState} from "../state/applicationUserInitialState";
import {ApplicationUserWithAccessToken} from "../../types";
import {authenticate} from "../thunks/authenticate";
import {actualizeUserUsingRefreshToken} from "../thunks/actualizeUserUsingRefreshToken";

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
        [actualizeUserUsingRefreshToken.fulfilled.type]: (state, action: PayloadAction<ApplicationUserWithAccessToken>) => {
            state.username = action.payload.username;
            state.roles = action.payload.roles;
            state.email = action.payload.email;
            state.accessToken = action.payload.accessToken;
            state.accessTokenExpirationDate = action.payload.accessTokenExpirationDate;
            state.isLoading = false;
            state.isAuthorized = true;
            state.errorMessage = "";
        },
        [actualizeUserUsingRefreshToken.pending.type]: (state) => {
            state.isLoading = true;
            state.errorMessage = "";
        },
        [actualizeUserUsingRefreshToken.rejected.type]: (state, action: PayloadAction<string>) => {
            state.errorMessage = action.payload;
            state.isAuthorized = false;
            state.isLoading = false;
        }

    }
})

export const applicationUserReducer = applicationUserSlice.reducer;
