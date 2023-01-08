import {AgentDto, AgentModel} from 'entities/agent/model/types';
import React, {useCallback, useEffect, useState} from 'react';
import {useNavigate, useParams} from 'react-router-dom';
import "./styles/AgentPage.css";
import {useSnackbar, VariantType} from "notistack";
import {deleteAgent, editAgent, getAgentByFiscalCode} from "../../../../entities/agent/model/api";
import {CLIENT_ERROR_OCCURRED} from "../../../../shared/lib";
import {Button, IconButton} from '@mui/material';
import CloseIcon from '@mui/icons-material/Close';
import {CenteredLoader, ErrorBannerWithMessage} from "../../../../shared/ui/components";
import {AgentForm} from 'features/AgentForm/ui';
import {AgentPageProps} from "../lib";


const AgentPage = ({width = "30%"}: AgentPageProps) => {

    const {agentFiscalCode} = useParams();
    const navigate = useNavigate();


    const [agent, setAgent] = useState<AgentModel | null>(null);
    const [loading, setLoading] = useState<boolean>(true);
    const [errorMessage, setErrorMessage] = useState<string | null>(null);
    const {enqueueSnackbar, closeSnackbar} = useSnackbar();

    const notifySnackbar = useCallback((message: string, variant: VariantType) => {
        enqueueSnackbar(message, {
            variant: variant, action: (key) =>
                <IconButton onClick={() => closeSnackbar(key)}>
                    <CloseIcon/>
                </IconButton>
        })
    }, [enqueueSnackbar, closeSnackbar]);

    const fetchAgent = useCallback(async (fiscalCode: number) => {
        setLoading(true);

        try {
            const response = await getAgentByFiscalCode(fiscalCode);
            setAgent(response);
        } catch (e) {
            let errorMessage = e as string || CLIENT_ERROR_OCCURRED;

            setErrorMessage(errorMessage);
            notifySnackbar(errorMessage, "error");
        }
        setLoading(false);
    }, [notifySnackbar, setLoading]);

    const editAgentCallback = useCallback(async (dto: AgentDto) => {

        setLoading(true);
        try {
            const response = await editAgent(dto);
            setAgent(response);
            notifySnackbar("Successfully edited agent information", "success");
            navigate(`../${response.fiscalCode}`, {replace: true});
        } catch (e) {
            notifySnackbar(e as string || CLIENT_ERROR_OCCURRED, "error");
        }
        setLoading(false);
    }, [navigate, notifySnackbar, setLoading]);

    const deleteAgentCallback = useCallback(async (concurrencyToken: string, agentId: number) => {
        setLoading(true);
        try {
            await deleteAgent(concurrencyToken, agentId);
            notifySnackbar("Successfully deleted agent entity", "success");
            navigate(`../menu`, {replace: true});
        } catch (e) {
            notifySnackbar(e as string || CLIENT_ERROR_OCCURRED, "error");
        }
        setLoading(false);
    }, [navigate, notifySnackbar, setLoading]);

    useEffect(() => {
        setLoading(true);
        if (agentFiscalCode) {
            let fiscalCodeAsInt = parseInt(agentFiscalCode || "0");

            if (isNaN(fiscalCodeAsInt)) {
                notifySnackbar("Incorrect fiscal code introduced", "error");
                navigate("../menu");
            } else {
                fetchAgent(fiscalCodeAsInt);
            }

        } else {
            navigate("../menu");
        }
        setLoading(false);

    }, [fetchAgent, navigate, notifySnackbar, agentFiscalCode]);

    return (
        <div className="page-wrapper">
            {
                loading
                    ?
                    <CenteredLoader/>
                    :
                    (
                        (agent && !errorMessage)
                            ?
                            <>
                                <AgentForm
                                    initialValue={{
                                        ...agent,
                                        type: agent.typeId,
                                        ifMatch: agent.entityTag
                                    }}
                                    width={width}
                                    formActionCallback={editAgentCallback} caption={"Edit agent"}/>
                                <Button sx={{margin: "20px auto", width: width}} variant="contained"
                                        onClick={() => deleteAgentCallback(agent?.entityTag, agent?.id)}>
                                    DELETE ENTITY
                                </Button>
                            </>
                            :
                            (errorMessage && <ErrorBannerWithMessage message={errorMessage}/>)
                    )
            }
        </div>
    );
};

export {AgentPage};