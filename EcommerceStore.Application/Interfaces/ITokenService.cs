using EcommerceStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceStore.Application.Interfaces
{
    public interface ITokenService
    {
        public string GenerateJwtToken(User user);
    }
}
