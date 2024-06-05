using Application.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Exceptions
{
    public class Result<T>(bool succeeded, IEnumerable<string> errors, T data = default)
    {
        public bool Succeeded { get; private set; } = succeeded;
        public T Data { get; private set; } = data;
        public string[] Errors { get; init; } = errors.ToArray();

        public static Result<T> Success(T data)
        {
            return new Result<T>(true, Array.Empty<string>(), data);
        }
        public static Result<T> Success()
        {
            return new Result<T>(true, Array.Empty<string>());
        }
        public static Result<T> Failure(IEnumerable<string> errors , T data)
        {
            return new Result<T>(false, errors, data);
        }

        public static Result<T> Failure(IEnumerable<string> errors)
        {
            return new Result<T>(false, errors);
        }
    }


    public class Result(bool succeeded, IEnumerable<string> errors)
    {
        public bool Succeeded { get; private set; } = succeeded;
        public string[] Errors { get; init; } = errors.ToArray();

        public static Result Success()
        {
            return new Result(true, Array.Empty<string>());
        }

        public static Result Failure(IEnumerable<string> errors)
        {
            return new Result(false, errors);
        }
    }
}