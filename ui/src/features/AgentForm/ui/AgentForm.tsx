import {AgentDto} from 'entities/agent/model/types';
import {useFormik} from 'formik';
import React from 'react';
import {AgentFormProps} from "./AgentFormProps";
import {LegalAgentType} from "../../../shared/lib/enum";
import * as Yup from "yup";
import {Button, MenuItem, Paper, Select, SelectChangeEvent, TextField, Typography} from '@mui/material';
import "./styles/AgentForm.css";
import {fiscalCodeRegex} from "../../../shared/lib";

const legalAgentTypes = {
    "Physical": LegalAgentType.Physical,
    "Juridical": LegalAgentType.Juridical
}

const AgentForm = ({
                       initialValue = {
                           id: 0,
                           ifMatch: "",
                           name: "",
                           fiscalCode: 1000000000000,
                           type: LegalAgentType.Physical
                       }, formActionCallback, width = "100%", caption
                   }: AgentFormProps) => {


    const formik = useFormik<AgentDto>({
        initialValues: initialValue,
        onSubmit: async (dto, formikHelpers) => await formActionCallback(dto),
        validationSchema: Yup.object({
            id: Yup.number()
                .optional(),
            ifMatch: Yup.string()
                .optional(),
            fiscalCode: Yup.string()
                .matches(fiscalCodeRegex, "Please introduce valid fiscal code")
                .required(),
            name: Yup.string()
                .required(),
            type: Yup.number()
                .required()
        })
    });

    return (
        <form style={{display: 'block'}} onSubmit={formik.handleSubmit}>
            <Paper elevation={12} className="agent-form-container" sx={{width: width}}>
                <Typography textAlign={"center"}>{caption}</Typography>
                <TextField className="text-input-field" label="Fiscal Code"
                           error={!!(formik.errors.fiscalCode)}
                           onChange={formik.handleChange}
                           value={formik.values.fiscalCode}
                           type="text"
                           helperText={formik.touched.fiscalCode && formik.errors.fiscalCode}
                           name="fiscalCode"
                />

                <TextField className="text-input-field" label="Agent name"
                           error={!!(formik.errors.name)}
                           onChange={formik.handleChange}
                           value={formik.values.name}
                           type="text"
                           helperText={formik.touched.name && formik.errors.name}
                           name="name"
                />

                <Select placeholder="Legal person type"
                        style={{width: width}}
                        id="type"
                        name="type"
                        onChange={(e: SelectChangeEvent<string>) => formik.setFieldValue("type", legalAgentTypes[e.target.value as keyof typeof legalAgentTypes])}
                        value={Object.keys(legalAgentTypes).find(key => legalAgentTypes[key as keyof typeof legalAgentTypes] === formik.values.type)}
                        MenuProps={{className: "dropdown"}}
                        error={!!formik.errors.type}
                >
                    {Object.keys(legalAgentTypes).map(t => <MenuItem value={t} key={t}>{t}</MenuItem>)}
                </Select>

                <Button type="submit" variant="contained">
                    SUBMIT
                </Button>
            </Paper>
        </form>
    );
};

export {AgentForm};