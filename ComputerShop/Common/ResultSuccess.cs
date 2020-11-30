using System;
using System.Collections.Generic;
using System.Text;

namespace Dashboard.Common
{
    public class ResultSuccess<T> : Result<T>
    { 
        public ResultSuccess(T resultObj)
        {
            IsSuccessed = true;
            ResultObj = resultObj;
        }

        public ResultSuccess()
        {
            IsSuccessed = true;
        }
    }
}
