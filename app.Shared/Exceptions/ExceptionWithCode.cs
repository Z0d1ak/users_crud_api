using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.Shared.Exceptions
{
    public sealed class ExceptionWithCode : Exception
    {
        public ExceptionWithCode(ErrorCode errorCode, string message)
            : base(message)
        {
            this.ErrorCode = errorCode;
        }

        public ErrorCode ErrorCode { get; }
    }
}
