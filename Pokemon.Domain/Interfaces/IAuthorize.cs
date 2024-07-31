using Pokemon.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon.Domain.Interfaces
{
    public interface IAuthorize
    {
        Task<string> TokenJWT();
        Task<bool> ValidateUser(AuthLogin authLogin);
    }
}
