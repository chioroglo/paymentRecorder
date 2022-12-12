import {Button, Typography} from '@mui/material';
import {authenticate} from 'entities/application-user/model/thunks/authenticate';
import React from 'react';
import {useDispatchTyped} from "../shared/store/hooks/useDispatchTyped";
import {useSelectorTyped} from "../shared/store/hooks/useSelectorTyped";
import {LoginDto} from 'entities/application-user/service/dto';

function App() {
    const dispatch = useDispatchTyped();


    const authState = useSelectorTyped(state => state.applicationUserReducer);

    const handleLogin = async () => {
        const username = prompt();
        const password = prompt();

        dispatch(await authenticate({emailOrUsername: username, password: password, rememberMe: true} as LoginDto));
    }

    return (
        <div className="App">
            <p>
                <Button onClick={handleLogin}>Login with credentials</Button>
                <Typography>{JSON.stringify(authState)}</Typography>*
            </p>
        </div>
    );
}

export {App};
