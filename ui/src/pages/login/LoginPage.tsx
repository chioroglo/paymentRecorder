import React from 'react';
import { useTitle } from 'shared/ui/hooks';
import { LoginForm } from 'widgets/loginForm';

export const LoginPage = () => {

    useTitle("Login")

    return (
        <div style={{padding: "15vh 0 0 0", margin: "0 auto", display: "flex", justifyContent: "space-around"}}>
            <LoginForm/>
        </div>
    );
};
