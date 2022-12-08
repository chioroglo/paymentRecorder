import axios from "axios";
import {API_URL} from "../../axios/config";

const instance = axios.create({
    withCredentials: false,
    baseURL : API_URL
})


export { instance };