﻿namespace PaymentRecorder.Extensions
{
    public class JWTConfiguration
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string DurationInDays { get; set; }
    }
}
