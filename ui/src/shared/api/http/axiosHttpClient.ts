import axios, {AxiosError} from "axios";
import {API_URL} from "shared/config/apiConfig";
import {AccessTokenExpirationDateStorageClaim, AccessTokenStorageClaim} from "../../../entities/application-user/lib";
import {ApplicationUserWithAccessToken} from "../../../entities/application-user/types";

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

    if (err.response?.status !== 401) {
        return Promise.reject(err);
    }

    const newAccessToken = await axiosHttpClient.get<ApplicationUserWithAccessToken>("/auth/get-access-token");
    console.log(newAccessToken);


    const storage = sessionStorage.getItem(AccessTokenStorageClaim) ? sessionStorage : localStorage;

    storage.setItem(AccessTokenStorageClaim, newAccessToken.data?.accessToken);

    storage.setItem(AccessTokenExpirationDateStorageClaim, newAccessToken.data?.accessTokenExpirationDate);

    if (error.response?.config.headers) {
        error.response.config.headers["Authorization"] = `Bearer ${newAccessToken.data.accessToken}`;
        return axios(error.response.config);
    }

    return Promise.reject(err);
});