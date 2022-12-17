import axios, {AxiosError} from "axios";
import {API_URL} from "shared/config/apiConfig";
import {AccessTokenStorageClaim} from "../../../entities/application-user/lib";

export const axiosHttpClient = axios.create({
    baseURL: API_URL,
    withCredentials: true,
})

axiosHttpClient.interceptors.request.use((config) => {

    if (config?.headers) {
        config.headers["Authorization"] = "Bearer " + sessionStorage.getItem(AccessTokenStorageClaim) || localStorage.getItem(AccessTokenStorageClaim);
    }
    return config;
});


// https://stackoverflow.com/questions/51646853/automating-access-token-refreshing-via-interceptors-in-axios
// make interceptors to refresh accessToken;
axiosHttpClient.interceptors.response.use((response) => {
    console.log(response);
    return Promise.resolve(response);
}, async (error) => {
    const err = error as AxiosError;
    console.log(err);
});