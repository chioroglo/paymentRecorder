import {Button, Typography} from '@mui/material';
import { login } from 'entities/application-user/model/thunks/login';
import React, {useEffect} from 'react';
import {useDispatchTyped} from "../shared/store/hooks/useDispatchTyped";
import {useSelectorTyped} from "../shared/store/hooks/useSelectorTyped";

function App() {
    const dispatch = useDispatchTyped();
    const state = useSelectorTyped(state => state.applicationUserReducer);

    const logMe = () => {
        dispatch(login({emailOrUsername:"alexandr.chioroglo",password:"Qwerty123___"}));
    }

    return (
        <div className="App">
            <p>
                <Button onClick={logMe}>Login with credentials</Button>
                <Typography>{JSON.stringify(state)}</Typography>*
            </p>
        </div>
    );
}

export {App};
