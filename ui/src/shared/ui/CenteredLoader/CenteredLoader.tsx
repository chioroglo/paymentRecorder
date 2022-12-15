import {Box, CircularProgress} from '@mui/material';
import React from 'react';

export const CenteredLoader = () => {
    return (
       <Box style={{margin:"50px auto",width:"fit-content"}}>
           <CircularProgress/>
       </Box>
    );
};
