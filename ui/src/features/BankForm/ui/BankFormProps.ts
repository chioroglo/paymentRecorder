import {BankDto} from "entities/bank/model/types";

export interface BankFormProps {
    initialValue?: BankDto,
    width?: string,
    caption: string,
    formActionCallback: (dto: BankDto) => Promise<void>
}