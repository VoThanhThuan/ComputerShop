using System;
using System.Collections.Generic;
using System.Text;

namespace Dashboard.Common
{
    public class ResultError : Result<string>
    {
        public ResultError(string message)
        {
            IsSuccessed = false;
            Message = message;
        }

        public ResultError()
        {
            IsSuccessed = false;
        }
    }
}
