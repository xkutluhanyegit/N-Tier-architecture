using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Applications.Common.Results.Implementations
{
    public class ErrorResult:Result
    {
        public ErrorResult() : base(false)
        {
        }

        public ErrorResult(string message) :base(false,message)
        {
            
        }
    }
}