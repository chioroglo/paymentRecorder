import React from 'react';
import {Avatar} from "@mui/material";
import {FormHeaderProps} from './FormHeaderProps';


const FormHeader = ({iconColor, caption, icon}: FormHeaderProps) => {
    return (
        <div style={{justifyContent: "center", display: "flex", flexDirection: "column"}}>
            <Avatar style={{margin: "0 auto", padding: "10px", backgroundColor: iconColor}} variant="circular">
                {icon}
            </Avatar>

            <h2 style={{textAlign: "center", color: "#000"}}>
                {caption}
            </h2>

        </div>
    );
};

export {FormHeader};