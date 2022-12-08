import {axiosInstance } from "../axiosInstance";

axiosInstance.interceptors.request.use((config) => {
    console.log(config);
    return config;
});
