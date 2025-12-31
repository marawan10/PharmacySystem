using PharmacySystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PharmacySystem.Application.Interfaces.Services
{
    public interface IAuthService
    {
        Task<string> GenerateJwtToken(string uid , string email , IList<string> roles);
    }
}
