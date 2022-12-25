// this function requires valid IANA timezone

export const changeTimezone = (date: Date, timezoneIana: string) => {
    return new Date(date.toLocaleString('en-US', {timeZone: timezoneIana}));
}