import {Navigate, useLocation} from "react-router-dom";
import {useSelectorTyped} from "../../../shared/store/hooks/useSelectorTyped";

export const RouteGuardUnauthorizedOnly = ({children}: { children: JSX.Element }) => {

    const location = useLocation();

    const userStateIsAuthorized = useSelectorTyped(state => state.applicationUserReducer).isAuthorized;

    const fromPage = location.state?.from?.pathname || "/";

    if (!userStateIsAuthorized) {
        return children
    } else {
        return <Navigate to={fromPage} state {...{from: location}} replace/>
    }
};
