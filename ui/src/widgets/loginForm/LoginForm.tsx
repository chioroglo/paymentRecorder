import React from 'react';
import {useDispatchTyped} from "../../shared/store/hooks/useDispatchTyped";
import {LoginFormFields} from "./lib/LoginFormFields";
import {useSelectorTyped} from "../../shared/store/hooks/useSelectorTyped";
import {useFormik} from "formik";
import * as Yup from "yup";
import {CenteredLoader} from "../../shared/ui/components/CenteredLoader";
import {
    Button,
    Checkbox,
    FormControl,
    FormControlLabel,
    FormHelperText,
    IconButton,
    Input,
    InputLabel,
    Paper
} from '@mui/material';
import {buttonStyle, checkboxStyle, errorTextStyle, paperStyle, textFieldStyle} from './lib/LoginFormStyles';
import {FormHeader} from 'shared/ui/components/FormHeader';
import ContactPageIcon from '@mui/icons-material/ContactPage';
import {authenticate} from "../../entities/application-user/model/thunks";
import {palette} from 'app/ui';
import {useSnackbar, VariantType} from "notistack";
import CloseIcon from '@mui/icons-material/Close';
import {CLIENT_ERROR_OCCURRED} from "../../shared/lib";

export const LoginForm = () => {

    const {enqueueSnackbar, closeSnackbar} = useSnackbar();

    const dispatch = useDispatchTyped();

    const userState = useSelectorTyped(s => s.applicationUserReducer);


    const formik = useFormik<LoginFormFields>({
        initialValues: {
            emailOrUsername: "",
            password: "",
            rememberMe: false
        },
        onSubmit: async (values, formikHelpers) => {
            const actionResult = await dispatch(authenticate(values));


            let message = "";
            let variant: VariantType = "info";

            if (actionResult.meta.requestStatus === "rejected") {
                message = (typeof actionResult.payload === "string" && actionResult.payload) || CLIENT_ERROR_OCCURRED;
                variant = "error";
            } else if (actionResult.meta.requestStatus === "fulfilled") {
                message = "Authorized successfully";
                variant = "success";
            }

            enqueueSnackbar(message, {
                variant: variant,
                action: (key) =>
                    <IconButton onClick={() => closeSnackbar(key)}>
                        <CloseIcon/>
                    </IconButton>
            });

        },
        validationSchema: Yup.object({
            emailOrUsername: Yup
                .string()
                .required("Please introduce email or username"),
            password: Yup
                .string()
                .required("Please introduce your password")
        })
    })

    return (
        <>
            {userState.isLoading ?
                <CenteredLoader/>
                :
                <form style={{display: "inline-block"}} onSubmit={formik.handleSubmit}>

                    <Paper style={paperStyle} elevation={12}>

                        <FormHeader iconColor={palette.ROCK_BLUE} caption="Login" icon={<ContactPageIcon/>}/>

                        <FormControl style={textFieldStyle}>
                            <InputLabel htmlFor="emailOrUsername">Email or username</InputLabel>
                            <Input onChange={formik.handleChange} value={formik.values.emailOrUsername}
                                   name="emailOrUsername"/>
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

                    </Paper>
                </form>
            }
        </>
    );
};
