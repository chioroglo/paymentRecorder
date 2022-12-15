import {combineReducers, configureStore} from "@reduxjs/toolkit";
import {applicationUserReducer} from "../../entities/application-user/model/slices/applicationUserSlice";

export const rootReducer = combineReducers({
    applicationUserReducer
});

export const setupStore = () => {
    return configureStore({
        reducer: rootReducer
    })
}
