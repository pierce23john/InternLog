﻿namespace InternLog.Api.Contracts.V1.Requests.Identity
{
    public class LoginUserRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
