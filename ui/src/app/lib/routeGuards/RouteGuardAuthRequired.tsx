import React from 'react';
import {Navigate, useLocation} from 'react-router-dom';
import {useSelectorTyped} from "../../../shared/store/hooks/useSelectorTyped";

export const RouteGuardAuthRequired = ({children}: { children: JSX.Element }) => {

    const location = useLocation();


    const userIsAuthorized = useSelectorTyped(s => s.applicationUserReducer).isAuthorized;

    if (userIsAuthorized) {
        return children
    } else {
        return <Navigate to="/login" state {...{from: location}} replace/>
    }
};
