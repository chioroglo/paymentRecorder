import {createTheme} from "@mui/material";
import {palette} from "./palette";

export const theme = createTheme({
    palette: {
        primary: {
            main: palette.KASHMIR_BLUE
        },
        secondary: {
            main: palette.NEPAL
        },
        info: {
            main: palette.BOTTICELLI
        },
        warning: {
            main: palette.WARNING_YELLOW
        },
        success: {
            main: palette.SUCCESS_MINT
        }
    }
})