import React from 'react';
import {useDispatchTyped} from "../../shared/store/hooks/useDispatchTyped";
import {LoginFormFields} from "./lib/LoginFormFields";
import {useSelectorTyped} from "../../shared/store/hooks/useSelectorTyped";
import {useFormik} from "formik";
import * as Yup from "yup";
import {CenteredLoader} from "../../shared/ui/CenteredLoader";
import {Button, Checkbox, FormControl, FormControlLabel, FormHelperText, Input, InputLabel, Paper } from '@mui/material';
import {buttonStyle, checkboxStyle, errorTextStyle, paperStyle, textFieldStyle } from './lib/LoginFormStyles';
import { FormHeader } from 'shared/ui/FormHeader';
import { Link } from "react-router-dom";
import ContactPageIcon from '@mui/icons-material/ContactPage';
import {authenticate} from "../../entities/application-user/model/thunks/authenticate";

export const LoginForm = () => {

    const dispatch = useDispatchTyped();

    const userState = useSelectorTyped(s => s.applicationUserReducer);


    const formik = useFormik<LoginFormFields>({
        initialValues: {
            emailOrUsername: "",
            password: "",
            rememberMe: false
        },
        onSubmit: async (values,formikHelpers) => {
            dispatch(authenticate(values));
        },
        validationSchema: Yup.object({
            emailOrUsername: Yup
                .string()
                .required(),
            password: Yup
                .string()
                .required()
        })
    })

    return (
        <>
            {userState.isLoading ?
                <CenteredLoader/>
                :
                <form style={{display: "inline-block"}} onSubmit={formik.handleSubmit}>

                    <Paper style={paperStyle} elevation={12}>

                        <FormHeader iconColor={"#000"} caption="Login" icon={<ContactPageIcon/>}/>

                        <FormControl style={textFieldStyle}>
                            <InputLabel htmlFor="emailOrUsername">Username</InputLabel>
                            <Input onChange={formik.handleChange} value={formik.values.emailOrUsername} name="emailOrUsername"/>
                            <FormHelperText>
                                {formik.touched.emailOrUsername && formik.errors.emailOrUsername && (
                                    <span style={errorTextStyle}>{formik.errors.emailOrUsername}</span>)}
                            </FormHelperText>
                        </FormControl>

                        <FormControl style={textFieldStyle}>
                            <InputLabel htmlFor="password">Password</InputLabel>
                            <Input type="password" onChange={formik.handleChange} value={formik.values.password}
                                   name="password"/>
                            <FormHelperText>
                                {formik.touched.password && formik.errors.password && (
                                    <span style={errorTextStyle}>{formik.errors.password}</span>)}
                            </FormHelperText>
                        </FormControl>

                        <FormControlLabel name="rememberMe" style={checkboxStyle} onChange={formik.handleChange}
                                          label="Remember me?" control={<Checkbox/>}/>

                        <Button variant="outlined" style={buttonStyle} type="submit">Log in</Button>

                        <Link style={{
                            textAlign: "center",
                            fontStyle: "italic",
                            textDecoration: "bold",
                            color: "blue"
                        }} to="/register">Do not have an account? Click here</Link>
                    </Paper>
                </form>
            }
        </>
    );
};
