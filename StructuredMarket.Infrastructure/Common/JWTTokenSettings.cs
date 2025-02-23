﻿namespace StructuredMarket.Infrastructure.Common
{
    public class JwtTokenSettings
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Key { get; set; }
        public string ExpiryMinutes { get; set; }
    }

}
