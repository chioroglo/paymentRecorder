import {combineReducers, configureStore} from "@reduxjs/toolkit";
import {applicationUserReducer} from "../../entities/application-user/model/state/applicationUserSlice";

export const rootReducer = combineReducers({
    applicationUserReducer
});

export const setupStore = () => {
    return configureStore({
        reducer: rootReducer,
        middleware: (getDefaultMiddleware) => getDefaultMiddleware({
            serializableCheck: false
        })
    })
}
