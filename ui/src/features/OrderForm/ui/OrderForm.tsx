import React, {useEffect, useState} from 'react';
import {
    Box,
    Button,
    FormControl,
    FormHelperText,
    Input,
    InputLabel,
    MenuItem,
    Paper,
    Select,
    SelectChangeEvent,
    TextField,
    Typography
} from "@mui/material";
import {OrderCardGridCell} from "../../../entities/order/ui";
import {useFormik} from "formik";
import {OrderDto} from "../types";
import {TransactionState, TransactionType} from "../../../shared/lib/enum";
import * as Yup from "yup";
import "./styles/OrderForm.css";
import {DateTimePicker} from "@mui/x-date-pickers";
import {Moment} from "moment";
import * as currencyCodes from "currency-codes";
import {CenteredLoader} from "../../../shared/ui/components/CenteredLoader";
import {AgentModel} from 'entities/agent/model/types';
import {AccountModel} from "../../../entities/account/model/types";
import {isValidFiscalCode} from "../../../shared/lib";
import {
    getBankOfOneAccountFromAccountList,
    getListOfKeysFromDictionary,
    orderDestinationMaxLength,
    resetAccountListState,
    resetAgentState
} from '../lib';
import {OrderFormProps} from "./OrderFormProps";
import { findWindows } from "windows-iana";
import {useSnackbar, VariantType} from "notistack";

const errorTextClassName = "error-text";

const FiscalCodeValidationErrorMessage = "Please enter valid fiscal code"
const sortedCurrencyList = currencyCodes.data.sort((left, right) => left.currency.localeCompare(right.currency));

const transactionTypes = new Map<string, TransactionType>([["Regular", TransactionType.Regular], ["Urgent", TransactionType.Urgent]]);
const transactionStates = new Map<string, TransactionState>([["Completed", TransactionState.Completed], ["Pending", TransactionState.Pending],["Cancelled",TransactionState.Cancelled]])

const transactionTypeNamesForDropdown = getListOfKeysFromDictionary<TransactionType>(transactionTypes);
const transactionStateNamesForDropdown = getListOfKeysFromDictionary<TransactionState>(transactionStates);

const OrderForm = ({initialOrder, formActionCallback}: OrderFormProps) => {


    const [isLoading, setLoading] = useState<boolean>(false);
    // for dropdowns and previews
    const [issuerAgentInfo, setIssuerAgentInfo] = useState<AgentModel | null>(null);
    const [beneficiaryAgentInfo, setBeneficiaryAgentInfo] = useState<AgentModel | null>(null);


    const [beneficiaryAccountsDropdownRange, setBeneficiaryAccountsDropdownRange] = useState<AccountModel[]>([]);
    const [issuerAccountsDropdownRange, setIssuerAccountsDropdownRange] = useState<AccountModel[]>([]);

    const formik = useFormik<OrderDto>({
        initialValues: {
            number: 0,
            emissionDate: "",
            amount: 0,
            currencyCode: 978, // EUR
            issuerAccountCode: "",
            issuerFiscalCode: 0,
            beneficiaryAccountCode: "",
            beneficiaryFiscalCode: 0,
            destination: "",
            transactionType: TransactionType.Regular,
            transactionState: TransactionState.Completed,
            executionDate: undefined,
            issueDate: "",
            timezone: findWindows(Intl.DateTimeFormat().resolvedOptions().timeZone)[0],
            ifMatch:""
        },
        onSubmit: async (dto, formikHelpers) => {
            await formActionCallback(dto);
        },
        validationSchema: Yup.object({
            number: Yup.number()
                .required("Please specify order number")
                .positive("Order number should be positive"),
            emissionDate: Yup.date()
                .required("Please specify valid emission date"),
            amount: Yup.number()
                .positive("Amount should be positive")
                .required("Please specify amount of money"),
            currencyCode: Yup.number()
                .required("Please specify currency code")
                .integer(),
            issuerFiscalCode: Yup.string()
                .length(13, FiscalCodeValidationErrorMessage)
                .required(FiscalCodeValidationErrorMessage),
            issuerAccountCode: Yup.string()
                .required("Please specify an account")
                .notOneOf([Yup.ref("beneficiaryAccountCode")], "Please specify different accounts for transfer"),
            beneficiaryFiscalCode: Yup.string()
                .length(13, FiscalCodeValidationErrorMessage)
                .required(FiscalCodeValidationErrorMessage),
            beneficiaryAccountCode: Yup.string()
                .required("Please specify an account")
                .notOneOf([Yup.ref("issuerAccountCode")], "Please specify different accounts for transfer"),
            destination: Yup.string()
                .required("Please specify destination")
                .max(orderDestinationMaxLength),
            transactionType: Yup.number()
                .required("PLease specify transaction type"),
            issueDate: Yup.date()
                .required("Please specify issue date"),
            executionDate: Yup.date()
                .optional()
        })
    })

    return (isLoading ?
            <CenteredLoader/>
            : <form onSubmit={formik.handleSubmit} style={{display: "inline-block", width: "100%"}}>
                <Paper variant="outlined" square>
                    <Box display="flex">
                        <OrderCardGridCell>PAYMENT ORDER NO.</OrderCardGridCell>

                        <OrderCardGridCell>
                            <Box display="flex" alignItems="center">
                                <Typography>№</Typography>
                                <FormControl>
                                    <TextField id="number" label={"Order number"}
                                               error={!!(formik.errors.number)}
                                               helperText={formik.errors.number} onChange={formik.handleChange}
                                               value={formik.values.number} type="number" fullWidth name="number"/>
                                </FormControl>
                            </Box>
                        </OrderCardGridCell>

                        <OrderCardGridCell style={{flexGrow: 1}}>
                            <FormControl>
                                <DateTimePicker label="Emission date" renderInput={(params) =>
                                    <TextField {...params}
                                               error={!!(formik.errors.emissionDate)}
                                               helperText={formik.errors.emissionDate}/>}
                                                value={formik.values.emissionDate}
                                                onChange={(value: Moment | null) => {
                                                    if (value?.isValid()) {
                                                        formik.setFieldValue("emissionDate", value);
                                                    }
                                                }}/>
                            </FormControl>
                        </OrderCardGridCell>
                    </Box>

                    <Box display="flex">
                        <OrderCardGridCell style={{display: "flex", alignItems: "center"}}>
                            <Typography>AMOUNT:</Typography>
                            <FormControl>
                                <TextField id="amount" label="Amount"
                                           error={!!(formik.errors.amount)}
                                           helperText={formik.errors.amount} onChange={formik.handleChange}
                                           value={formik.values.amount} type="number" name="amount"/>
                            </FormControl>
                        </OrderCardGridCell>

                        <OrderCardGridCell
                            style={{flexGrow: 1, display: "flex", justifyContent: "center", alignItems: "center"}}>

                            <Typography>{formik.values.amount}</Typography>
                            <FormControl>
                                <InputLabel variant="standard" htmlFor="currencyCode">Currency Code</InputLabel>
                                <Select style={{minWidth: "200px"}} label="Currency Code" id="currencyCode"
                                        name="currencyCode"
                                        onChange={formik.handleChange}
                                        value={formik.values.currencyCode} MenuProps={{style: {maxHeight: "400px"}}}>
                                    {sortedCurrencyList.map((currency) =>
                                        <MenuItem key={currency.number}
                                                  value={currency.number}>{currency.currency}</MenuItem>)}
                                </Select>
                            </FormControl>
                        </OrderCardGridCell>

                    </Box>

                    {/* ISSUER SECTION*/}

                    <Box display="flex">
                        <OrderCardGridCell style={{flexGrow: 1}}>
                            {`ISSUER: ${issuerAgentInfo?.name || ""}`}
                        </OrderCardGridCell>

                        <Box>
                            <OrderCardGridCell>
                                <FormControl>
                                    <InputLabel variant="standard" htmlFor="issuerAccountCode">Issuer Acc.
                                        Code</InputLabel>
                                    <Select disabled={!!(formik.errors.issuerFiscalCode)} style={{minWidth: "200px"}}
                                            id="issuerAccountCode"
                                            name="issuerAccountCode" onChange={formik.handleChange}
                                            value={formik.values.issuerAccountCode}
                                            MenuProps={{style: {maxHeight: "400px"}}}
                                            error={!!(formik.errors.issuerAccountCode)}>
                                        {issuerAccountsDropdownRange.map((acc) => <MenuItem value={acc.accountCode}
                                                                                            key={acc.id}>{acc.accountCode}</MenuItem>)}
                                    </Select>
                                    <FormHelperText
                                        error={!!(formik.errors.issuerAccountCode)}>{formik.errors.issuerAccountCode}</FormHelperText>
                                </FormControl>
                            </OrderCardGridCell>

                            <OrderCardGridCell>
                                <Typography>ISSUER IDNP</Typography>
                                <FormControl>
                                    <TextField id="issuerFiscalCode" label="Issuer Fiscal Code"
                                               error={!!(formik.errors.issuerFiscalCode)}
                                               helperText={formik.errors.issuerFiscalCode}
                                               onChange={async (e) => {
                                                   formik.handleChange(e);
                                                   if (isValidFiscalCode(e.target.value)) {
                                                       await resetAccountListState(setIssuerAccountsDropdownRange, parseInt(e.target.value));
                                                       await resetAgentState(setIssuerAgentInfo, parseInt(e.target.value))
                                                   }
                                               }}
                                               value={formik.values.issuerFiscalCode}
                                               type="number"
                                               name="issuerFiscalCode"/>

                                </FormControl>
                            </OrderCardGridCell>
                        </Box>
                    </Box>

                    <Box display="flex">
                        <OrderCardGridCell
                            style={{flexGrow: 1}}>
                            {`ISSUER REPRESENTER: ${formik.values.issuerAccountCode
                                ?
                                getBankOfOneAccountFromAccountList(issuerAccountsDropdownRange, formik.values.issuerAccountCode)?.name || ""
                                : ""
                            }
                            `}
                        </OrderCardGridCell>
                        <OrderCardGridCell>
                            {`BANK CODE: ${formik.values.issuerAccountCode
                                ?
                                getBankOfOneAccountFromAccountList(issuerAccountsDropdownRange, formik.values.issuerAccountCode)?.code || ""
                                : ""}
                                `}
                        </OrderCardGridCell>
                    </Box>

                    {/*// beneficiary section*/}

                    <Box display="flex">
                        <OrderCardGridCell style={{flexGrow: 1}}>
                            {`BENEFICIARY: ${beneficiaryAgentInfo?.name || ""}`}
                        </OrderCardGridCell>

                        <Box>
                            <OrderCardGridCell>
                                <FormControl>
                                    <InputLabel variant="standard" htmlFor="beneficiaryAccountCode">Beneficiary Acc.
                                        Code</InputLabel>
                                    <Select disabled={!!(formik.errors.beneficiaryFiscalCode)}
                                            style={{minWidth: "200px"}}
                                            id="beneficiaryAccountCode"
                                            name="beneficiaryAccountCode" onChange={formik.handleChange}
                                            value={formik.values.beneficiaryAccountCode}
                                            MenuProps={{style: {maxHeight: "400px"}}}
                                            error={!!(formik.errors.beneficiaryAccountCode)}>
                                        {beneficiaryAccountsDropdownRange.map((acc) => <MenuItem value={acc.accountCode}
                                                                                                 key={acc.id}>{acc.accountCode}</MenuItem>)}
                                    </Select>

                                    <FormHelperText
                                        error={!!(formik.errors.beneficiaryAccountCode)}>{formik.errors.beneficiaryAccountCode}</FormHelperText>
                                </FormControl>
                            </OrderCardGridCell>

                            <OrderCardGridCell>
                                <Typography>BENEFICIARY IDNP</Typography>
                                <FormControl>
                                    <TextField id="beneficiaryFiscalCode" label="Beneficiary Fiscal Code"
                                               error={!!(formik.errors.beneficiaryFiscalCode)}
                                               helperText={formik.errors.beneficiaryFiscalCode}
                                               onChange={async (e) => {
                                                   formik.handleChange(e);
                                                   if (isValidFiscalCode(e.target.value)) {
                                                       await resetAccountListState(setBeneficiaryAccountsDropdownRange, parseInt(e.target.value));
                                                       await resetAgentState(setBeneficiaryAgentInfo, parseInt(e.target.value))
                                                   }

                                               }}
                                               value={formik.values.beneficiaryFiscalCode}
                                               type="number"
                                               name="beneficiaryFiscalCode"/>
                                </FormControl>
                            </OrderCardGridCell>

                        </Box>
                    </Box>

                    <Box display="flex">
                        <OrderCardGridCell
                            style={{flexGrow: 1}}>
                            {`BENEFICIARY REPRESENTER: ${formik.values.beneficiaryAccountCode
                                ?
                                (getBankOfOneAccountFromAccountList(beneficiaryAccountsDropdownRange, formik.values.beneficiaryAccountCode)?.name || "")
                                : ""
                            }
                            `}
                        </OrderCardGridCell>
                        <OrderCardGridCell>
                            {`BANK CODE: ${formik.values.beneficiaryAccountCode
                                ?
                                getBankOfOneAccountFromAccountList(beneficiaryAccountsDropdownRange, formik.values.beneficiaryAccountCode)?.code || ""
                                : ""}
                                `}
                        </OrderCardGridCell>
                    </Box>

                    <Box display="flex">
                        <OrderCardGridCell style={{flexGrow: 1}}>

                            <TextField rows={5}
                                       fullWidth id="destination"
                                       name="destination"
                                       label="Destination"
                                       value={formik.values.destination}
                                       helperText={formik.errors.destination}
                                       error={!!(formik.errors.destination)}
                                       onChange={formik.handleChange}
                                       multiline/>
                        </OrderCardGridCell>
                        <OrderCardGridCell style={{display: "flex", justifyContent: "center", flexDirection: "column"}}>
                            <FormControl>
                                <Select placeholder="Transaction Type"
                                        style={{minWidth: "200px"}}
                                        id="transactionType"
                                        name="transactionType"
                                        onChange={(e: SelectChangeEvent<string>) => formik.setFieldValue("transactionType", transactionTypes.get(e.target.value))}
                                        value={transactionTypeNamesForDropdown[formik.values.transactionType]}
                                        MenuProps={{style: {maxHeight: "400px"}}}
                                        error={!!(formik.errors.transactionType)}>
                                    {transactionTypeNamesForDropdown.map(t => <MenuItem value={t}
                                                                                        key={t}>{t}</MenuItem>)}
                                </Select>
                                <FormHelperText>{"Transaction type"}</FormHelperText>
                            </FormControl>
                        </OrderCardGridCell>
                    </Box>


                    <Box display="flex">
                        <OrderCardGridCell style={{flexGrow: 1}}>
                            <Box display="flex" justifyContent="space-around">
                            <FormControl>
                                <DateTimePicker label="Issue date" renderInput={(params) =>
                                    <TextField {...params}
                                               helperText={formik.errors.issueDate}
                                               error={!!(formik.errors.issueDate)}/>}
                                                value={formik.values.issueDate}
                                                onChange={(value: Moment | null) => {
                                                    if (value?.isValid()) {
                                                        formik.setFieldValue("issueDate", value);
                                                    }
                                                }}/>
                            </FormControl>

                            <FormControl>
                                <DateTimePicker disabled={formik.values.transactionState !== 0} label="Execution date" renderInput={(params) =>
                                    <TextField {...params}
                                               error={!!(formik.errors.executionDate)} helperText={formik.errors.executionDate}/>}
                                                value={formik.values.transactionState === 0 ? formik.values.executionDate : ""}
                                                onChange={(value: Moment | null) => {
                                                    if (value?.isValid()) {
                                                        formik.setFieldValue("executionDate", value);
                                                    }
                                                }}/>
                            </FormControl>
                            </Box>
                        </OrderCardGridCell>

                        <OrderCardGridCell>
                            <FormControl>
                                <Select placeholder="Transaction state"
                                        style={{minWidth: "200px"}}
                                        id="transactionState"
                                        name="transactionState"
                                        onChange={(e: SelectChangeEvent<string>) => formik.setFieldValue("transactionState", transactionStates.get(e.target.value))}
                                        value={transactionStateNamesForDropdown[formik.values.transactionState]}
                                        MenuProps={{style:{maxHeight:"400px"}}}
                                        error={!!(formik.errors.transactionState)}>
                                    {transactionStateNamesForDropdown.map(t => <MenuItem value={t} key={t}>{t}</MenuItem>)}
                                </Select>
                                <FormHelperText>{"Transaction type"}</FormHelperText>
                            </FormControl>
                        </OrderCardGridCell>
                    </Box>

                </Paper>
                <Button type="submit">SUBMIT</Button>
            </form>
    )
};

export {OrderForm};