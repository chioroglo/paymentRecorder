import axios from "axios";
import {API_URL} from "./config";

export const axiosInstance = axios.create({
    withCredentials: false,
    baseURL : API_URL
})
