import {Button} from '@mui/material';
import React from 'react';
import {getOrderByOrderNumber} from "../../entities/order/model/api";

const MainPage = () => {


    const queryApi = async () => {
        try {
            const response = await getOrderByOrderNumber(443);
            console.log("COMPONENT");
            console.log(response);
        } catch (err) {
            //console.log(err);
        }
    }

    return (
        <div>
            MAIN PAGE
            <Button onClick={() => queryApi()}>QUERY</Button>
        </div>
    );
};

export default MainPage;