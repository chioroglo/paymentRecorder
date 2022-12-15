import {Button, Typography} from '@mui/material';
import {authenticate} from 'entities/application-user/model/thunks/authenticate';
import React from 'react';
import {useDispatchTyped} from "../shared/store/hooks/useDispatchTyped";
import {useSelectorTyped} from "../shared/store/hooks/useSelectorTyped";
import {LoginDto} from 'entities/application-user/types';
import {
    actualizeUserUsingRefreshTokenFromCookies
} from "../entities/application-user/lib/actualizeUserUsingRefreshTokenFromCookies";

function App() {
    const dispatch = useDispatchTyped();

    const authState = useSelectorTyped(state => state.applicationUserReducer);

    const handleLogin = async () => {
        const username = prompt();
        const password = prompt();

        dispatch(await authenticate({emailOrUsername: username, password: password, rememberMe: false} as LoginDto));
    }

    const actualize = async() => {
        dispatch(await actualizeUserUsingRefreshTokenFromCookies());
    }

    return (
        <div className="App">
            <p>
                <Button onClick={handleLogin}>Login with credentials</Button>
                <Button onClick={actualize}>ACTUALIZE</Button>
                <Typography>{JSON.stringify(authState)}</Typography>
            </p>
        </div>
    );
}

export {App};
