import {useEffect} from "react"

/* TODO REMIND use this component on each page */
export const useTitle = (title: string) => {
    useEffect(() => {
        const prevTitle = document.title;
        document.title = title;

        return () => {
            document.title = prevTitle;
        }
    })
}
