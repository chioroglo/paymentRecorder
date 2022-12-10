import axios from "axios";
import {API_URL} from "../config";

export const axiosInstance = axios.create({
    baseURL: API_URL,
    withCredentials: true
})

axiosInstance.interceptors.request.use((config) => {
    if (config?.headers) {
        config.headers["Authorization"] = "Bearer " + sessionStorage.getItem("access-token") || localStorage.getItem("access-token");
    }
    return config;
});