using AlfredCMS.Core.Models.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AlfredCMS.Core.Repositories.Interfaces
{
    public interface IAuthInterface
    {
        Task<ResponseType.Response> AuthorizeAsync(string username, string password);
    }
}
