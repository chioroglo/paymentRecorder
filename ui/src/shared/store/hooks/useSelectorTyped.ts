import {TypedUseSelectorHook, useSelector} from "react-redux";
import {RootState} from "../types";

export const useSelectorTyped: TypedUseSelectorHook<RootState> = useSelector;