using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.Contracts
{
    public enum ErrorCodeDto
    {
        Successfull = 0,
        UserAlreadyExists = 1,
        UserNotFound = 2,

        Unknown = 3,
    }
}
