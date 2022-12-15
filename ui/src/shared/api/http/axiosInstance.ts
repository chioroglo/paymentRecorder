import axios from "axios";
import {API_URL} from "shared/config/apiConfig";
import {AccessTokenClaim} from "../../../entities/application-user/lib";

export const axiosInstance = axios.create({
    baseURL: API_URL,
    withCredentials: true,
})

axiosInstance.interceptors.request.use((config) => {
    if (config?.headers) {
        config.headers["Authorization"] = "Bearer " + sessionStorage.getItem(AccessTokenClaim) || localStorage.getItem(AccessTokenClaim);
    }
    return config;
});

axiosInstance.interceptors.response.use((response) => {
    console.log(response);
    return Promise.resolve(response);
},(error) => {
    return Promise.reject(error);
});