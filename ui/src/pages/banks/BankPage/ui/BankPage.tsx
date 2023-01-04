import React, {useCallback, useEffect, useState} from 'react';
import {useNavigate, useParams} from 'react-router-dom';
import {BankDto, BankModel} from "../../../../entities/bank/model/types";
import {CLIENT_ERROR_OCCURRED} from "../../../../shared/lib";
import {useSnackbar, VariantType} from "notistack";
import {getBankByCode} from "../../../../entities/bank/model/api";
import {Button, IconButton} from '@mui/material';
import CloseIcon from '@mui/icons-material/Close';
import {CenteredLoader, ErrorBannerWithMessage} from 'shared/ui/components';
import {BankForm} from 'features/BankForm';
import {editBank} from "../../../../entities/bank/model/api/editBank";
import {BankPageProps} from "../lib";
import {deleteBank} from "../../../../entities/bank/model/api/deleteBank";

const BankPage = ({width = '30%'}: BankPageProps) => { // preferrably use percents '50%' f.e

    const {bankCode} = useParams();
    const navigate = useNavigate();

    const [bank, setBank] = useState<BankModel>();
    const [loading, setLoading] = useState<boolean>(true);
    const [error, setError] = useState<boolean>(false);
    const [errorMessage, setErrorMessage] = useState<string>("");
    const {enqueueSnackbar, closeSnackbar} = useSnackbar();


    const fetchBank = useCallback(async (code: string) => {

        setLoading(true);

        try {
            const response = await getBankByCode(code);
            setBank(response);
            setLoading(false);
        } catch (errorMessage) {
            setError(true);
            setErrorMessage(errorMessage as string || CLIENT_ERROR_OCCURRED);
            enqueueSnackbar(errorMessage as string || CLIENT_ERROR_OCCURRED, {
                variant: "error", action: (key) =>
                    <IconButton onClick={() => closeSnackbar(key)}>
                        <CloseIcon/>
                    </IconButton>
            });
            setLoading(false);
        }
    }, [enqueueSnackbar, closeSnackbar]);

    const editBankCallback = async (dto: BankDto) => {

        let message: string;
        let type: VariantType;

        try {
            const response = await editBank(dto);
            setBank(response);
            message = "Successfully edited bank information";
            type = "success";
            navigate(`../${response.code}`, {replace: true});
        } catch (e) {
            message = e as string || CLIENT_ERROR_OCCURRED;
            type = "error";
        }

        enqueueSnackbar(message, {
            variant: type,
            action: (key) =>
                <IconButton>
                    <CloseIcon/>
                </IconButton>
        })

    }

    const deleteBankCallback = async (concurrencyToken: string, bankId: number) => {

        let message: string;
        let type: VariantType;

        try {
            await deleteBank(concurrencyToken, bankId);
            message = "Successfully deleted bank from the register";
            type = "success";
            navigate(`../menu`, {replace: true});
        } catch (e) {
            message = e as string || CLIENT_ERROR_OCCURRED;
            type = "error";
        }

        enqueueSnackbar(message, {
            variant: type,
            action: (key) =>
                <IconButton>
                    <CloseIcon/>
                </IconButton>
        })

    }


    useEffect(() => {

        fetchBank(bankCode || "");

    }, [fetchBank, bankCode]);


    return (
        <div style={{display: "flex", justifyContent: "space-between", flexDirection: "column"}}>
            {
                loading
                    ?
                    <CenteredLoader/>
                    :
                    (
                        (bank && !error)
                            ?
                            <>
                                <BankForm initialValue={{...bank, ifMatch: bank.entityTag}} width={width}
                                          caption={"Edit bank"} formActionCallback={editBankCallback}/>
                                <Button onClick={() => deleteBankCallback(bank?.entityTag, bank?.id)}
                                        style={{width: width, margin: "20px auto"}} variant="contained">DELETE
                                    ENTITY</Button>
                            </>
                            :
                            <ErrorBannerWithMessage message={errorMessage}/>
                    )
            }
        </div>
    );
};

export {BankPage};