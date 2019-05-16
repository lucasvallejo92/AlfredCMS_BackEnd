using Microsoft.AspNetCore.Mvc;

namespace AlfredCMS.Core.Models.Data
{
    public class ResponseType
    {
        public enum Response
        {
            Not_Found,
            Cannot_Delete,
            Not_Exists,
            Deleted
        }
    }
}
