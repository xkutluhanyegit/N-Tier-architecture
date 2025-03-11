using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Applications.Common.Results.Interfaces
{
    public interface IDataResult<T>:IResult
    {
        T Data { get; }
    }
}