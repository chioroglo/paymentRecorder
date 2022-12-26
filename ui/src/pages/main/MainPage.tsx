import {Box, Button, Paper} from '@mui/material';
import React, { useState } from 'react';
import {getAllBanks} from "../../entities/bank/model/api";
import { BankModel } from 'entities/bank/model/types';
import { BankTable } from 'features/BankTable';

const MainPage = () => {

    const [st,setSt] = useState<BankModel[]>([]);

    const queryApi = async () => {
        try {
            const response = await getAllBanks();
            console.log(response);
            setSt(response);
        } catch (err) {

        }
    }

    return (
        <div>
            MAIN PAGE
            <Button onClick={() => queryApi()}>QUERY</Button>
            <Paper style={{margin:"0 auto",width:"50%"}}>
                <BankTable items={st}></BankTable>
            </Paper>

        </div>
    );
};

export default MainPage;