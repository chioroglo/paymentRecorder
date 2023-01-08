import React, {useCallback, useEffect, useState} from 'react';
import {BankTable} from "../../../../features/BankTable";
import {BankDto, BankModel} from "../../../../entities/bank/model/types";
import {getAllBanks} from "../../../../entities/bank/model/api";
import {useSnackbar} from "notistack";
import {IconButton, Typography} from "@mui/material";
import CloseIcon from "@mui/icons-material/Close";
import {CenteredLoader} from 'shared/ui/components';
import {BankForm} from "../../../../features/BankForm";
import {addNewBank} from "../../../../entities/bank/model/api/addNewBank";
import {CLIENT_ERROR_OCCURRED} from "../../../../shared/lib";

const BankMenuPage = () => {

    const [banks, setBanks] = useState<BankModel[]>([]);
    const [loading, setLoading] = useState<boolean>(true);

    const {enqueueSnackbar, closeSnackbar} = useSnackbar();

    const addNewBankFormAction = useCallback(async (dto: BankDto) => {

        setLoading(true);

        try {
            const response = await addNewBank(dto);
            setBanks([...banks, response]);
            enqueueSnackbar("Bank added successfully")
        } catch (errorMessage) {
            enqueueSnackbar(errorMessage as string || CLIENT_ERROR_OCCURRED);
        } finally {
            setLoading(false);
        }

    }, [banks]);

    useEffect(() => {

        getAllBanks()
            .then((result) => setBanks(result))
            .catch((message: string) => {
                enqueueSnackbar(message, {
                    variant: "error", action: (key) =>
                        <IconButton onClick={() => closeSnackbar(key)}>
                            <CloseIcon/>
                        </IconButton>
                });
            })
            .finally(() => setLoading(false));
    });

    return (
        <div>
            <div style={{margin: "0 auto", width: "fit-content", height: "fit-content"}}>
                <BankForm caption="Add new bank" formActionCallback={addNewBankFormAction} width={"100%"}/>
            </div>

            <Typography variant="h3" textAlign="center">
                Bank registry list
            </Typography>
            {loading ? <CenteredLoader/> :
                <BankTable width={"50%"} items={banks}/>
            }
        </div>
    );
};

export {BankMenuPage};