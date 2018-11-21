using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ICredentialsService
    {
        bool Autenticate(string login, string password);
        CredentialsDTO Registrate(int employeeId, string password);
    }
}
