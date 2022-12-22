// this method is used for getting string keys of a dictionary for dropdown lists

export const getListOfKeysFromDictionary = <T>(range: Map<string, T>): string[] => {
    const keys: string[] = [];

    range.forEach((key, value) => keys.push(value));
    return keys;
}