using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Bloomcoding.Bll.Exceptions
{
    public class EntryNotFoundException : ApiException
    {
        public EntryNotFoundException(string message) : base(HttpStatusCode.BadRequest, message)
        {

        }
    }
}
