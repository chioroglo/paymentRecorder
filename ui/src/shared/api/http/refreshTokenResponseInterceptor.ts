import {axiosHttpClient} from ".";
import axios, {AxiosError, AxiosResponse} from "axios";
import {AccessTokenExpirationDateStorageClaim, AccessTokenStorageClaim} from "../../../entities/application-user/lib";
import {AccessToken} from "../../../entities/application-user/types";

function refreshTokenResponseInterceptor() {
    console.log('refreshtokeninterceptor called');
    const interceptor = axiosHttpClient.interceptors.response.use((response) => response,
        (error: AxiosError) => {
            if (error.response?.status !== 401) {
                return Promise.reject(error);
            }

            axiosHttpClient.interceptors.response.eject(interceptor);

            return axiosHttpClient.post("/auth/exchange-refresh-token").then((response: AxiosResponse<AccessToken>) => {

                const storage = sessionStorage.getItem(AccessTokenStorageClaim) ? sessionStorage : localStorage;

                storage.setItem(AccessTokenStorageClaim, response.data.token);

                storage.setItem(AccessTokenExpirationDateStorageClaim, response.data.expirationDate);

                if (error.response?.config.headers) {
                    error.response.config.headers["Authorization"] = `Bearer ${response.data.token}`;
                    return axios(error.response.config);
                }
            }).catch(error2 => {
                return Promise.reject(error2);
            })
                .finally(refreshTokenResponseInterceptor);

        })
}

refreshTokenResponseInterceptor()