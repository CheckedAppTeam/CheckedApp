﻿namespace CheckedAppProject.API.Contracts
{
    public record AuthResponse(string Email, string UserName, string Token, string RefreshToken);
}
