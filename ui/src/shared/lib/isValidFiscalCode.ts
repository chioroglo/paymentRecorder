export const isValidFiscalCode = (fiscalCode: number | string) => {
    return fiscalCode.toString().length === 13;
}