import {AgentModel} from 'entities/agent/model/types';
import React, {useCallback, useEffect, useState} from 'react';
import {getAllAgents} from "../../../../entities/agent/model/api";
import {IconButton, Typography} from "@mui/material";
import {useSnackbar, VariantType} from "notistack";
import {CLIENT_ERROR_OCCURRED} from "../../../../shared/lib";
import CloseIcon from "@mui/icons-material/Close";
import {AgentTable} from "../../../../features/AgentTable/ui/AgentTable";

const AgentMenuPage = () => {

    const [agents, setAgents] = useState<AgentModel[]>([]);
    const [loading,setLoading] = useState<boolean>(true);
    const [errorMessage,setErrorMessage] = useState<string | null>(null);

    const {enqueueSnackbar, closeSnackbar} = useSnackbar();

    const notifySnackbar = useCallback((message: string,variant: VariantType) => {
        enqueueSnackbar(message,{
            variant: variant,
            action: key =>
                <IconButton onClick={() => closeSnackbar(key)}>
                    <CloseIcon/>
                </IconButton>
        })
    },[enqueueSnackbar,closeSnackbar]);

    const fetchAgents = useCallback(async () => {

        setLoading(true);

        try {
            const response = await getAllAgents();
            setAgents(response);
        } catch(e) {
            let errorMessage = e as string || CLIENT_ERROR_OCCURRED;
            setErrorMessage(errorMessage);
            notifySnackbar(errorMessage,"error");
        }
        setLoading(false);

    }, []);

    useEffect(() => {
        fetchAgents();
    },[fetchAgents]);
    return (
        <div>
            <Typography variant="h3" textAlign="center">
                Agents registry list
            </Typography>

            <AgentTable width={"50%"} items={agents}/>

        </div>
    );
};

export {AgentMenuPage};