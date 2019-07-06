using AlfredCMS.Core.Models.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AlfredCMS.Core.Repositories.Interfaces.Auth
{
    public interface IAuthInterface<T> where T: class
    {
        Task<ResponseType.Response> AuthorizeAsync(T credentials);
    }
}
