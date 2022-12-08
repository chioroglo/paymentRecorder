import {instance} from "../../api/http/axiosInstance";

instance.interceptors.request.use((config) => {
    console.log(config);
    return config;
});
