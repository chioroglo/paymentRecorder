import { Button } from '@mui/material';
import React, {useEffect, useState} from 'react';
import {axiosInstance} from "../shared/api/http";

function App() {

    const [info,setInfo] = useState<any>();

    const register = () => {
        axiosInstance.post('/register',{
            "emailOrUsername" : "alexandr.chioroglo",
            "password": "Qwerty123_s_"
        });
    };

    return (
    <div className="App">
      <p>
          <Button onClick={register}>send request</Button>
      </p>
    </div>
  );
}

export { App };
