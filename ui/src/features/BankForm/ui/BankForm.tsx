import {Button, FormControl, Paper, TextField, Typography} from '@mui/material';
import React from 'react';
import {useFormik} from "formik";
import {BankDto} from 'entities/bank/model/types';
import {BankFormProps} from "./BankFormProps";
import * as Yup from "yup";

const BankForm = ({
                      formActionCallback, width = "100%", caption, initialValue = {
        code: "",
        name: "",
        id: 0,
        ifMatch: ""
    }
                  }: BankFormProps) => {


    const formik = useFormik<BankDto>({
        initialValues: initialValue,
        onSubmit: async (dto, formikHelpers) => {
            await formActionCallback(dto);
        },
        validationSchema: Yup.object({
            name: Yup.string()
                .required(),
            code: Yup.string()
                .required(),
            id: Yup.number()
                .optional(),
            ifMatch: Yup.string()
                .optional()
        })
    });

    return (
        <form style={{display: "inline-block"}} onSubmit={formik.handleSubmit}>
            <Paper sx={{width: width, display: "flex", flexDirection: "column", padding: "20px"}} elevation={12}>
                <Typography textAlign={"center"}>{caption}</Typography>
                <FormControl>
                    <TextField style={{margin: "20px"}} label="Swift Code" error={!!(formik.errors.code)}
                               onChange={formik.handleChange}
                               value={formik.values.code}
                               helperText={formik.touched.code && formik.errors.code}
                               name="code"/>
                </FormControl>

                <FormControl>
                    <TextField style={{margin: "20px"}} label="Bank name"
                               error={!!(formik.errors.name)}
                               onChange={formik.handleChange}
                               value={formik.values.name}
                               helperText={formik.touched.name && formik.errors.name}
                               name="name"/>
                </FormControl>

                <Button variant="contained" type="submit">
                    SUBMIT
                </Button>
            </Paper>
        </form>
    );
};

export {BankForm};